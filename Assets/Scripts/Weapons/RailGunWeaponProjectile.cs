using UnityEngine;

public class RailGunWeaponProjectile : ProjectileBase
{
    [SerializeField] float lifetime = 2f;

    private void Start()
    {
       // Destroy(gameObject, lifetime);
    }
    
    protected override void HitDetection()
    {
        base.HitDetection();
        //Custom logic
    }
}
