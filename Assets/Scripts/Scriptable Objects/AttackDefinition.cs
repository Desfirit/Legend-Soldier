using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack.asset", menuName = "Attack/BaseAttack", order = 1)]
public class AttackDefinition : ScriptableObject
{
    public float coolDown;

    public float range;
    public float minDamage;
    public float maxDamage;
    public float criticalMultiplier;
    public float criticalChance;

    public Attack CreateAttack(CharacterStats attackerStats, CharacterStats defenderStats)
    {
        float coreDamage = attackerStats.GetDamage();
        coreDamage += Random.Range(minDamage, maxDamage);

        bool isCritical = Random.value < criticalChance;
        if (isCritical)
            coreDamage *= criticalMultiplier;

        if (defenderStats != null)
            coreDamage -= defenderStats.GetResistance();

        return new Attack((int)coreDamage < 0 ? 0 : (int)coreDamage, isCritical);
    }
}
