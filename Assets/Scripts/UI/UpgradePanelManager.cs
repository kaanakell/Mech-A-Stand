using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private List<UpgradeButton> upgradeButtons;
    private PauseManager pauseManager;
    private PlayerLevel playerLevel;

    private List<UpgradeData> _upgradeDatas;
    private Dictionary<UpgradeData,int> _chosenUpgrades = new Dictionary<UpgradeData,int>();

    public Action<Dictionary<UpgradeData,int>> onUpgradeChose;

    private void Awake()
    {
        pauseManager = GetComponent<PauseManager>();
        
    }

    void Start()
    {
        playerLevel = GameManager.instance.playerTransform.GetComponent<PlayerLevel>();
        SetButtonClicks();
        HideButtons();
    }
    private void SetButtonClicks(){
        for (int i = 0;i < upgradeButtons.Count; i++)
{{
    UpgradeButton upgradeButton = upgradeButtons[i];
    upgradeButton.SetButtonClick(OnButtonClick,i);
}

}    }

    private void OnButtonClick(int obj)
    {
       Upgrade(obj);
       var upgradeData = _upgradeDatas[obj];
       if(_chosenUpgrades.ContainsKey(upgradeData)){
        _chosenUpgrades[upgradeData]++;
       }
       else if(CheckUpgradeItemIsSame(upgradeData, out var sameData)){
        _chosenUpgrades[sameData]++;
       }
       else{
        _chosenUpgrades.Add(upgradeData, 1);
       }
       onUpgradeChose?.Invoke(_chosenUpgrades);
       FMODUnity.RuntimeManager.PlayOneShot("event:/UI_YES");
    }
     private bool CheckUpgradeItemIsSame(UpgradeData data,out UpgradeData sameData){
        sameData = default;

        if(_chosenUpgrades.Count <= 0)
        return false;

        foreach (var upgrade in _chosenUpgrades){
            bool hasItem = data.item != null;
            bool hasWeapon = data.weaponData != null;
            if(hasItem){
                if(upgrade.Key.item == data.item){
                sameData = upgrade.Key;
                return true;
            }
            }
            if(hasWeapon){
                if(upgrade.Key.weaponData == data.weaponData){
                sameData = upgrade.Key;
                return true;
            }
            }
            
        }
        return false;
     }
     
    public void OpenPanel(List<UpgradeData> upgradeDatas)
    {
        Clean();
        FMODUnity.RuntimeManager.PlayOneShot("event:/Coin_Get");
        pauseManager.PauseGame();
        panel.SetActive(true);
        _upgradeDatas = upgradeDatas;

        for (int i = 0; i < upgradeDatas.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtons[i].Set(upgradeDatas[i]);
        }
    }

    public void Clean()
    {
        foreach (var button in upgradeButtons)
        {
            button.Clean();
        }
    }

    public void Upgrade(int pressedButtonID)
    {
        playerLevel.Upgrade(pressedButtonID);
        ClosePanel();
    }

    public void ClosePanel()
    {
        HideButtons();
        pauseManager.UnPauseGame();
        panel.SetActive(false);
    }

    private void HideButtons()
    {
        foreach (var button in upgradeButtons)
        {
            button.gameObject.SetActive(false);
        }
    }
}
