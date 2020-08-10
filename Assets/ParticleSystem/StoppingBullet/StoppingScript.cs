using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StoppingScript : MonoBehaviour
{
    [SerializeField]
    private float _explosionForce;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IAttackable>() != null)
        {
            
        }
    }
}
