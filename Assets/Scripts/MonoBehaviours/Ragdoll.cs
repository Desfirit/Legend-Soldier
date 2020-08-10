using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbodyCore;

    [SerializeField]
    private float _timeDestroy;

    private void Start()
    {
        Destroy(gameObject, _timeDestroy);
    }

    public void ApplyForce(Vector3 force)
    {
        _rigidbodyCore.AddForce(force);
    }

}
