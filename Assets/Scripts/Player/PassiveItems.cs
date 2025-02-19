using System;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItems : MonoBehaviour
{
    [SerializeField]List<Item> items;

    PlayerHealth playerHealth;
    PlayerMovement playerMovement;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Start()
    {
        
    }

    public void Equip(Item itemToEquip)
    {
        if(items == null)
        {
            items = new List<Item>();
        }

        Item newItemInstance = new Item();
        newItemInstance.Init(itemToEquip.Name);
        newItemInstance.stats.Sum(itemToEquip.stats);

        items.Add(newItemInstance);
        newItemInstance.Equip(playerHealth, playerMovement);
    }

    internal void UpgradeItem(UpgradeData upgradeData)
    {
        Item itemToUpgrade = items.Find(id => id.Name == upgradeData.item.Name);
        itemToUpgrade.UnEquip(playerHealth, playerMovement);
        itemToUpgrade.stats.Sum(upgradeData.itemStats);
        itemToUpgrade.Equip(playerHealth, playerMovement);
    }
}
