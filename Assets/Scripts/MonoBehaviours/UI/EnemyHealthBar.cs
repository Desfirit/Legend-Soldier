using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    private Vector3 _localScale;

    private CharacterStats _characterStats;
    void Start()
    {
        _localScale = transform.localScale;
        _characterStats = transform.parent.GetComponent<CharacterStats>();
    }

    
    void Update()
    {
        if (_characterStats != null)
        {
            _localScale.x = (float)_characterStats.characterDefinition.currentHealth / (float)_characterStats.characterDefinition.maxHealth;
            transform.localScale = _localScale;
        }

        transform.LookAt(Camera.main.transform);
    }
}
