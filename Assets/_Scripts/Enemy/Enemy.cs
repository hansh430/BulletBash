using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHittable, IAgent, IKnockBack
{
    [field: SerializeField]
    public EnemyDataSO EnemyData { get; set; }

    [field: SerializeField]
    public int Health { get; private set; } = 2;

    [field: SerializeField]
    public EnemyAttack enemyAttack { get; set; }

    [field: SerializeField]
    public UnityEvent OnGetHit { get; set; }

    [field: SerializeField]
    public UnityEvent OnDie { get; set; }

    private bool isEnemyDead = false;
    public bool IsEnemyDead { get => isEnemyDead; set => isEnemyDead = value; }

    private AgentMovement agentMovement;

    private void Awake()
    {
        if (enemyAttack == null)
            enemyAttack = GetComponent<EnemyAttack>();
        agentMovement = GetComponent<AgentMovement>();
    }

    private void Start()
    {
        Health = EnemyData.MaxHealth;
    }
    public void GetHit(int damage, GameObject damageDealer)
    {
        if (!isEnemyDead)
        {
            Health--;
            OnGetHit?.Invoke();
            if (Health <= 0)
            {
                isEnemyDead = true;
                EnemySpawner.EnemyCount--;
                OnDie?.Invoke();
            }
        }
    }
    public void Die()
    {
        //  Destroy(gameObject);
        gameObject.SetActive(false);
    }
    public void PerformAttack()
    {
        if (!isEnemyDead)
        {
            enemyAttack.Attack(EnemyData.Damage);
        }
    }

    public void KnockBack(Vector2 direction, float power, float duration)
    {
        agentMovement.KnockBack(direction, power, duration);
    }
}
