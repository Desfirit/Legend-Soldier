using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour
{
  [SerializeField]
  private List<GameObject> _vfx = new List<GameObject>();

  [SerializeField]
  private Transform _firePoint;

  private GameObject _effectToSpawn;
    void Start()
    {
    _effectToSpawn = _vfx[0];
    }

  private void Update()
  {
    //if (Input.GetMouseButtonDown(0))
    //{
    //  GameObject vfx;
    //  vfx = Instantiate(_effectToSpawn, _firePoint.position, Quaternion.identity);

    //}
  }
  public void Shoot(Quaternion rotation)
    {
    GameObject vfx;
      if(_firePoint != null)
      {
        vfx = Instantiate(_effectToSpawn, _firePoint.position, rotation);
      }
      else
      {
        Debug.Log("Нет FirePoint");
      }
    }
    
}
