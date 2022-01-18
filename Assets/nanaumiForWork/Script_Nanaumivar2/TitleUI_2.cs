using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUI_2 : UIController_2
{
    // 警告パネルオブジェクトとスクリプト
    [SerializeField] private GameObject cautionPanel = default;
    [SerializeField] private CautionPanel_2 caucau = default;

    private void Start()
    {
        // Audioの設定
        audios.bgm = (AudioManager.BGM)0;
        audios.AudioChanger("BGM");

        _nowSelectNumber = 1;

        // カーソルの場所の初期化
        _selector.anchoredPosition = _selectPoint[_nowSelectNumber - 1].anchoredPosition;
        _selector.sizeDelta = _selectPoint[_nowSelectNumber - 1].sizeDelta * _selectorSizeDeltaMagnitude;
    }

    private void Update()
    {
        InputManager();

        // 決定が押されたとき
        if(_isInput[1])
        {
            TitleSelector();
        }
    }

    /// <summary>
    /// 次に遷移するシーン管理
    /// </summary>
    private void TitleSelector()
    {
        switch (_nowSelectNumber)
        {
            // to InGame
            case 1:
                if (!_isLoaded)
                {
                    _isLoaded = true;
                    bookimage.material.SetFloat("_Flip", -1f);

                    audios.uiSE = (AudioManager.UISE)1;
                    audios.AudioChanger("UI");
                    audios.uiSE = (AudioManager.UISE)4;
                    audios.AudioChanger("UI");

                    sceneMan.LoadScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Main), false, 10);
                    sceneMan.LoadScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.InGame), true, SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Title));
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
                break;
            // to Option
            case 2:
                if (!_isLoaded)
                {
                    _isLoaded = true;
                    bookimage.material.SetFloat("_Flip", -1f);

                    audios.uiSE = (AudioManager.UISE)4;
                    audios.AudioChanger("UI");

                    sceneMan.LoadScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Option2), true, SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Title));
                }

                if (_imageFlipTime >= 0)
                {
                    bookimage.material.SetFloat("_Flip", bookimage.material.GetFloat("_Flip") + Time.deltaTime * 4);
                    _imageFlipTime += Time.deltaTime;
                }
                else
                {
                    _isInput[1] = false;
                }
                break;
            // Exit Game to CautionPanel
            case 3:
                audios.uiSE = (AudioManager.UISE)1;
                audios.AudioChanger("UI");
                cautionPanel.SetActive(true);
                caucau.enabled = true;
                this.enabled = false;

                _isInput[1] = false;
                _isFlip = false;
                break;
            default:
                break;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _isFlip = false;
        _isLoaded = false;

        //audios.bgm = (AudioManager.BGM)0;
        //audios.AudioChanger("BGM");
        SceneStateUI_2.sceneState = SceneStateUI_2.SceneState.Title;
    }
}
