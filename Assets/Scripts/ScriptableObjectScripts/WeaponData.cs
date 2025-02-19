using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponStats
{
    public int damage;
    public float timeToAttack;
    public float knockbackForce;
    public int numberOfAttacks;
    public int numberOfHits;
    public float projectileSpeed;
    public float stun;
    public float attackAreaForProjectileWeapons;
    public float attackAreaSizeForCloseRangeWeapon;
    
    public WeaponStats(WeaponStats stats)
    {
        this.damage = stats.damage;
        this.timeToAttack = stats.timeToAttack;
        this.knockbackForce = stats.knockbackForce;
        this.numberOfAttacks = stats.numberOfAttacks;
        this.numberOfHits = stats.numberOfHits;
        this.projectileSpeed = stats.projectileSpeed;
        this.stun = stats.stun;
        this.attackAreaForProjectileWeapons = stats.attackAreaForProjectileWeapons;
        this.attackAreaSizeForCloseRangeWeapon = stats.attackAreaSizeForCloseRangeWeapon;
    }

    internal void Sum(WeaponStats weaponUpgradeStats)
    {
        this.damage += weaponUpgradeStats.damage;
        this.timeToAttack += weaponUpgradeStats.timeToAttack;
        this.knockbackForce += weaponUpgradeStats.knockbackForce;
        this.numberOfAttacks += weaponUpgradeStats.numberOfAttacks;
        this.numberOfHits += weaponUpgradeStats.numberOfHits;
        this.projectileSpeed += weaponUpgradeStats.projectileSpeed;
        this.stun += weaponUpgradeStats.stun;
        this.attackAreaForProjectileWeapons += weaponUpgradeStats.attackAreaForProjectileWeapons;
        this.attackAreaSizeForCloseRangeWeapon += weaponUpgradeStats.attackAreaSizeForCloseRangeWeapon;
    }
}

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public string Name;
    public WeaponStats stats;
    public GameObject weaponBasePrefab;
    public List<UpgradeData> upgrades;
}
