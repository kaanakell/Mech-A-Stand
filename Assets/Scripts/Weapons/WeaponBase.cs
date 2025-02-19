using System;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public enum DirectionOfAttack
    {
        None,
        Forward,
        LeftRight,
        UpDown,
        Auto,
        Manuel
    }

    protected PlayerMovement playerMovement;
    public WeaponData weaponDataReference;

    public WeaponStats weaponStats;
    float timer;
    public Vector2 vectorOfAttack;


    PlayerHealth weilder;
    [SerializeField] DirectionOfAttack attackDirection;
    [SerializeField] protected PoolObjectData projectilePrefab;
    PoolManager poolManager;

    private void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Attack();
            timer = weaponStats.timeToAttack;
        }
    }

    public void ApplyDamage(Collider2D[] colliders)
    {
        
        int damage = GetDamage();
        Debug.Log("Weapon Base: ApplyDamage : damage  = " + damage );
        for (int i = 0; i < colliders.Length; i++)
        {
            IDamageable damageable = colliders[i].GetComponent<IDamageable>();
            if (damageable != null)
            {
                ApplyDamage(damage, damageable);
            }
        }
    }

    public void ApplyDamage(int damage, IDamageable damageable)
    {
        if (damageable is MonoBehaviour damageableMonoBehaviour)
        {
            Transform targetTransform = damageableMonoBehaviour.transform;
            Vector2 knockbackDirection = ((Vector2)targetTransform.position - (Vector2)transform.position).normalized;
            damageable.TakeDamageWithKnockBack(damage, knockbackDirection, weaponStats.knockbackForce);
            ApplyAdditionalEffects(damageable);
        }
    }

    private void ApplyAdditionalEffects(IDamageable damageable)
    {
        damageable.Stun(weaponStats.stun);
    }

    public virtual void SetData(WeaponData wd)
    {
        weaponDataReference = wd;

        weaponStats = new WeaponStats(wd.stats);
    }

    public void SetPoolManager(PoolManager poolManager)
    {
        this.poolManager = poolManager;
    }

    public abstract void Attack();

    public int GetDamage()
    {
        int damage = (int)(weaponStats.damage * weilder.damageBonus);
        return damage;
    }

    public void Upgrade(UpgradeData upgradeData)
    {
        weaponStats.Sum(upgradeData.weaponUpgradeStats);
    }

    internal void AddOwnerCharacter(PlayerHealth player)
    {
        weilder = player;
    }

    public void UpdateVectorOfAttack()
    {
        if (attackDirection == DirectionOfAttack.None)
        {
            vectorOfAttack = Vector2.zero;
            return;
        }

        switch (attackDirection)
        {
            case DirectionOfAttack.Forward:
                vectorOfAttack.x = playerMovement.lastHorizontalCoupledVector;
                vectorOfAttack.y = playerMovement.lastVerticalCoupledVector;
                break;
            case DirectionOfAttack.LeftRight:
                vectorOfAttack.x = playerMovement.lastHorizontalDeCoupledVector;
                vectorOfAttack.y = 0f;
                break;
            case DirectionOfAttack.UpDown:
                vectorOfAttack.x = 0f;
                vectorOfAttack.y = playerMovement.lastVerticalDeCoupledVector;
                break;
            case DirectionOfAttack.Auto:
                break;
            case DirectionOfAttack.Manuel:
                break;
        }
        vectorOfAttack = vectorOfAttack.normalized;
    }

    protected void SpawnProjectile(PoolObjectData poolObjectData, Vector3 position, Vector3 direction, float spread = 0, int index = 0)
    {
        GameObject projectile = poolManager.GetObject(poolObjectData);

        Vector3 perpendicular = Vector3.Cross(direction, Vector3.forward).normalized;

        float offset = (index - (weaponStats.numberOfAttacks - 1) / 2.0f) * spread;
        position += perpendicular * offset;

        projectile.transform.position = position;
        projectile.transform.rotation = Quaternion.identity;

        var projectileComponent = projectile.GetComponent<ProjectileBase>();
        if (projectileComponent != null)
        {
            projectileComponent.SetDirection(direction);
            projectileComponent.SetStats(this);
            Debug.Log("STAT APPLIED" + this.GetDamage());
        }
    }

}