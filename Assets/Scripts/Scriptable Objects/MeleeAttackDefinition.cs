using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MeleeAttack", menuName = "Attack/MeleeAttack", order = 3)]
public class MeleeAttackDefinition : AttackDefinition
{
    public void ExecuteAttack(GameObject attacker, GameObject defender)
    {
        var attackables = defender.GetComponents<IAttackable>();

        var attack = CreateAttack(attacker.GetComponent<CharacterStats>(), defender.GetComponent<CharacterStats>());

        if (Vector3.Distance(attacker.transform.position, defender.transform.position) < range) //Проверка на дистацию атаки
        {
            foreach (var attackable in attackables)
                attackable.OnAttack(attacker, attack);
        }
    }
}
