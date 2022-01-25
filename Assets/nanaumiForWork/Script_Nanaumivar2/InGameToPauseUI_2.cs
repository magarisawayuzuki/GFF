using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameToPauseUI_2 : UIController_2
{
    /// <summary>
    /// ポーズ中か
    /// </summary>
    public static bool _isStaticPause = default;
    /// <summary>
    /// ポーズシーンでポーズが押されたか
    /// </summary>
    public static bool _isPause = default;

    protected override void Awake()
    {
        base.Awake();

        // ScreenShotの初期化
        audios.sprite.Clear();
    }

    private void Start()
    {
        bookimage.material.SetFloat("_Flip", 1f);
        _isStaticPause = false;
        _isPause = false;
    }

    private void Update()
    {
        if (_isNotFlip)
        {
            // Pauseが押されたとき
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
        // 本をめくる処理
        else if (!_isNotFlip)
        {
            bookimage.material.SetFloat("_Flip", bookimage.material.GetFloat("_Flip") - Time.deltaTime * 4);
            _imageFlipTime += Time.deltaTime;
            if (!(_imageFlipTime < 0.5f && _imageFlipTime > -0.5f) && !_isLoaded)
            {
                _isNotFlip = true;
            }
        }
    }

    /// <summary>
    /// Pauseシーンへの遷移
    /// </summary>
    private void ToPause()
    {
        // シーンが存在していないとき、ロードする
        if (!ContainsScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Pause)))
        {
            if (!_isLoaded)
            {
                _isLoaded = true;
                bookimage.material.SetFloat("_Flip", -1f);

                // Audio
                audios.uiSE = (AudioManager.UISE)3;
                audios.AudioChanger("UI");

                sceneMan.LoadScene(SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.Pause), false, SceneStateUI_2.SceneName(SceneStateUI_2.SceneState.InGame));
            }
        }

        // ロードが始まったら
        if (_isLoaded)
        {
            // 本をめくる
            if (_imageFlipTime >= 0)
            {
                bookimage.material.SetFloat("_Flip", bookimage.material.GetFloat("_Flip") + Time.deltaTime * 4);
                _imageFlipTime -= Time.deltaTime;
            }
            else
            {
                _isInput[1] = false;
                _isNotFlip = false;
                _isLoaded = false;
            }
        }
    }

    /// <summary>
    /// シーンがゲーム上に存在しているか
    /// </summary>
    /// <param name="sceneName">確認するシーンIndex</param>
    /// <returns>true:存在している</returns>
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
        _isNotFlip = false;
        _isLoaded = false;

        // Audio
        audios.bgm = (AudioManager.BGM)1;
        audios.AudioChanger("BGM");

        // SceneStateをInGameに設定
        SceneStateUI_2.sceneState = SceneStateUI_2.SceneState.InGame;
    }
}
