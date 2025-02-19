using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    int level = 1;
    int experience = 0;
    [SerializeField] PlayerExperienceBar playerExperienceBar;
    [SerializeField] UpgradePanelManager upgradePanelManager;

    [SerializeField] List<UpgradeData> upgrades;
    List<UpgradeData> selectedUpgrades;

    [SerializeField] List<UpgradeData> acquiredUpgrades;

    WeaponManager weaponManager;
    PassiveItems passiveItems;

    [SerializeField] List<UpgradeData> upgradesAvailableOnStart;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
        passiveItems = GetComponent<PassiveItems>();
    }

    int TO_LEVEL_UP
    {
        get
        {
            return level * 1000;
        }
    }

    internal void AddUpgradesIntoTheListOfAvaliableUpgrades(List<UpgradeData> upgradesToAdd)
    {
        if(upgradesToAdd == null)
        {
            return;
        }
        this.upgrades.AddRange(upgradesToAdd);
    }

    void Start()
    {
        playerExperienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
        playerExperienceBar.SetLevelText(level);
        AddUpgradesIntoTheListOfAvaliableUpgrades(upgradesAvailableOnStart);
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        CheckLevelUp();
        playerExperienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
    }

    public void Upgrade(int selectedUpgradeId)
    {
        UpgradeData upgradeData = selectedUpgrades[selectedUpgradeId];

        if (acquiredUpgrades == null)
        {
            acquiredUpgrades = new List<UpgradeData>();
        }

        switch (upgradeData.upgradeType)
        {
            case UpgradeType.WeaponUpgrade:
            weaponManager.UpgradeWeapon(upgradeData);
                break;
            case UpgradeType.ItemUpgrade:
            passiveItems.UpgradeItem(upgradeData);
                break;
            case UpgradeType.WeaponGet:
            weaponManager.AddWeapon(upgradeData.weaponData);
                break;
            case UpgradeType.ItemGet:
            passiveItems.Equip(upgradeData.item);
            AddUpgradesIntoTheListOfAvaliableUpgrades(upgradeData.item.upgrades);
                break;
        }

        acquiredUpgrades.Add(upgradeData);
        upgrades.Remove(upgradeData);
    }

    public void CheckLevelUp()
    {
        if (experience >= TO_LEVEL_UP)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        if (selectedUpgrades == null)
        {
            selectedUpgrades = new List<UpgradeData>();
        }
        selectedUpgrades.Clear();
        selectedUpgrades.AddRange(GetUpgrades(4));

        upgradePanelManager.OpenPanel(selectedUpgrades);
        experience -= TO_LEVEL_UP;
        level += 1;
        playerExperienceBar.SetLevelText(level);
    }

    public void ShuffleUpgrades()
    {
        for(int i = upgrades.Count - 1; i > 0; i--)
        {
            int x = Random.Range(0, i + 1);
            UpgradeData shuffleElement = upgrades[i];
            upgrades[i] = upgrades[x];
            upgrades[x] = shuffleElement;
        }
    }

    public List<UpgradeData> GetUpgrades(int count)
    {
        ShuffleUpgrades();
        List<UpgradeData> upgradeList = new List<UpgradeData>();

        if (count > upgrades.Count)
        {
            count = upgrades.Count;
        }

        for (int i = 0; i < count; i++)
        {
            upgradeList.Add(upgrades[i]);

        }

        return upgradeList;
    }
}
