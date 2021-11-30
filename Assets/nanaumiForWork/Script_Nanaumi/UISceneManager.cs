using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneManager : MonoBehaviour
{
    public AsyncOperation[] asyncLoad = { default, default, default, default };

    /// <summary>
    /// シーンのロード
    /// </summary>
    /// <param name="sceneNum">UIController.SceneNumber</param>
    public void LoadScene(int sceneNum)
    {
        LoadSceneWait(sceneNum);
    }

    private IEnumerator LoadSceneWait(int sceneNum)
    {
        // 非同期でロードを行う
        asyncLoad[sceneNum] = SceneManager.LoadSceneAsync(sceneNum);

        // ロードが完了していても，シーンのアクティブ化は許可しない
        asyncLoad[sceneNum].allowSceneActivation = false;

        // ロードが完了するまで待つ
        yield return asyncLoad[sceneNum];
    }
}
