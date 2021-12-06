using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionUI_2 : UIController_2
{
    private bool _isVolumeChange = false;

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
            VolumeChanger();
        }

        if (_isInput[1] || _inputs.UI.Decide.triggered)
        {
            _isVolumeChange = !_isVolumeChange;
            _isInput[1] = false;

            if(_nowSelectNumber == 4)
            {
                BackToScene(SceneStateUI_2.sceneState);
                _isVolumeChange = false;
            }
        }
    }

    private void VolumeChanger()
    {
        slider[_nowSelectNumber - 1].value += _inputs.UI.Select_Horizontal.ReadValue<float>() / _volumeSliderMagnitude;
    }

    private void BackToScene(SceneStateUI_2.SceneState sceneState)
    {
        Debug.Log("sceneState" + sceneState);
        switch (sceneState)
        {
            case SceneStateUI_2.SceneState.Title:
                sceneMan.LoadScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Title), true, SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Option));
                break;
            case SceneStateUI_2.SceneState.Pause:
                sceneMan.LoadScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Pause), true, SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Option));
                break;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        SceneStateUI_2.sceneState = SceneStateUI_2.SceneState.Title;
    }
}
