using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageInfo : MonoBehaviour, IAttackable
{
    public GameObject text;
    
    public void OnAttack(GameObject attacker, Attack attack)
    {
        var text1 = Instantiate(text, transform.position, Quaternion.identity);
        text1.GetComponent<TextContr>().SetText(Convert.ToString(attack.Damage));

    }
}
