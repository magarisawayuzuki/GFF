using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI_2 : UIController_2
{
    // 警告パネルの設定
    [SerializeField] private GameObject cautionPanel = default;
    [SerializeField] private CautionPanel_2 caucau = default;

    private void Start()
    {
        base.Start();
        _nowSelectNumber = 1;

        // カーソルの初期化
        _selector.anchoredPosition = _selectPoint[_nowSelectNumber - 1].anchoredPosition;
        _selector.sizeDelta = _selectPoint[_nowSelectNumber - 1].sizeDelta * _selectorSizeDeltaMagnitude;
    }

    private void Update()
    {
        InputManager();

        if (_isInput[1])
        {
            PauseSelector();
        }
    }

    /// <summary>
    /// 決定されたときの処理
    /// </summary>
    private void PauseSelector()
    {
        switch (_nowSelectNumber)
        {
            // Back to InGame
            case 1:
                if (!_isLoaded)
                {
                    _isLoaded = true;
                    bookimage.material.SetFloat("_Flip", -1f);

                    // Audio
                    audios.uiSE = (AudioManager.UISE)2;
                    audios.AudioChanger("UI");

                    InGameToPauseUI_2._isPause = false;
                    InGameToPauseUI_2._isStaticPause = false;
                    SceneManager.UnloadSceneAsync(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Pause));
                }

                // 本をめくる
                if (_imageFlipTime >= 0)
                {
                    bookimage.material.SetFloat("_Flip", bookimage.material.GetFloat("_Flip") + Time.deltaTime);
                    _imageFlipTime -= Time.deltaTime;
                }
                else
                {
                    _isInput[1] = false;
                }
                break;
            // Back to Title to CautionPanel
            case 2:
                // Audio
                audios.uiSE = (AudioManager.UISE)1;
                audios.AudioChanger("UI");

                // 警告パネルの有効化
                cautionPanel.SetActive(true);
                caucau.enabled = true;
                _isInput[1] = false;
                this.enabled = false;
                break;
            // to Option
            case 3:
                if (!_isLoaded)
                {
                    _isLoaded = true;
                    bookimage.material.SetFloat("_Flip", -1f);

                    // Audio
                    audios.uiSE = (AudioManager.UISE)4;
                    audios.AudioChanger("UI");

                    InGameToPauseUI_2._isPause = false;
                    sceneMan.LoadScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Option), true, SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Pause));
                }

                // 本をめくる
                if (_imageFlipTime >= 0)
                {
                    bookimage.material.SetFloat("_Flip", bookimage.material.GetFloat("_Flip") + Time.deltaTime * 4);
                    _imageFlipTime -= Time.deltaTime;
                }
                else
                {
                    _isInput[1] = false;
                }
                break;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _isNotFlip = false;
        _isLoaded = false;
        _imageFlipTime = 0;

        // SceneStateをPauseに設定
        SceneStateUI_2.sceneState = SceneStateUI_2.SceneState.Pause;
    }
}
