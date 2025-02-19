using UnityEngine;

public class RailGunWeapon : WeaponBase
{
    [SerializeField] private float range = 10f;
    [SerializeField] private float spread = 0.5f;

    private Enemy FindClosestEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
        Enemy closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
        }
        return closestEnemy;
    }

    public override void Attack()
    {
        Enemy closestEnemy = FindClosestEnemy();
        if (closestEnemy == null) return;
        FMODUnity.RuntimeManager.PlayOneShot("event:/Weapon_RailGun_FIRE");
        Vector3 direction = (closestEnemy.transform.position - transform.position).normalized;
        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            SpawnProjectile(projectilePrefab, transform.position, direction, spread, i);
        }
    }
}
