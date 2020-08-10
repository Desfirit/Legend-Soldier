using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{

    public AttackDefinition attackDefinition;

    [HideInInspector]
    public CharacterStats playerStats;

    [SerializeField]
  private float _speed;

  [SerializeField]
  private GameObject _muzzlePrefab;

  [SerializeField]
  private GameObject _hitPrefab;


  private void Start()
  {
    if(_muzzlePrefab != null)
    {
      var muzzlePrefab = Instantiate(_muzzlePrefab, transform.position, Quaternion.identity);
      muzzlePrefab.transform.forward = gameObject.transform.forward;

      var psMuzzle = muzzlePrefab.GetComponent<ParticleSystem>();
      if(psMuzzle != null)
      {
        Destroy(muzzlePrefab, psMuzzle.main.duration);
      }
      else
      {
        var psChild = muzzlePrefab.transform.GetChild(0).GetComponent<ParticleSystem>();
        Destroy(muzzlePrefab, psChild.main.duration);
      }
    }
    Destroying();
  }

  void Destroying()
  {
    Destroy(gameObject, 5f);
  }

    private void OnTriggerEnter(Collider collision)
  {
    Vector3 pos = transform.position;

    if(_hitPrefab != null)
    {
      var hitPrefab = Instantiate(_hitPrefab, pos, Quaternion.identity);
            var psHit = hitPrefab.GetComponent<ParticleSystem>();
            if(psHit != null)
            {
                Destroy(hitPrefab, psHit.main.duration);

            }
            else
            {
                var psChild = hitPrefab.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitPrefab, psChild.main.duration);
            }
    }
    if(collision.gameObject.tag == "Enemy")
        {
            var attackables = collision.gameObject.GetComponents<IAttackable>();
            if (attackables.Length > 0) //Проверка на атакуемость объекта
            {
                var attack = attackDefinition.CreateAttack(playerStats, gameObject.GetComponent<CharacterStats>());
                foreach (var attackable in attackables)
                    attackable.OnAttack(gameObject, attack);
            }
            
        }
        Destroy(gameObject);
  }
  void Update()
    {
    transform.position += transform.forward * (_speed * Time.deltaTime);
    }
}
