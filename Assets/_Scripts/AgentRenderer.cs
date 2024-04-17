using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AgentRenderer : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void FaceDirection(Vector2 pointerInput)
    {
        var direction = (Vector3)pointerInput - transform.position;
        var result = Vector3.Cross(Vector2.up, direction);
        spriteRenderer.flipX=(result.z>0)? true:false;
    }
}
