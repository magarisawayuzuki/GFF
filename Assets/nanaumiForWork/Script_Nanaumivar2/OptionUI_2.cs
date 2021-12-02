using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionUI_2 : UIController_2
{
    private bool _isVolumeChange = false;

    [SerializeField] private string TitleSceneName;
    [SerializeField] private string PauseSceneName;

    [SerializeField] private Slider[] slider;

    [SerializeField] private int _volumeSliderMagnitude;

    private void Start()
    {
        _isVolumeChange = false;

        _selector.anchoredPosition = _selectPoint[_nowSelectNumber - 1].anchoredPosition;
        _selector.sizeDelta = _selectPoint[_nowSelectNumber - 1].sizeDelta;
    }

    private void Update()
    {
        if (!_isVolumeChange)
        {
            InputManager();
        }
        else if (_isVolumeChange)
        {
            TitleSelector();
        }

        if (_isInput[1] || _inputs.UI.Decide.triggered)
        {
            _isVolumeChange = !_isVolumeChange;
            _isInput[1] = false;

            if(_nowSelectNumber == 4)
            {
                BackToScene(SceneManager.GetActiveScene().name);
                _isVolumeChange = false;
            }
        }
    }

    private void TitleSelector()
    {
        slider[_nowSelectNumber - 1].value += _inputs.UI.Select_Horizontal.ReadValue<float>() / _volumeSliderMagnitude;
    }

    private void BackToScene(string getSceneName)
    {
        switch (getSceneName)
        {
            case "Title":
                sceneMan.LoadScene(TitleSceneName, true, getSceneName);
                break;
            case "Pause":
                sceneMan.LoadScene(PauseSceneName, true, getSceneName);
                break;
        }
    }
}
