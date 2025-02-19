using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour, IPoolMember
{
    private PoolMember poolMember;
    protected WeaponBase weapon;
    protected Vector3 direction;

    private float attackArea = 0.7f;
    private float speed;
    protected int damage;
    protected int numberOffHits;
    private float timeToLeaveTimer = 6f;
    protected List<IDamageable> enemiesHit;

    public void SetDirection(float x, float y)
    {
        SetDirection(new Vector3(x, y, 0f));
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    protected virtual void Update()
    {
        Move();
        TimerToLeave();
    }

    private void TimerToLeave()
    {
        timeToLeaveTimer -= Time.deltaTime;
        if (timeToLeaveTimer < 0f)
        {
            DestroyProjectile();
        }
    }

    protected void DestroyProjectile()
    {
        if (poolMember == null)
        {
            Destroy(gameObject);
            Debug.Log("pew pew");
        }
        else
        {
            poolMember.ReturnToPool();
        }
    }

    private void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    protected virtual void HitDetection()
    {
        if (numberOffHits > 0)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackArea);
            foreach (Collider2D hit in hits)
            {
                IDamageable enemy = hit.GetComponent<IDamageable>();
                if (enemy != null && enemy.EntityType == EntityType.Enemy)
                {
                    if (!CheckRepeatHit(enemy))
                    {
                        enemiesHit.Add(enemy);
                        weapon.ApplyDamage(damage, enemy);
                        numberOffHits--;

                        if (numberOffHits <= 0)
                        {
                            DestroyProjectile();
                            return;
                        }
                    }
                }
            }
        }

        if (numberOffHits <= 0)
        {
            DestroyProjectile();
        }
    }

    protected bool CheckRepeatHit(IDamageable enemy)
    {
        if (enemiesHit == null)
        {
            enemiesHit = new List<IDamageable>();
        }
        return enemiesHit.Contains(enemy);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HitDetection();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackArea);
    }

    public void SetStats(WeaponBase weaponBase)
    {
        weapon = weaponBase;
        damage = weaponBase.GetDamage();
        numberOffHits = weaponBase.weaponStats.numberOfHits;
        speed = weaponBase.weaponStats.projectileSpeed;
        attackArea = weaponBase.weaponStats.attackAreaForProjectileWeapons;
    }

    private void OnEnable()
    {
        timeToLeaveTimer = 6f;
    }

    public void SetPoolMember(PoolMember poolMember)
    {
        this.poolMember = poolMember;
    }

    public void Reset()
    {
        enemiesHit = new List<IDamageable>();
    }
}
