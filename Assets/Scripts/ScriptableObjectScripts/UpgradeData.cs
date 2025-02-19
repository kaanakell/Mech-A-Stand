using UnityEngine;

public enum UpgradeType
{
    WeaponUpgrade,
    ItemUpgrade,
    WeaponGet,
    ItemGet
}

public enum UpgradeTier
{
    Tier0,
    Tier1,
    Tier2,
    Tier3,
    Tier4,
    Tier5
}

[CreateAssetMenu]
public class UpgradeData : ScriptableObject
{
    public UpgradeType upgradeType;
    public string Name;
    public string Description;
    public Sprite icon;
    public UpgradeTier upgradeTier;

    public WeaponData weaponData;
    public WeaponStats weaponUpgradeStats;

    public Item item;
    public ItemStats itemStats;
}
