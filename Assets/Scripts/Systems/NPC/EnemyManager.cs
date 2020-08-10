using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _enemies;

    [SerializeField]
    private GameObject _boss;

    [SerializeField]
    private SpawnPoint _bossSpawnPoint;

    [SerializeField]
    private EnemyWave[] _enemyWaves;

    [SerializeField]
    private bool _isFinal = false;

    [SerializeField]
    private GameObject[] _smokeWalls;

    [SerializeField]
    private float _pingBetweenWaves = 3f;

    [SerializeField]
    private float _pingFinalWaves = 13f;


    private Action<int> OnEnemyKilled;
    private Action<int> OnWaveComplete;
    private Action OnOutOfWaves;
    private Action OnEndFinalWave;

    [SerializeField]
    private SpawnPoint[] _spawnPoints;

    private int _activeMobs;
    private int _currentWaveIndex = 0;
    void Start()
    {
        HeroController player = FindObjectOfType<HeroController>();
        if (player != null)
        {
            OnEnemyKilled += player.OnEnemyDead;
            OnEnemyKilled += GameManager.Instance.OnMobDied;

            OnWaveComplete += player.OnWaveEnd;
            OnWaveComplete += GameManager.Instance.OnWaveComplete;

            OnOutOfWaves += GameManager.Instance.OnOutOfWaves;

            OnEndFinalWave += GameManager.Instance.OnEndFinalWave;

        }
    } 
    
    public void SpawnWave()
    {
        if(_enemyWaves.Length - 1 < _currentWaveIndex)
        {
            if (_isFinal)
            {
                OnEndFinalWave?.Invoke();
                CancelInvoke();
            }
            else
            {
                OnOutOfWaves?.Invoke();
            }
            foreach (var wall in _smokeWalls)
            {
                wall.SetActive(false);
            }
            
            return;
        }

        if (_isFinal)
        {
            _activeMobs += _enemyWaves[_currentWaveIndex].numberOfNPC;
        }
        else
        {
            _activeMobs = _enemyWaves[_currentWaveIndex].numberOfNPC;
        }


        for(int i = 0; i < _enemyWaves[_currentWaveIndex].numberOfNPC; i++)
        {
            SpawnPoint spawnPoint = SelectRandomSpawnPoint();
            GameObject enemy = Instantiate(SelectRandomEnemy(), spawnPoint.transform.position, Quaternion.identity);

            CharacterStats enemyStats = enemy.GetComponent<CharacterStats>();
            EnemyWave wave = _enemyWaves[_currentWaveIndex];

            enemyStats.SetInitialHealth(wave.enemyHealth);
            enemyStats.SetInitialResistance(wave.enemyResistance);
            enemyStats.SetInitialDamage(wave.enemyDamage);

            NPCController npcController = enemy.GetComponent<NPCController>();
            if(npcController != null)
            {
                npcController.OnEnemyDead += OnEnemyDead;
            }
        }
    }

    void SpawnBoss()
    {
        _activeMobs++;
        GameObject boss = Instantiate(_boss, _bossSpawnPoint.transform.position, Quaternion.identity);
        NPCController npcController = boss.GetComponent<NPCController>();
        if(npcController != null)
        {
            npcController.OnEnemyDead += OnEnemyDead;
        }
    }

    public void OnEnemyDead()
    {
        EnemyWave currentWave = _enemyWaves[_currentWaveIndex];

        _activeMobs--;
        OnEnemyKilled?.Invoke(currentWave.pointsPerKill);
        if(_activeMobs <= 0)
        {
            _currentWaveIndex++;
            if (_currentWaveIndex <= _enemyWaves.Length - 1)
                OnWaveComplete?.Invoke(currentWave.waveValue);
            Invoke("SpawnWave", _pingBetweenWaves);
        }
    }

    public void StartBattle()
    {
        foreach(var wall in _smokeWalls)
        {
            wall.SetActive(true);
        }
        if (_isFinal)
        {
            SpawnBoss();
            InvokeRepeating("SpawnWave", _pingBetweenWaves, _pingFinalWaves);
        }
        else
        {
            Invoke("SpawnWave", _pingBetweenWaves);
        }
    }

    private GameObject SelectRandomEnemy()
    {
        return _enemies[UnityEngine.Random.Range(0, _enemies.Length)];
    }

    private SpawnPoint SelectRandomSpawnPoint()
    {
        return _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)];
    }

    
}
