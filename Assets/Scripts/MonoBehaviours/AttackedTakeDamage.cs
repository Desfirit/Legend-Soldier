using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedTakeDamage : MonoBehaviour, IAttackable
{
    private CharacterStats _characterStats;

    private void Awake()
    {
        _characterStats = GetComponent<CharacterStats>();
    }
    public void OnAttack(GameObject attacker, Attack attack)
    {
        _characterStats.TakeDamage(attack.Damage);
        if(_characterStats.GetHealth() < 0)
        {
            var destructables = GetComponents(typeof(IDestructable));
            foreach(IDestructable d in destructables)
            {
                d.OnDestruction(attacker);
            }
        }
    }
}
