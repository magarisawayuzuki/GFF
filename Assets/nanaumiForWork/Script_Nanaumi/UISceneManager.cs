using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneManager : MonoBehaviour
{
    /// <summary>
    /// シーンのロード
    /// </summary>
    /// <param name="sceneNum">UIController.SceneNumber</param>
    public void LoadScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }
}
