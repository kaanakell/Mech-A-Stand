using UnityEngine;

public class GarlickTypeWeapon : WeaponBase
{

    public override void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, weaponStats.attackAreaSizeForCloseRangeWeapon);
        ApplyDamage(colliders);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, weaponStats.attackAreaSizeForCloseRangeWeapon);
    }
}
