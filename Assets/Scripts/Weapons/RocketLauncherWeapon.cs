using UnityEngine;

public class RocketLauncherWeapon : WeaponBase
{
    [SerializeField] private float spread = 0.5f;

    public override void Attack()
    {
        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Weapons_EMP_Plant");
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            Vector3 direction = new Vector3(randomDirection.x, randomDirection.y, 0);
            SpawnProjectile(projectilePrefab, transform.position, direction, spread, i);
        }
    }
}
