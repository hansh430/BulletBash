using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IAgent, IHittable
{
    [SerializeField]
    private int maxHealth;

    private int health;
    public int Health
    {
        get => health;
        set
        {
            health = Mathf.Clamp(value, 0, maxHealth);
            UIHealth.UpdateHealthUI(health);
        }
    }
    private bool isPlayerDead;
    public bool IsPlayerDead => isPlayerDead;

    [field: SerializeField]
    public UIHealth UIHealth { get; set; }

    [field: SerializeField]
    public UnityEvent OnDie { get; set; }

    [field: SerializeField]
    public UnityEvent OnGetHit { get; set; }

    private PlayerWeapon playerWeapon;
    private void Awake()
    {
        playerWeapon = GetComponentInChildren<PlayerWeapon>();
    }

    private void Start()
    {
        Health = maxHealth;
        UIHealth.Initialize(Health);
    }
    public void GetHit(int damage, GameObject damageDealer)
    {
        if (!isPlayerDead)
        {
            Health--;
            OnGetHit?.Invoke();
            if (Health <= 0)
            {
                OnDie?.Invoke();
                isPlayerDead = true;
                ScoreSubject.Instance.SetScoreToLeaderBoard();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Resource"))
        {
            var resource = collision.gameObject.GetComponent<Resources>();
            if (resource != null)
            {
                switch (resource.ResourcesData.ResourceType)
                {
                    case ResourcesTypeEnum.Health:
                        if (Health >= maxHealth)
                        {
                            return;
                        }
                        Health += resource.ResourcesData.GetAmount();
                        resource.PickUpResource();
                        break;
                    case ResourcesTypeEnum.Ammo:
                        if (playerWeapon.AmmoFull)
                        {
                            return;
                        }
                        playerWeapon.AddAmmo(resource.ResourcesData.GetAmount());
                        resource.PickUpResource();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
