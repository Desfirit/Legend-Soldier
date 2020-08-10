using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;
using System;

public class GameManager : Manager<GameManager>
{

    [System.Serializable] public class EventGameState : UnityEvent<GameState, GameState> { }

    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSED,
        POSTGAME,
    }

    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        private set { _currentGameState = value; }
    }

    public GameObject[] SystemPrefabs;
    public EventGameState OnGameStateChanged;

    [SerializeField]
    private CharacterStats_SO _characterDefinition;

    

    List<GameObject> _instancedSystemPrefabs;

    GameState _currentGameState = GameState.PREGAME;

    SessionStats _currentSession;
    public SessionStats CurrentSession
    {
        get 
        {
            return _currentSession;
        }
    }

    HeroController _hero;
    HeroController heroController
    {
        get 
        {
            if(_hero == null)
            {
                _hero = FindObjectOfType<HeroController>();
            }
            return _hero;
        }
    }

    string _currentLevelName;
    string CurrentLevelName
    {
        get {
            return _currentLevelName;
        }

        set {
            _levelNumber = SceneManager.GetSceneByName(value).buildIndex;
            _currentLevelName = value;
        }
    }

    private int _levelNumber;


    #region Public Methods

    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to load level " + levelName);
            return;
        }

        ao.completed += OnLoadOperationComplete;

        CurrentLevelName = levelName;
    }

    public void TogglePause()
    {
        UpdateState(_currentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);
    }

    public void RestartLevel()
    {
        LoadLevel(_currentLevelName);
        InitSession(); //Возможно ошибка
    }

    public void LoadMainMenu()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        ao.completed += OnUnloadOperationComplete;
        CurrentLevelName = "MainMenu";
    }

    public void QuitGame()
    {

        Debug.Log("[GameManager] Quit Game.");

        SaveCharacterStats();
        Application.Quit();
    }

    public void OnHeroLevelUp(int newLevel)
    {
        if(heroController != null)
            UIManager.Instance.UpdateUnitFrame(heroController);

        _currentSession.HighestLevel++;
    }

    public void OnWaveComplete(int wave)
    {
        UIManager.Instance.PlayNextWave();
        _currentSession.WavesCompleted++;
    }

    public void OnOutOfWaves()
    {
        _currentSession.WavesCompleted++;
        UIManager.Instance.PlayEndWaves();
    }

    public void OnEndFinalWave()
    {
        EstimateSession(EndGameState.Win);
        _currentSession.WavesCompleted++;
        UIManager.Instance.PlayYouWin();
        _characterDefinition.OverrideStats(heroController.GetComponent<CharacterStats>().characterDefinition);
        SaveCharacterStats();
    }

    public void OnHeroWin()
    {
        UpdateState(GameState.POSTGAME);
        GiveReward();
        SaveCharacterStats();
    }

    public void OnHeroDied()
    {
        EstimateSession(EndGameState.Loss);
        StartCoroutine("EndGame");
    }

    public void OnStartBattle()
    {
        UIManager.Instance.PlayStartBattle();
    }
    
    public void OnHeroGainedExp(int heroExp)
    {
        if(heroController != null)
            UIManager.Instance.UpdateUnitFrame(heroController);

        _currentSession.ExperienceGained += heroExp;
    }

    public void OnMobDied(int points)
    {
        _currentSession.EnemyKilled++;
    }

    public IEnumerator EndGame()
    {
        UIManager.Instance.PlayGameOver();
        yield return new WaitForSeconds(3f);

        UpdateState(GameState.POSTGAME);
    }

    #endregion

    #region Private Methods

    void GiveReward()
    {
        _characterDefinition.currentGold += _currentSession.EnemyKilled * _levelNumber;
    }

    void OnLoadOperationComplete(AsyncOperation ao)
    {
        UpdateState(GameState.RUNNING);
        InitSession();
    }

    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        UpdateState(GameState.PREGAME);
    }

    void UpdateState(GameState state)
    {
        GameState previousGameState = _currentGameState;
        _currentGameState = state;

        switch (CurrentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1.0f;
                break;

            case GameState.RUNNING:
                Time.timeScale = 1.0f;
                break;

            case GameState.PAUSED:
                Time.timeScale = 0.0f;
                break;

            case GameState.POSTGAME:
                Time.timeScale = 0.0f;
                break;

            default:
                break;
        }

        OnGameStateChanged.Invoke(_currentGameState, previousGameState);
    }

    void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        for (int i = 0; i < SystemPrefabs.Length; ++i)
        {
            prefabInstance = Instantiate(SystemPrefabs[i]);
            DontDestroyOnLoad(prefabInstance);
            _instancedSystemPrefabs.Add(prefabInstance);
        }
    }

    #endregion

    #region MonoBehaviours

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        _instancedSystemPrefabs = new List<GameObject>();

        InstantiateSystemPrefabs();

        LoadCharacterStats();

        OnGameStateChanged.Invoke(GameState.PREGAME, _currentGameState);
    }

    void Update()
    {

    }

    #endregion

    #region Stats

    private void LoadCharacterStats()
    {
        StatsManager.SaveFilePath = Path.Combine(Application.persistentDataPath, "saveGame.json");
        StatsManager.LoadCharacterStats(_characterDefinition);
    }

    private void InitSession()
    {
        _currentSession = new SessionStats();
    }

    public void EstimateSession(EndGameState endGameState)
    {
        _currentSession.SessionDate = DateTime.Now.ToString();
        
        _currentSession.WinOrLoss = endGameState;
        
    }

    public void SaveCharacterStats()
    {
        
        StatsManager.SaveCharacterStats(_characterDefinition);
    }

    #endregion
}
