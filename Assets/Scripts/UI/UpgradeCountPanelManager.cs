using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCountPanelManager : MonoBehaviour
{
    [SerializeField] private UpgradePanelManager upgradePanelManager;
    public GameObject upgradeUIPrefab;
    public Transform upgradeShowPanel;

    private Dictionary<string, GameObject> upgradeUIElements = new Dictionary<string, GameObject>();

    private void Start()
    {
        upgradePanelManager.onUpgradeChose -= OnUpgradeChose;
        upgradePanelManager.onUpgradeChose += OnUpgradeChose;
        upgradeShowPanel.gameObject.SetActive(false);
    }

    private void OnUpgradeChose(Dictionary<UpgradeData, int> dictionary)
    {
        if (dictionary.Count <= 0)
        {
            upgradeShowPanel.gameObject.SetActive(false);
            return;
        }
        DestroyOldUpgrades();
        foreach (var upgradeData in dictionary)
        {
            UpdateUpgradeUI(upgradeData.Key, upgradeData.Value);
        }
        upgradeShowPanel.gameObject.SetActive(true);
    }

    public void UpdateUpgradeUI(UpgradeData upgradeData, int count)
    {
        GameObject newUI = Instantiate(upgradeUIPrefab, upgradeShowPanel);
        newUI.transform.Find("UpgradeImage").GetComponent<Image>().sprite = upgradeData.icon;

        TMPro.TextMeshProUGUI countText = newUI.transform.Find("UpgradeCount").GetComponent<TMPro.TextMeshProUGUI>();
        countText.text = count > 0 ? count.ToString() : "";
        countText.color = Color.white;

        upgradeUIElements.Add(upgradeData.Name, newUI);
    }

    private void DestroyOldUpgrades()
    {
        foreach (var uiElement in upgradeUIElements)
        {
            Destroy(uiElement.Value.gameObject);
        }
        upgradeUIElements.Clear();
    }
}
