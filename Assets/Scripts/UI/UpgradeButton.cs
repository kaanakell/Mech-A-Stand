using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI upgradeNameText;
    [SerializeField] private TextMeshProUGUI upgradeDescriptionText;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Button button;

    [SerializeField] private Sprite tier0Background;
    [SerializeField] private Sprite tier1Background;
    [SerializeField] private Sprite tier2Background;
    [SerializeField] private Sprite tier3Background;
    [SerializeField] private Sprite tier4Background;
    [SerializeField] private Sprite tier5Background;

    public int ID {get;private set;}

    public Action<int> buttonClick;

    public void SetButtonClick(Action<int> callback,int id){
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnButtonClick);
        buttonClick = callback;
        ID = id;
    }

    private void OnButtonClick()
    {
        buttonClick?.Invoke(ID); 
    }

    public void Set(UpgradeData upgradeData)
    {
        icon.sprite = upgradeData.icon;
        upgradeNameText.text = upgradeData.Name;
        upgradeDescriptionText.text = upgradeData.Description;

        switch (upgradeData.upgradeTier)
        {
            case UpgradeTier.Tier0:
                backgroundImage.sprite = tier0Background;
                break;
            case UpgradeTier.Tier1:
                backgroundImage.sprite = tier1Background;
                break;
            case UpgradeTier.Tier2:
                backgroundImage.sprite = tier2Background;
                break;
            case UpgradeTier.Tier3:
                backgroundImage.sprite = tier3Background;
                break;
            case UpgradeTier.Tier4:
                backgroundImage.sprite = tier4Background;
                break;
            case UpgradeTier.Tier5:
                backgroundImage.sprite = tier5Background;
                break;
        }
    }

    public void Clean()
    {
        icon.sprite = null;
        upgradeNameText.text = "";
        upgradeDescriptionText.text = "";
        backgroundImage.sprite = null;
    }
}
