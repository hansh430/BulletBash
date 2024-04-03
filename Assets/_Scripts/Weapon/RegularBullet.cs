using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularBullet : Bullet
{
    protected Rigidbody2D rigidbody2D;
    private bool isDead = false;
    public override BulletDataSO BulletData
    {
        get => base.BulletData;
        set
        {
            base.BulletData = value;
            rigidbody2D = GetComponent<Rigidbody2D>();
            rigidbody2D.drag = BulletData.Friction;
        }
    }
    private void FixedUpdate()
    {
        if (rigidbody2D != null && BulletData != null)
        {
            rigidbody2D.MovePosition(transform.position + BulletData.BulletSpeed * transform.right * Time.fixedDeltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead)
            return;
        isDead = true;
        var hittable = collision.GetComponent<IHittable>();
        hittable?.GetHit(bulletData.Damage, gameObject);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            HitObstacle(collision);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            HitEnemy(collision);
        }
        Destroy(gameObject);
    }
    private void HitEnemy(Collider2D collision)
    {
        var knockBack = collision.GetComponent<IKnockBack>();
        knockBack?.KnockBack(transform.right, BulletData.KnockBackPower, BulletData.KnockBackDelay);
        Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
        Instantiate(bulletData.ImpactEnemyPrefab, collision.transform.position + (Vector3)randomOffset, Quaternion.identity);
    }
    private void HitObstacle(Collider2D collision)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);
        if (hit.collider != null)
        {
            Instantiate(bulletData.ImpactObstaclePrefab, hit.point, Quaternion.identity);
        }
    }
}
