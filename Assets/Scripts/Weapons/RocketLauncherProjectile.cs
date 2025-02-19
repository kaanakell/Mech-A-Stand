using UnityEngine;

public class RocketLauncherProjectile : ProjectileBase
{
    [SerializeField] private float lifetime = 2f;
    private Collider2D projectileCollider;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        projectileCollider = GetComponent<Collider2D>();
        projectileCollider.enabled = false;
    }

    protected override void Update()
    {
        base.Update();
        if (rb.linearVelocity.magnitude < 0.1f && !projectileCollider.enabled)
        {

            projectileCollider.enabled = true;

            HitDetection();
        }
    }

    protected override void HitDetection()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, weapon.weaponStats.attackAreaForProjectileWeapons);

        foreach (Collider2D hit in hits)
        {
            IDamageable enemy = hit.GetComponent<IDamageable>();
            if (enemy != null && enemy.EntityType == EntityType.Enemy)
            {
                if (!CheckRepeatHit(enemy))
                {
                    enemiesHit.Add(enemy);
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Weapons_Missile_Explode");
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

}
