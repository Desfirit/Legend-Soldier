using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject _text;
    void Start()
    {
        GetComponent<CharacterStats>().characterDefinition.OnHeroLevelUp += OnHeroLevelUp;
    }

    private void OnHeroLevelUp(int level)
    {
        Instantiate(_text, transform.position, Quaternion.identity);
    }
}
