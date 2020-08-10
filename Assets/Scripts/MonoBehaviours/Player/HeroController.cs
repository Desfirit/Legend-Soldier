using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeroController : MonoBehaviour
{
    [SerializeField]
    private Animator _characterAnimator;

    [SerializeField]
    private float _speedMove = 8f;

    [SerializeField]
    private GameObject _shootSlot;

    [SerializeField]
    private ProjectileMove[] _bulletsType;

    private CharacterStats _characterStats;

    private Rigidbody _rigidbody;

    private GameObject _target;

    private float _lastAttackTime = 0;

    private bool _isCoolDown;

    private int _currentBullet = 0;

    #region Public Methods
    public void Move(Vector3 direction)
    {
        direction *= _speedMove;
        _rigidbody.velocity = direction;
        if (direction != Vector3.zero)
        {
            transform.LookAt(transform.position + direction);
        }
    }

    public void SetBulletType(BulletType bulletType)
    {
        _currentBullet = (int)bulletType;
    }

    public int GetCurrentLevel()
    {
        return _characterStats.characterDefinition.charLevel;
    }

    public int GetCurrentExp()
    {
        return _characterStats.characterDefinition.charExperience;
    }

    public int GetRequierExp()
    {
        return _characterStats.characterDefinition.charLevels[GetCurrentLevel()].experienceRequirement;
    }

    #endregion

    #region Aim and Shoot methods

    void GetTarget()
    {
        var arrayOfTargets = GameObject.FindGameObjectsWithTag("Enemy");
        if (arrayOfTargets.Length != 0)
        {
            float minDistance = _bulletsType[_currentBullet].attackDefinition.range;
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

            if (!_isCoolDown) 
            {
                _characterAnimator.SetBool("Shooting", true);
                ProjectileMove bullet = Instantiate(_bulletsType[_currentBullet], _shootSlot.transform.position, _shootSlot.transform.rotation) as ProjectileMove; 
                _lastAttackTime = Time.time; 
                bullet.transform.parent = null; 
                bullet.playerStats = _characterStats; 
            }
        }
        else
        {
            _characterAnimator.SetBool("Shooting", false);
        }
    }

    void CoolDown() 
    {
        if ((Time.time - _lastAttackTime) > _bulletsType[_currentBullet].attackDefinition.coolDown)
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
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        _characterStats = GetComponent<CharacterStats>();
        _characterStats.characterDefinition.OnHeroLevelUp += GameManager.Instance.OnHeroLevelUp;
        _characterStats.characterDefinition.OnHeroGainedExp += GameManager.Instance.OnHeroGainedExp;
        UIManager.Instance.UpdateUnitFrame(this);
    }

    private void FixedUpdate()
    {
        if (_characterAnimator != null)
        {
            
            _characterAnimator.SetFloat("speed", _rigidbody.velocity.magnitude);
        }
    }

    void Update()
    {
        GetTarget();
        Aiming();
        CoolDown(); 
    }

    #endregion

    #region Event Handlers

    public void OnEnemyDead(int pointValue)
    {
        _characterStats.IncreaseExp(pointValue);
    }

    public void OnWaveEnd(int pointValue)
    {
        _characterStats.IncreaseExp(pointValue);
    }

    #endregion
}
