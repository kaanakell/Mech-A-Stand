using UnityEngine;

public class PlayerMainWeapon : WeaponBase
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float firePointDistance = 1.0f; // Distance from the player
    private float nextFireTime;

    private void Update()
    {
        AimAtMouse();
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Attack();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void AimAtMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;

        // Set firePoint rotation
        firePoint.rotation = Quaternion.Euler(0, 0, angle);

        // Move firePoint to maintain distance from player
        firePoint.position = transform.position + (aimDirection * firePointDistance);
    }

    public override void Attack()
    {
        SpawnProjectile(projectilePrefab, firePoint.position, firePoint.up);
        FMODUnity.RuntimeManager.PlayOneShot("event:/weapons_MiniGun_FIRE");
    }
}
