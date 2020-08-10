using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Skill", menuName = "Skill", order = 1)]
public class SkillDefinition : ScriptableObject
{
    public float coolDown;

    public float timeWorking;

    public BulletType BulletType;
}

public enum BulletType
{
    SimpleBullet,
    FastBullet,
    StoppingBullet


}

