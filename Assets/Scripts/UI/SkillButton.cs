using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SkillButton : MonoBehaviour
{
    public event Action OnFinishUsing;

    [SerializeField]
    private Image _skillBar;
    public void StartCooldown(float time)
    {
        StopAllCoroutines();
        StartCoroutine("StartCooldownCoroutine", time);
    }

   
    public void StartUsing(float time)
    {
        StopAllCoroutines();
        GetComponent<Button>().enabled = false;
        StartCoroutine("StartUsingCoroutine", time);
    }

    private IEnumerator StartCooldownCoroutine(float time)
    {
        float startTime = Time.time;
        while ((Time.time - startTime) < time)
        {
            _skillBar.fillAmount = (Time.time - startTime) / time;
            yield return new WaitForEndOfFrame();
        }
        GetComponent<Button>().enabled = true;
    }

    private IEnumerator StartUsingCoroutine(float time)
    {
        float startTime = Time.time;
        while ((Time.time - startTime) < time)
        {
            _skillBar.fillAmount =  1 - ((Time.time - startTime) / time);
            yield return new WaitForEndOfFrame();
        }
        OnFinishUsing?.Invoke();
    }

    private void AccessButton()
    {
        GetComponent<Button>().enabled = GetComponent<Button>().enabled ? false : true;
    }
}
