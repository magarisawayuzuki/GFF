using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneManager_2 : MonoBehaviour
{
    private int DefaultSceneName;

    /// <summary>
    /// シーンのロード
    /// </summary>
    /// <param name="NextSceneName">次に読み込むシーン名</param>
    /// <param name="IsUnloadScene">アンロードするしないフラグ</param>
    /// <param name="ThisSceneName">破棄するシーン名</param>
    public void LoadScene(int NextSceneName, bool IsUnloadScene, int ThisSceneName)
    {
        // アンロードする場合
        if (IsUnloadScene)
        {
            this.DefaultSceneName = ThisSceneName;

            // イベントにイベントハンドラーを追加
            SceneManager.sceneLoaded += SceneLoaded;
        }

        // シーンの追加ロード
        AsyncOperation async = SceneManager.LoadSceneAsync(NextSceneName, LoadSceneMode.Additive);
        async.allowSceneActivation = false;
        StartCoroutine(WaitSec(async));
    }

    private IEnumerator WaitSec(AsyncOperation activeScene)
    {
        yield return new WaitForSeconds(0.4f);
        yield return activeScene.allowSceneActivation = true;
        yield break;
    }

    // イベントハンドラー（イベント発生時に動かしたいアンロードの処理）
    private void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        UnLoadScene(this.DefaultSceneName);
    }

    /// <summary>
    /// シ－ンのアンロード
    /// </summary>
    /// <param name="SceneName">アンロードするシーン名</param>
    public void UnLoadScene(int SceneName)
    {
        SceneManager.UnloadSceneAsync(SceneName,UnloadSceneOptions.None);
        SceneManager.sceneLoaded -= SceneLoaded;
    }
}
