using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextContr : MonoBehaviour
{
    [SerializeField]
    private float _timeDestroy;

    TextMesh textMesh;
    private void Awake()
    {
        textMesh = GetComponent<TextMesh>();
    }
    void Start()
    {
        
        Destroy(gameObject, _timeDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 6, Space.World);
        transform.rotation = Camera.main.transform.rotation;
    }

    public void SetText(string damage)
    {
        textMesh.text = damage;
    }
}
