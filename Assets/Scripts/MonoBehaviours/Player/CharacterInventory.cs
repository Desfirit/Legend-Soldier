using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : Singleton<CharacterInventory>
{
    private List<Item> _items;

    private Item _weapon;

    private CharacterStats _player;


    public void StoreItem(Item item)
    {
        _items.Add(item);
    }

    public void IdentifyPlayer(CharacterStats player)
    {
        _player = player;
    }

    public void StartEquip()
    {
        _player.ChangeWeapon(_weapon);
    }

    #region MonoBehaviours

    private void Awake()
    {
        _items = new List<Item>();
    }

    #endregion


}
