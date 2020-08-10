using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Manager<UIManager>
{
    [SerializeField]
    MainMenu _mainMenu;

    [SerializeField]
    GameObject _sessionResultMenu;

    [SerializeField]
    GameObject _pauseMenu;

    [SerializeField]
    GameObject _unitFrame;

    [SerializeField]
    Text _levelAmount;

    [SerializeField]
    Image _experienceBar;

    [SerializeField]
    GameObject _nextWaveText;

    [SerializeField]
    GameObject _youWinText;

    [SerializeField]
    GameObject _gameOverText;

    [SerializeField]
    GameObject _endWavesText;

    [SerializeField]
    GameObject _startBattleText;
    public void UpdateUnitFrame(HeroController hero)
    {
        _levelAmount.text = Convert.ToString(hero.GetCurrentLevel());
        _experienceBar.fillAmount = (float)hero.GetCurrentExp() / (float)hero.GetRequierExp();
    }

    public void PlayNextWave()
    {
        _nextWaveText.SetActive(true);
    }

    public void PlayYouWin()
    {
        _youWinText.SetActive(true);
    }

    public void PlayGameOver()
    {
        _gameOverText.SetActive(true);
    }

    public void PlayEndWaves()
    {
        _endWavesText.SetActive(true);
    }

    public void PlayStartBattle()
    {
        _startBattleText.SetActive(true);
    }

    void HandleGameStateChanged(GameManager.GameState currentGameState, GameManager.GameState previousGameState)
    {
        if(currentGameState == GameManager.GameState.PREGAME)
        {
            _mainMenu.gameObject.SetActive(true);
            _pauseMenu.SetActive(false);
            _unitFrame.SetActive(false);
            _sessionResultMenu.SetActive(false);
        }

        if(currentGameState == GameManager.GameState.POSTGAME)
        {
            _mainMenu.gameObject.SetActive(false);
            _pauseMenu.SetActive(false);
            _unitFrame.SetActive(true);
            _sessionResultMenu.SetActive(true);
        }

        if (currentGameState == GameManager.GameState.RUNNING && previousGameState == GameManager.GameState.PREGAME)
        {
            _sessionResultMenu.SetActive(false);
            _mainMenu.gameObject.SetActive(false);
            _pauseMenu.SetActive(true);
            _unitFrame.SetActive(true);
        }

        if (_mainMenu.isActiveAndEnabled)
        {
            _mainMenu.UpdateUIDefinition();
        }
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    
}
