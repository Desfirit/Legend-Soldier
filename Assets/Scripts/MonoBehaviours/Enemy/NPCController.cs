using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCController : MonoBehaviour
{
    

    [SerializeField]
    private AttackDefinition _attackDefinition;

    [SerializeField]
    private Transform _fireSpot;

    public event Action OnEnemyDead;

    private float _speed, _agentSpeed;
    private Transform _player;

    private Animator _animator;

    private NavMeshAgent _agent;

    private bool _isCoolDown;

    private float _lastAttackTime = 0;

    private bool _playerIsAlive = true;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agentSpeed = _agent.speed;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _animator = GetComponent<Animator>();
        _player.GetComponent<DestructedEvent>().IDied += PlayerDied;
    }

    private void PlayerDied()
    {
        _playerIsAlive = false;
    }

    private void OnEnable()
    {
        InvokeRepeating("Tick", 0f, 0.25f);
    }

    void Update()
    {
        _speed = Mathf.Lerp(_speed, _agent.velocity.magnitude, Time.deltaTime * 10);
        _animator.SetFloat("Speed", _speed);

        if ((Time.time - _lastAttackTime) > _attackDefinition.coolDown)
        {
            _isCoolDown = false;
        }
        else
        {
            _isCoolDown = true;
        }
        if (_playerIsAlive)
        {
            if (_agent.remainingDistance <= _attackDefinition.range) // Начало атаки
            {
                
                if (!_isCoolDown) //Проверка на кулдаун
                {
                    Attack();
                }
            }
        }
    }

    void Attack()
    {
        _animator.SetTrigger("Attack");
        _lastAttackTime = Time.time;

        if(_attackDefinition is MeleeAttackDefinition)
        {
            ((MeleeAttackDefinition)_attackDefinition).ExecuteAttack(gameObject, _player.gameObject);
        }
        else if(_attackDefinition is DistanceAttackDefinition)
        {
            ((DistanceAttackDefinition)_attackDefinition).Shoot(gameObject, _fireSpot.position, _player.position, LayerMask.NameToLayer("EnemyProjectiles"));
        }
    }

    void Tick()
    {
        if (_player != null)
        {
            if (_agent.isOnNavMesh)
            {
                _agent.speed = _agentSpeed;
                _agent.destination = _player.position;
            }
        }
    }

    private void OnDestroy()
    {
        OnEnemyDead?.Invoke();
    }
}
