using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Wave", menuName = "Enemy Wave")]
public class EnemyWave : ScriptableObject
{ 
    public int enemyHealth;
    public int enemyDamage;
    public int enemyResistance;
    public int numberOfNPC;
    public int pointsPerKill;
    public int waveValue;

}
