using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class BattleTrigger : MonoBehaviour
{
    public UnityEvent OnBattleStart;

    private void Start()
    {
        OnBattleStart.AddListener(GameManager.Instance.OnStartBattle);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(OnBattleStart != null)
                OnBattleStart.Invoke();
        }
    }
}
