using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemStats
{
    public int armor;
    public float armorRegenerationRate;
    public float armorRegenerationCooldown;
    public float healthRegenerationRate;
    public float healthRegenerationCooldown;
    public float dashDamage;
    public int dashCount;

    internal void Sum(ItemStats stats)
    {
        armor += stats.armor;
        armorRegenerationRate += stats.armorRegenerationRate;
        armorRegenerationCooldown += stats.armorRegenerationCooldown;
        healthRegenerationRate += stats.healthRegenerationRate;
        healthRegenerationCooldown += stats.healthRegenerationCooldown;
        dashDamage += stats.dashDamage;
        dashCount += stats.dashCount;
    }
}


[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string Name;
    public ItemStats stats;
    public List<UpgradeData> upgrades;

    public void Init(string Name)
    {
        this.Name = Name;
        stats = new ItemStats();
        upgrades = new List<UpgradeData>();
    }

    public void Equip(PlayerHealth playerHealth, PlayerMovement playerMovement)
    {
        playerHealth.maxArmor += stats.armor;
        playerHealth.armor += stats.armor;
        playerHealth.armorBar.SetState(playerHealth.armor, playerHealth.maxArmor);
        playerHealth.armorRegenerationRate += stats.armorRegenerationRate;
        playerHealth.armorRegenerationCooldown = stats.armorRegenerationCooldown;
        
        playerHealth.hpRegenerationRate += stats.healthRegenerationRate;
        playerHealth.hpRegenerationCooldown = stats.healthRegenerationCooldown;

        playerMovement.dashDamage += stats.dashDamage;
        //playerMovement.dashCount += stats.dashCount;
        playerMovement.UpgradeDash(stats.dashCount);
        
    }

    public void UnEquip(PlayerHealth playerHealth, PlayerMovement playerMovement)
    {
        playerHealth.maxArmor -= stats.armor;
        playerHealth.armor -= stats.armor;

        playerHealth.armorRegenerationRate -= stats.armorRegenerationRate;
        playerHealth.armorRegenerationCooldown = stats.armorRegenerationCooldown;

        playerHealth.hpRegenerationRate -= stats.healthRegenerationRate;
        playerHealth.hpRegenerationCooldown = stats.healthRegenerationCooldown;

        playerMovement.dashDamage -= stats.dashDamage;
        playerMovement.dashCount -= stats.dashCount;
    }



}
