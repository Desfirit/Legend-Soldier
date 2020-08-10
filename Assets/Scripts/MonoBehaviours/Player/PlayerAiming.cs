using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    [SerializeField]
    private float _maxAimDistance;

    [SerializeField]
    private GameObject _shootSlot; //Потом в Hero перенести

    [SerializeField]
    private ProjectileMove _bullet; //Потом в Hero перенести

    private GameObject _target;

    private CharacterStats _characterStats; //Потом в Hero перенести

    private float _lastAttackTime = 0; //Потом в Hero перенести
    private bool _isCoolDown; //Потом в Hero перенести

    private Animator _animator;

    #region Methods
    void GetTarget()
    {
        var arrayOfTargets = GameObject.FindGameObjectsWithTag("Enemy");
        if (arrayOfTargets.Length != 0)
        {
            float minDistance = _maxAimDistance;
            int minIndex = -1;
            for (int i = 0; i < arrayOfTargets.Length; i++)
            {
                if (GetDistance(arrayOfTargets[i]) < minDistance)
                {
                    minDistance = GetDistance(arrayOfTargets[i]);
                    minIndex = i;
                }
            }
            if (minIndex < 0)
            {
                _target = null;
            }
            else
            {
                _target = arrayOfTargets[minIndex];
            }
        }
        else
        {
            _target = null;
        }
    }

    float GetDistance(GameObject target) => Vector3.Distance(transform.position, target.transform.position);

    void Aiming()
    {
        if (_target != null)
        {
            var direction = _target.transform.position;
            direction.y = transform.position.y;
            transform.LookAt(direction);

            //Потом в Hero перенести
            if (!_isCoolDown) //Потом в Hero перенести
            {
                _animator.SetBool("Shooting", true);
                ProjectileMove bullet = Instantiate(_bullet, _shootSlot.transform.position, _shootSlot.transform.rotation) as ProjectileMove; //Потом в Hero перенести
                _lastAttackTime = Time.time; //Потом в Hero перенести
                bullet.transform.parent = null; //Потом в Hero перенести
                bullet.playerStats = GetComponent<CharacterStats>(); //Потом в Hero перенести
            }
        }
        else
        {
            _animator.SetBool("Shooting", false);
        }
    }

    void CoolDown() //Потом в Hero перенести
    {
        if ((Time.time - _lastAttackTime) > _bullet.attackDefinition.coolDown)
        {
            _isCoolDown = false;
        }
        else
        {
            _isCoolDown = true;
        }
    }

    #endregion

    #region MonoBehaviours
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        GetTarget();
        Aiming();
        CoolDown();  //Потом в Hero перенести
    }

    #endregion
}
