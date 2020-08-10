using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructedRagdoll : MonoBehaviour, IDestructable
{
    [SerializeField]
    private Ragdoll _ragdoll;

    [SerializeField]
    private float _force;
    [SerializeField]
    private float _lift;

    public void OnDestruction(GameObject destroyer)
    {
        var ragdoll = Instantiate(_ragdoll, transform.position, transform.rotation);

        var vectorFromDestroyer = transform.position - destroyer.transform.position;
        vectorFromDestroyer.Normalize();
        vectorFromDestroyer.y += _lift;

        ragdoll.ApplyForce(vectorFromDestroyer * _force);
    }
}
