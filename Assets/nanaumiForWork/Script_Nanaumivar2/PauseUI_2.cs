﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI_2 : UIController_2
{
    [SerializeField] private string InGameSceneName;
    [SerializeField] private string OptionSceneName;

    [SerializeField] private GameObject cautionPanel;
    [SerializeField] private CautionPanel_2 caucau;

    private void Start()
    {
        _nowSelectNumber = 1;

        _selector.anchoredPosition = _selectPoint[_nowSelectNumber - 1].anchoredPosition;
        _selector.sizeDelta = _selectPoint[_nowSelectNumber - 1].sizeDelta;
    }

    private void Update()
    {
        InputManager();

        if (_isInput[1])
        {
            TitleSelector();
        }
    }

    private void TitleSelector()
    {
        switch (_nowSelectNumber)
        {
            // Back to InGame
            case 1:
                sceneMan.LoadScene(InGameSceneName, true, SceneManager.GetActiveScene().name);
                break;
            // Back to Title to CautionPanel
            case 2:
                cautionPanel.SetActive(true);
                caucau.enabled = true;
                this.enabled = false;
                break;
            // to Option
            case 3:
                sceneMan.LoadScene(OptionSceneName, true, SceneManager.GetActiveScene().name);
                break;
        }

        _isInput[1] = false;
    }
}
