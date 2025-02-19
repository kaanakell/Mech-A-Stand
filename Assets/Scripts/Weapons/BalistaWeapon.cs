using UnityEngine;

public class BalistaWeapon : WeaponBase
{
    [SerializeField] private float spread = 0.5f;

    //private PlayerMovement playerMovement;

    private void Start()
    {
        //playerMovement = transform.parent.GetComponentInParent<PlayerMovement>();
    }

    public override void Attack()
    {
        // Ensure the attack direction is updated
        UpdateVectorOfAttack();

        // Convert Vector2 to Vector3
        Vector3 direction = new Vector3(vectorOfAttack.x, vectorOfAttack.y, 0);


        if (direction == Vector3.zero)
        {
            direction = Vector3.right;
        }
        if ((playerMovement.playerMovementVector != Vector3.zero))
        {
            for (int i = 0; i < weaponStats.numberOfAttacks; i++)
            {
                SpawnProjectile(projectilePrefab, transform.position, direction, spread, i);
                FMODUnity.RuntimeManager.PlayOneShot("event:/weapons_MiniGun_FIRE");
            }
        }

    }
}
