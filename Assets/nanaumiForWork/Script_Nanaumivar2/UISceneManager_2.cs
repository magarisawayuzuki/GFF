using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneManager_2
{
    private string DefaultSceneName;

    public void LoadScene(string NextSceneName, bool IsUnloadScene, string ThisSceneName)
    {
        if (IsUnloadScene)
        {
            // イベントにイベントハンドラーを追加
            SceneManager.sceneLoaded += SceneLoaded;
            this.DefaultSceneName = ThisSceneName;
        }

        SceneManager.LoadScene(NextSceneName, LoadSceneMode.Additive);
    }

    // イベントハンドラー（イベント発生時に動かしたい処理）
    private void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        Debug.Log(nextScene.name);
        Debug.Log(mode);

        UnLoadScene(this.DefaultSceneName);
    }

    public void UnLoadScene(string SceneName)
    {
        SceneManager.UnloadSceneAsync(SceneName);
    }

    public enum SceneState
    {
        Title,
        Pause,
        InGame
    }
}
