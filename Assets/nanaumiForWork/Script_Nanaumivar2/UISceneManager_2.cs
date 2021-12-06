using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneManager_2
{
    private string DefaultSceneName;

    /// <summary>
    /// シーンのロード
    /// </summary>
    /// <param name="NextSceneName">次に読み込むシーン名</param>
    /// <param name="IsUnloadScene">アンロードするしないフラグ</param>
    /// <param name="ThisSceneName">破棄するシーン名</param>
    public void LoadScene(string NextSceneName, bool IsUnloadScene, string ThisSceneName)
    {
        // アンロードする場合
        if (IsUnloadScene)
        {
            // イベントにイベントハンドラーを追加
            Debug.Log("イベントハンドラーを追加");
            SceneManager.sceneLoaded += SceneLoaded;
            this.DefaultSceneName = ThisSceneName;
        }

        // シーンの追加ロード
        Debug.Log("Loading");
        SceneManager.LoadScene(NextSceneName, LoadSceneMode.Additive);
    }

    // イベントハンドラー（イベント発生時に動かしたいアンロードの処理）
    private void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        Debug.Log(nextScene.name);
        Debug.Log(mode);

        UnLoadScene(this.DefaultSceneName);
    }

    /// <summary>
    /// シ－ンのアンロード
    /// </summary>
    /// <param name="SceneName">アンロードするシーン名</param>
    public void UnLoadScene(string SceneName)
    {
        Debug.Log("Unloading");
        SceneManager.UnloadSceneAsync(SceneName);
    }
}
