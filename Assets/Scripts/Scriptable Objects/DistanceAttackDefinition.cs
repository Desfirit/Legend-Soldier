using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Distance Attack", menuName = "Attack/DistanceAttack", order = 2)]
public class DistanceAttackDefinition : AttackDefinition
{
    public Projectile projectile;
    public float projectileSpeed;

    public void Shoot(GameObject shooter, Vector3 fireSpot, Vector3 target, int layer)
    {
        var projectil = Instantiate(projectile, fireSpot, Quaternion.identity);
        projectil.Fire(shooter, target, range, projectileSpeed);

        projectil.gameObject.layer = layer;

        projectil.OnProjectileCollided += ProjectileCollidedHandler;
    }

    private void ProjectileCollidedHandler(GameObject shooter, GameObject target)
    {
        if (shooter == null || target == null)
            return;

        var shooterStats = shooter.GetComponent<CharacterStats>();
        var targetStats = target.GetComponent<CharacterStats>();

        if(shooterStats != null && targetStats !=null)
        {
            var attack = CreateAttack(shooterStats, targetStats);

            var attackables = target.GetComponents<IAttackable>();

            foreach(var a in attackables)
            {
                a.OnAttack(shooter, attack);
            }
        }
    }
}
