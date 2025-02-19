using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Transform weaponObjectsContainer;
    [SerializeField] WeaponData startingWeapon;
    [SerializeField] PoolManager poolManager;

    List<WeaponBase> weapons;

    PlayerHealth player;

    private void Awake()
    {
        weapons = new List<WeaponBase>();
        player = GetComponent<PlayerHealth>();
    }

    void Start()
    {
        AddWeapon(startingWeapon);
    }

    public void AddWeapon(WeaponData weaponData)
    {
        GameObject weaponGameObject = Instantiate(weaponData.weaponBasePrefab, weaponObjectsContainer);

        WeaponBase weaponBase= weaponGameObject.GetComponent<WeaponBase>();

        weaponBase.SetData(weaponData);
        weaponBase.SetPoolManager(poolManager);
        weapons.Add(weaponBase);
        weaponBase.AddOwnerCharacter(player);

        weaponGameObject.GetComponent<WeaponBase>().SetData(weaponData);
        PlayerLevel level = GetComponent<PlayerLevel>();

        if(level != null)
        {
            level.AddUpgradesIntoTheListOfAvaliableUpgrades(weaponData.upgrades);
        }
    }

    internal void UpgradeWeapon(UpgradeData upgradeData)
    {
        WeaponBase weaponToUpgrade = weapons.Find(wd => wd.weaponDataReference == upgradeData.weaponData);
        weaponToUpgrade.Upgrade(upgradeData);
    }
}
