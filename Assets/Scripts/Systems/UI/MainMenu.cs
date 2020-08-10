using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Text _goldAmount;

    [SerializeField]
    private Text _gemAmount;

    [SerializeField]
    private Text _levelAmount;

    [SerializeField]
    private Image _experienceBar;

    [SerializeField]
    private CharacterStats_SO _characterDefinition;

    [SerializeField]
    private Button _settingsButton;

    [SerializeField]
    private Button _playButton;

    [SerializeField]
    private Button _changeLevelButton;

    [SerializeField]
    private GameObject _settingsMenu;

    [SerializeField]
    private GameObject _levelsMenu;

    [SerializeField]
    private LevelsDefinition _levelsDefinition;

    private string _choosenLevel;

    public void UpdateUIDefinition()
    {
        _gemAmount.text = Convert.ToString(_characterDefinition.currentGem);
        _goldAmount.text = Convert.ToString(_characterDefinition.currentGold);
        _levelAmount.text = Convert.ToString(_characterDefinition.charLevel);
        _experienceBar.fillAmount = (float)_characterDefinition.charExperience / (float)_characterDefinition.charLevels[_characterDefinition.charLevel].experienceRequirement;
    }

    

    public void HandleLevelsButtons(string level)
    {
        if (SetChoosenLevel(level))
        {
            DisplayChangeLevelMenu();
        }
    }

    bool SetChoosenLevel(string level)
    {
        bool success = false;
        if (_levelsDefinition.levels.Any(l => l.levelName == level))
        {
            _changeLevelButton.GetComponent<Image>().sprite = _levelsDefinition.levels.Single(l => l.levelName == level).levelImage;
            _choosenLevel = level;
            success = true;
        }
        return success;
    }


    void HandleSettingClick()
    {
        DisplaySettingsMenu();
    }

    void HandlePlayButton()
    {
        GameManager.Instance.LoadLevel(_choosenLevel);
    }

    void HandleChangeLevelButton()
    {
        DisplayChangeLevelMenu();
    }

    void DisplayChangeLevelMenu()
    {
        bool set = _levelsMenu.activeSelf ? false : true;
        _levelsMenu.SetActive(set);
    }

    void DisplaySettingsMenu()
    {
        bool set = _settingsMenu.activeSelf ? false : true;
        _settingsMenu.SetActive(set);
    }

    

    void Start()
    {
        _changeLevelButton.onClick.AddListener(HandleChangeLevelButton);
        _playButton.onClick.AddListener(HandlePlayButton);
        _settingsButton.onClick.AddListener(HandleSettingClick);
        SetChoosenLevel(_levelsDefinition.levels[0].levelName);
        UpdateUIDefinition();
    }
}
