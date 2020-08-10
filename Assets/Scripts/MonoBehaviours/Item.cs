using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Item_SO itemDefinition;

    public CharacterStats charStats;
    CharacterInventory charInventory;

    GameObject foundStats;

    #region Constructors
    public Item()
    {
        charInventory = CharacterInventory.Instance;
    }
    #endregion

    void Start()
    {
        foundStats = GameObject.FindGameObjectWithTag("Player");
        charStats = foundStats.GetComponent<CharacterStats>();
    }

    void StoreItem()
    {
        charInventory.StoreItem(this);
    }

    public void UseItem()
    {
        switch (itemDefinition.itemType)
        {
            case ItemTypeDefinitions.HEALTH:
                charStats.ApplyHealth(itemDefinition.itemAmount);
                Debug.Log(charStats.GetHealth());
                break;
            case ItemTypeDefinitions.GOLD:
                charStats.GiveGold(itemDefinition.itemAmount);
                break;
            case ItemTypeDefinitions.GEM:
                charStats.GiveGem(itemDefinition.itemAmount);
                break;
            case ItemTypeDefinitions.WEAPON:
                //charStats.ChangeWeapon(this);
                //break;
            case ItemTypeDefinitions.ARMOR:
                // TODO: Call change armor
                //charStats.ChangeArmor();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (itemDefinition.isStorable)
            {
                StoreItem();
            }
            else
            {
                UseItem();
            }
        }
    }
}

