using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class AgentMovement : MonoBehaviour
{
    protected Rigidbody2D rigidbody2D;
    [field: SerializeField]
    public MovementDataSO m_MovementData { get; set; }
    [SerializeField] protected float currentVelocity = 3f;
    protected Vector2 movementDirection;
    protected bool isKnockBack = false;

    [field: SerializeField]
    public UnityEvent<float> OnVelocityChange { get; set; }
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    public void MoveAgent(Vector2 movementInput)
    {
        if (movementInput.magnitude > 0)
        {
            if (Vector2.Dot(movementInput.normalized, movementDirection) < 0)
            {
                currentVelocity = 0;
            }
            movementDirection = movementInput.normalized;
        }
        currentVelocity = CalculateSpeed(movementInput);
    }

    private float CalculateSpeed(Vector2 movementInput)
    {
        if (movementInput.magnitude > 0)
        {
            currentVelocity += m_MovementData.accleration * Time.deltaTime;
        }
        else
        {
            currentVelocity -= m_MovementData.deacceleration * Time.deltaTime;
        }
        return Math.Clamp(currentVelocity, 0.0f, m_MovementData.maxSpeed);
    }
    private void FixedUpdate()
    {
        OnVelocityChange?.Invoke(currentVelocity);
        if (!isKnockBack)
            rigidbody2D.velocity = currentVelocity * movementDirection.normalized;
    }
    public void StopImediatelly()
    {
        currentVelocity = 0;
        rigidbody2D.velocity = Vector2.zero;
    }
    public void KnockBack(Vector2 direction, float power, float duration)
    {
        if (!isKnockBack)
        {
            isKnockBack = true;
            StartCoroutine(KnockBackCoroutine(direction, power, duration));
        }
    }
    public void ResetKnockBack()
    {
        StopAllCoroutines();
        ResetKnockBackParameters();
    }
    IEnumerator KnockBackCoroutine(Vector2 direction, float power, float duration)
    {
        rigidbody2D.AddForce(direction.normalized * power, ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);
        ResetKnockBackParameters();
    }

    private void ResetKnockBackParameters()
    {
        currentVelocity = 0;
        rigidbody2D.velocity = Vector2.zero;
        isKnockBack = false;
    }
}
