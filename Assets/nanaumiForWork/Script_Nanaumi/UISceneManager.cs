using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneManager : MonoBehaviour
{
    public AsyncOperation[] asyncLoad = { default, default, default, default };

    public void LoadScene(int sceneNum)
    {
        StartCoroutine(AsyncLoadScene(sceneNum));
    }

    /// <summary>
    /// シーンのロード
    /// </summary>
    /// <param name="sceneNum">UIController.SceneNumber</param>
    private IEnumerator AsyncLoadScene(int sceneNum)
    {
        // 非同期でロードを行う
        asyncLoad[sceneNum] = SceneManager.LoadSceneAsync(sceneNum);

        // ロードが完了していても，シーンのアクティブ化は許可しない
        //asyncLoad[sceneNum].allowSceneActivation = false;

        yield return StartCoroutine(Loading(sceneNum));
    }

    private IEnumerator Loading(int sceneNum)
    {
        Debug.Log("Coroutine Loading");
        // ロードが完了するまで待つ
        yield return asyncLoad[sceneNum];
    }
}
