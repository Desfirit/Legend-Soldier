using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroHealthBar : MonoBehaviour
{
    [SerializeField]
    private CharacterStats _characterStats;

    [SerializeField]
    private Image _healthBar;
    void Start()
    {
        
    }
    void Update()
    {
        if(_characterStats!=null)
            _healthBar.fillAmount = (float)_characterStats.characterDefinition.currentHealth / (float)_characterStats.characterDefinition.maxHealth;
        
        
    }

    private void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
