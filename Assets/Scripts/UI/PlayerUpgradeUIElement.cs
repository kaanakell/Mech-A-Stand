using TMPro;
using UnityEngine;

public class PlayerUpgradeUIElement : MonoBehaviour
{
    [SerializeField] PlayerPersistentUpgrades playerPersistentUpgrades;

    [SerializeField] TextMeshProUGUI upgradeName;
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] TextMeshProUGUI price;

    [SerializeField] DataContainer dataContainer;

    void Start()
    {
        UpdateElement();
    }

    public void Upgrade()
    {
        PlayerUpgrades playerUpgrades = dataContainer.upgrades[(int)playerPersistentUpgrades];

        if(playerUpgrades.level >= playerUpgrades.maxLevel)
        {
            return;
        }
        
        if(dataContainer.coins >= playerUpgrades.costToUpgrade)
        {
            dataContainer.coins -= playerUpgrades.costToUpgrade;
            playerUpgrades.level++;
            UpdateElement();
        }

    }

    void UpdateElement()
    {
        PlayerUpgrades playerUpgrade = dataContainer.upgrades[(int)playerPersistentUpgrades];

        upgradeName.text = playerPersistentUpgrades.ToString();
        level.text = playerUpgrade.level.ToString();
        price.text = playerUpgrade.costToUpgrade.ToString();
    }

}
