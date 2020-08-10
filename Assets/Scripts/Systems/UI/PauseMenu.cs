using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private Button _pauseButton;

    [SerializeField]
    private Button _resumeButton;

    [SerializeField]
    private Button _restartButton;

    [SerializeField]
    private Button _exitButton;

    [SerializeField]
    private GameObject _pauseMenu;

    void HandlePauseButton()
    {
        _pauseMenu.SetActive(true);
        GameManager.Instance.TogglePause();
    }

    void HandleResumeButton()
    {
        _pauseMenu.SetActive(false);
        GameManager.Instance.TogglePause();
    }

    void HandleRestartButton()
    {
        _pauseMenu.SetActive(false);
        GameManager.Instance.RestartLevel();
    }

    void HandleExitButton()
    {
        _pauseMenu.SetActive(false);
        GameManager.Instance.LoadMainMenu();
    }
    private void Start()
    {
        if(_pauseButton != null)
            _pauseButton.onClick.AddListener(HandlePauseButton);

        if(_resumeButton != null)
            _resumeButton.onClick.AddListener(HandleResumeButton);

        _restartButton.onClick.AddListener(HandleRestartButton);

        _exitButton.onClick.AddListener(HandleExitButton);
    }


}
