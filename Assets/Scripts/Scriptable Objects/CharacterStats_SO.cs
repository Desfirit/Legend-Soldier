using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName = "Character/Stats", order = 1)]
public class CharacterStats_SO : ScriptableObject
{
    [System.Serializable]
    public class CharLevel
    {
        public int maxHealth;
        public int baseDamage;
        public float baseResistance;
        public int experienceRequirement;
    }

    public event Action<int> OnHeroLevelUp;
    public event Action<int> OnHeroGainedExp;

    #region Fields
    public bool isHero = false;

    public Item weapon { get; private set; }
    //public ItemPickUp headArmor { get; private set; }
    //public ItemPickUp chestArmor { get; private set; }
    //public ItemPickUp handArmor { get; private set; }
    //public ItemPickUp legArmor { get; private set; }
    //public ItemPickUp footArmor { get; private set; }
    //public ItemPickUp misc1 { get; private set; }
    //public ItemPickUp misc2 { get; private set; }

    public int maxHealth = 0;
    public int currentHealth = 0;

    public int currentGold = 0;
    public int currentGem = 0;

    public int baseDamage = 0;
    public int currentDamage = 0;

    public float baseResistance = 0;
    public float currentResistance = 0f;

    public int charExperience = 0;
    public int charLevel = 0;

    public CharLevel[] charLevels;
    #endregion

    #region Stat Increasers
    public void ApplyHealth(int healthAmount)
    {
        if ((currentHealth + healthAmount) > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += healthAmount;
        }
    }

    public void GiveGold(int goldAmount)
    {
        currentGold += goldAmount;
    }

    public void GiveGem(int gemAmount)
    {
        currentGem += gemAmount;
    }

    public void GiveExp(int exp)
    {
        charExperience += exp;
        if(charLevel < charLevels.Length)
        {
            int levelTarget = charLevels[charLevel].experienceRequirement;
            if(charExperience >= levelTarget)
            {
                SetCharacterLevel(charLevel);
            }
            OnHeroGainedExp?.Invoke(exp);
        }
    }

    public void EquipWeapon(Item weapon, CharacterInventory charInventory, GameObject weaponSlot)
    {
        this.weapon = weapon;
        currentDamage = baseDamage + this.weapon.itemDefinition.itemAmount;

    }

    #endregion

    #region Stat Reducers
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public bool UnEquipWeapon(Item weapon, CharacterInventory charInventory, GameObject weaponSlot)
    {
        bool previousWeaponSame = false;

        if (weapon != null)
        {
            if (this.weapon == weapon)
            {
                previousWeaponSame = true;
            }
            //charInventory.inventoryDisplaySlots[2].sprite = null;
            Destroy(weaponSlot.transform.GetChild(1).gameObject);
            weapon = null;
            currentDamage = baseDamage;
        }

        return previousWeaponSame;
    }

    public void TakeGold(int goldAmount)
    {
        currentGold -= goldAmount;
    }

    public void TakeGem(int gemAmount)
    {
        currentGem -= gemAmount;
    }

    #endregion

    #region Stats Inicialization

    public void OverrideStats(CharacterStats_SO characterStats_SO)
    {

        maxHealth = characterStats_SO.maxHealth;
        currentGem = characterStats_SO.currentGem;
        currentGold = characterStats_SO.currentGold;
        baseDamage = characterStats_SO.baseDamage;
        baseResistance = characterStats_SO.baseResistance;
        charExperience = characterStats_SO.charExperience;
        charLevel = characterStats_SO.charLevel;


    }

    public void RestoreAllStats()
    {
        currentHealth = maxHealth;
        currentDamage = baseDamage;
        currentResistance = baseResistance;
    }

    #endregion

    #region Character Level Up and Death
    private void Death()
    {
        //Debug.Log("You are dead");
    }

    public void SetCharacterLevel(int newLevel)
    {
        charLevel = newLevel + 1;

        maxHealth = charLevels[newLevel].maxHealth;
        currentHealth = charLevels[newLevel].maxHealth;

        baseDamage = charLevels[newLevel].baseDamage;
        currentDamage = charLevels[newLevel].baseDamage;

        baseResistance = charLevels[newLevel].baseResistance;
        currentResistance = charLevels[newLevel].baseResistance;

        if(charLevel > 1)
        {
            OnHeroLevelUp?.Invoke(charLevel);
        }
    }
    #endregion
}
