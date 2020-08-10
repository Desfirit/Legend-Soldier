using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SessionMenu : MonoBehaviour
{
    [SerializeField]
    private Text _winOrLossText;

    [SerializeField]
    private Text _sessionDataText;

    [SerializeField]
    private Text _highestlevelText;

    [SerializeField]
    private Text _experienceGainedText;

    [SerializeField]
    private Text _enemyKilledText;

    [SerializeField]
    private Text _wavesCompletedText;

    [SerializeField]
    private Button _returnMainMenuButton;

    [SerializeField]
    private Button _restartButon;

    public void DisplayStatsField()
    {
        SessionStats sessionStats = GameManager.Instance.CurrentSession;

        _winOrLossText.text = "You " + sessionStats.WinOrLoss.ToString();
        _sessionDataText.text = sessionStats.SessionDate;
        _highestlevelText.text = sessionStats.HighestLevel.ToString();
        _experienceGainedText.text = sessionStats.ExperienceGained.ToString();
        _enemyKilledText.text = sessionStats.EnemyKilled.ToString();
        _wavesCompletedText.text = sessionStats.WavesCompleted.ToString();
    }

    private void HandlerReturnMainMenuButton()
    {
        GameManager.Instance.LoadMainMenu();
    }

    private void HandlerRestartButton()
    {
        GameManager.Instance.RestartLevel();
    }

    private void OnEnable()
    {
        DisplayStatsField();
    }

    private void Start()
    {
        _restartButon.onClick.AddListener(HandlerRestartButton);
        _returnMainMenuButton.onClick.AddListener(HandlerReturnMainMenuButton);
    }
}
