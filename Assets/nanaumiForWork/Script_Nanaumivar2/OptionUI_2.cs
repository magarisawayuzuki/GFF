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
            }
        }
    }

    private void VolumeChanger()
    {
        slider[_nowSelectNumber - 1].value += _inputs.UI.Select_Horizontal.ReadValue<float>() / _volumeSliderMagnitude;
    }

    private void OnMouseDrag()
    {
        //Cubeの位置をワールド座標からスクリーン座標に変換して、objectPointに格納
        Vector3 objectPoint
            = Camera.main.WorldToScreenPoint(transform.position);

        //Cubeの現在位置(マウス位置)を、pointScreenに格納
        Vector3 pointScreen
            = new Vector3(Input.mousePosition.x,
                          Input.mousePosition.y,
                          objectPoint.z);

        //Cubeの現在位置を、スクリーン座標からワールド座標に変換して、pointWorldに格納
        Vector3 pointWorld = Camera.main.ScreenToWorldPoint(pointScreen);
        pointWorld.z = transform.position.z;

        //Cubeの位置を、pointWorldにする
        transform.position = pointWorld;
    }

    private void BackToScene(SceneStateUI_2.SceneState sceneState)
    {
        _isVolumeChange = false;
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
}
