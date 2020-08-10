using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypeDefinitions { HEALTH, GOLD, GEM, WEAPON, ARMOR, BUFF, EMPTY };
public enum ItemArmorSubType { None, Head, Chest, Hands, Legs, Boots };

[CreateAssetMenu(fileName = "NewItem", menuName = "Spawnable Item/New Pick-up", order = 1)]
public class Item_SO : ScriptableObject
{
    public string itemName = "New Item";
    public ItemTypeDefinitions itemType = ItemTypeDefinitions.HEALTH;
    public ItemArmorSubType itemArmorSubType = ItemArmorSubType.None;
    public int itemAmount = 0;
    public int spawnChanceWeight = 0;

    public Material itemMaterial = null;
    public Sprite itemIcon = null;
    public Rigidbody itemSpawnObject = null;
    public Rigidbody weaponSlotObject = null;

    public bool isEquipped = false;
    public bool isUnique = false;
    public bool isStorable;
}
