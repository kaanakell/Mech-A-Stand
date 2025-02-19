using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCountDisplay : MonoBehaviour
{
    [SerializeField] private Image weaponIcon;
    [SerializeField] private TextMeshProUGUI upgradeCountText;

    private int upgradeCount = 0;

    public void SetWeaponItem(Sprite icon, int upgradeLevel)
    {
        weaponIcon.sprite = icon;
        upgradeCount = upgradeLevel;
        UpdateUI();
    }

    public void IncrementUpgrade()
    {
        upgradeCount++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        upgradeCountText.text = "x" + upgradeCount;
    }
}
