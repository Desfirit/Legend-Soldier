using UnityEngine;

public class CameraController : MonoBehaviour
{
  [SerializeField]
  private GameObject _player;

  [SerializeField]
  private float _distanceZ;

  [SerializeField]
  private float _distanceX;

  [SerializeField]
  private float _hight;

  [SerializeField]
  [Range(0f, 20f)]
  private float _interpolate = 1;
  void Start()
  {

  }


  void Update()
  {
    if(_player != null)
        Moving();
  }

  void Moving()
  {
    transform.position = Vector3.Lerp(transform.position, _player.transform.position + new Vector3(_distanceX, _hight, -_distanceZ), _interpolate * Time.deltaTime);
  }
}
