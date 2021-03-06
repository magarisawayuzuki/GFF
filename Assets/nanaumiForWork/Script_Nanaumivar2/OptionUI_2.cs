using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionUI_2 : UIController_2
{
    private bool _isVolumeChange = false;

    [SerializeField] private Slider[] slider = default;

    [SerializeField] private int _volumeSliderMagnitude = default;

    private void Start()
    {
        base.Start();
        _isVolumeChange = false;

        _selector.anchoredPosition = _selectPoint[_nowSelectNumber - 1].anchoredPosition;
        _selector.sizeDelta = _selectPoint[_nowSelectNumber - 1].sizeDelta * _selectorSizeDeltaMagnitude;
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
            if (_nowSelectNumber == 4)
            {
                BackToScene(SceneStateUI_2.sceneState);
            }
            else
            {
                _isVolumeChange = !_isVolumeChange;
                _isInput[1] = false;
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
        if (!_isLoaded)
        {
            _isLoaded = true;
            bookimage.material.SetFloat("_Flip", -1f);

            switch (sceneState)
            {
                case SceneStateUI_2.SceneState.Title:
                    audios.uiSE = (AudioManager.UISE)4;
                    audios.AudioChanger("UI");
                    sceneMan.LoadScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Title), true, SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Option2));
                    break;
                case SceneStateUI_2.SceneState.Pause:
                    audios.uiSE = (AudioManager.UISE)4;
                    audios.AudioChanger("UI");
                    sceneMan.LoadScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Pause), true, SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Option));
                    break;
            }
        }

        if (_imageFlipTime >= 0)
        {
            bookimage.material.SetFloat("_Flip", bookimage.material.GetFloat("_Flip") + Time.deltaTime * 4);
            _imageFlipTime -= Time.deltaTime;
        }
        else
        {
            _isInput[1] = false;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _isLoaded = false;
    }
}
