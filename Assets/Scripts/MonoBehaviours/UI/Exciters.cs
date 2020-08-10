using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exciters : MonoBehaviour
{
    
    private RectTransform _rectTransform;
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        //_rectTransform.localScale = Vector3.one;
    }

    public void Stop()
    {
        gameObject.SetActive(false);
    }

}
