using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterStats_SO characterDefinitionTeamplate;
    public CharacterStats_SO characterDefinition;
    public CharacterInventory charInv;
    public GameObject characterWeaponSlot;

    #region Initializations

    private void Awake()
    {
        if (characterDefinitionTeamplate != null)
        {
            characterDefinition = Instantiate(characterDefinitionTeamplate);
        }
    }
    void Start()
    {
        
        if (characterDefinition.isHero)
        {
            characterDefinition.RestoreAllStats();
        }
    }
    #endregion

    #region Constructors
    public CharacterStats()
    {
        charInv = CharacterInventory.Instance;
    }
    #endregion

    #region SaveData
    private void Update()
    {
        //This should be triggered by the game manager during a save point
    }
    #endregion

    #region Stat Increasers
    public void ApplyHealth(int healthAmount)
    {
        characterDefinition.ApplyHealth(healthAmount);
    }

    public void GiveGold(int goldAmount)
    {
        characterDefinition.GiveGold(goldAmount);
    }

    public void GiveGem(int gemAmount)
    {
        characterDefinition.GiveGem(gemAmount);
    }

    public void IncreaseExp(int exp)
    {
        characterDefinition.GiveExp(exp);
    }
    #endregion

    #region Stat Reducers
    public void TakeDamage(int amount)
    {
        characterDefinition.TakeDamage(amount);
    }

    public void TakeGold(int goldAmount)
    {
        characterDefinition.TakeGold(goldAmount);
    }

    public void TakeGem(int gemAmount)
    {
        characterDefinition.TakeGem(gemAmount);
    }

    #endregion

    #region Weapon and Armor Change
    public void ChangeWeapon(Item weaponPickUp)
    {
        characterDefinition.EquipWeapon(weaponPickUp, charInv, characterWeaponSlot);
    }

    public void ChangeArmor(Item armorPickUp)
    {

    }
    #endregion

    #region Reporters
    public int GetHealth()
    {
        return characterDefinition.currentHealth;
    }

    public Item GetCurrentWeapon()
    {
        return characterDefinition.weapon;
    }

    public int GetDamage()
    {
        return characterDefinition.currentDamage;
    }

    public float GetResistance()
    {
        return characterDefinition.currentResistance;
    }
    #endregion

    #region Stat Initializers 

    public void SetInitialHealth(int health)
    {
        characterDefinition.maxHealth = health;
        characterDefinition.currentHealth= health;
    }

    public void SetInitialResistance(int resistance)
    {
        characterDefinition.baseResistance = resistance;
        characterDefinition.currentResistance = resistance;
    }

    public void SetInitialDamage(int damage)
    {
        characterDefinition.baseDamage = damage;
        characterDefinition.currentDamage = damage;
    }

    #endregion
}
