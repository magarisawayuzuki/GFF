using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class UIController : MonoBehaviour
{
    private InputController _inputs;
    private string _nowScene;
    protected virtual void Awake()
    {
        _inputs = new InputController();
        _nowScene = SceneManager.GetActiveScene().name;
    }

    protected Dictionary<string,int> SceneDictionary = new Dictionary<string, int>() 
    {
        {"Title", 0},
        {"InGame", 1},
        {"Option", 2},
        {"Pause", 3}
    };

    protected enum SceneState
    {
        Title,
        InGame,
        Option,
        Pause
    }

    protected virtual void InputManager()
    {
        InputSelector(_nowScene);
    }

    private int _nowSelectNumber = 0;
    /// <summary>
    /// 0決定 1上 2下 3右 4左
    /// </summary>
    protected bool[] _isInput = { false, false, false, false, false };
    private void InputSelector(string nowScene)
    {
        if (_inputs.UI.Decide.triggered)
        {
            _isInput = Enumerable.Repeat<bool>(false, _isInput.Length).ToArray();
            _isInput[0] = true;
        }

        if (_inputs.UI.Select_Vertical.ReadValue<float>() > 0)
        {
            _isInput = Enumerable.Repeat<bool>(false, _isInput.Length).ToArray();
            _isInput[1] = true;
        }
        else if (_inputs.UI.Select_Vertical.ReadValue<float>() < 0)
        {
            _isInput = Enumerable.Repeat<bool>(false, _isInput.Length).ToArray();
            _isInput[2] = true;
        }
        else if (_inputs.UI.Select_Horizontal.ReadValue<float>() > 0)
        {
            _isInput = Enumerable.Repeat<bool>(false, _isInput.Length).ToArray();
            _isInput[3] = true;
        }
        else if (_inputs.UI.Select_Horizontal.ReadValue<float>() < 0)
        {
            _isInput = Enumerable.Repeat<bool>(false, _isInput.Length).ToArray();
            _isInput[4] = true;
        }
        else
        {
            Debug.Log("none");
            _isInput = Enumerable.Repeat<bool>(false, _isInput.Length).ToArray();
            return;
        }
    }

    protected void ChangeSceneCall()
    {
        UISceneManager loadScene = new UISceneManager();

        // シーン名の取得
        string sceneName = SceneManager.GetActiveScene().name;
        // シーン名からシーンナンバー取得 ロードシーンの呼び出し時に使用
        int sceneNumber = SceneDictionary[sceneName];

        loadScene.LoadScene(sceneNumber);
    }

    private void OnEnable()
    {
        _inputs.Enable();
    }
    private void OnDisable()
    {
        _inputs.Disable();
    }
}

[System.Serializable]
public class Select
{
    public GameObject obj;
}
