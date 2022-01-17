using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameToPauseUI_2 : UIController_2
{
    // ポーズ中か
    public static bool _isStaticPause = default;
    // ポーズシーンか
    public static bool _isPause = default;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        bookimage.material.SetFloat("_Flip", 1f);
        _isStaticPause = false;
        _isPause = false;
    }

    private void Update()
    {
        if (_isFlip)
        {
            if (_inputs.UI.Pause.triggered && !_isPause)
            {
                _isStaticPause = true;
                _isPause = true;
            }

            if (_isPause)
            {
                ToPause();
            }
        }
        else if (!_isFlip)
        {
            bookimage.material.SetFloat("_Flip", bookimage.material.GetFloat("_Flip") - Time.deltaTime * 4);
            _imageFlipTime += Time.deltaTime;
            if (!(_imageFlipTime < 0.5f && _imageFlipTime > -0.5f) && !_isLoaded)
            {
                _isFlip = true;
            }
        }
    }

    private void ToPause()
    {
        if (!ContainsScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Pause)))
        {
            if (!_isLoaded)
            {
                _isLoaded = true;
                bookimage.material.SetFloat("_Flip", -1f);

                audios.uiSE = (AudioManager.UISE)3;
                audios.AudioChanger("UI");

                sceneMan.LoadScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Pause), false, SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.InGame));
            }
        }

        if (_isLoaded)
        {
            if (_imageFlipTime >= 0)
            {
                bookimage.material.SetFloat("_Flip", bookimage.material.GetFloat("_Flip") + Time.deltaTime * 4);
                _imageFlipTime -= Time.deltaTime;
            }
            else
            {
                _isInput[1] = false;
                _isFlip = false;
                _isLoaded = false;
            }
        }
    }

    private bool ContainsScene(int sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).buildIndex == sceneName)
            {
                return true;
            }
        }
        return false;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _isFlip = false;
        _isLoaded = false;

        audios.bgm = (AudioManager.BGM)1;
        audios.AudioChanger("BGM");
        SceneStateUI_2.sceneState = SceneStateUI_2.SceneState.InGame;
    }
}
