using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructedDestoryTheObjecct : MonoBehaviour, IDestructable
{
    public void OnDestruction(GameObject destroyer)
    {
        Destroy(gameObject);
    }
}
