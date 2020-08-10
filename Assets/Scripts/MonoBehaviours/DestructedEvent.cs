using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructedEvent : MonoBehaviour, IDestructable
{
    public event Action IDied;

    void Start()
    {
       IDied += GameManager.Instance.OnHeroDied;
    }

    public void OnDestruction(GameObject destroyer)
    {
        IDied?.Invoke();
    }
}
