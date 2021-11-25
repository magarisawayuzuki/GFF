using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class UIController : MonoBehaviour
{
    protected InputController _inputs;
    private string _nowScene;
    [SerializeField] SelectorMove[] _select;

    protected bool _isPause = false;
    private int _nowSelectNumber = 1;
    protected bool _isInput = false;

    protected virtual void Awake()
    {
        _inputs = new InputController();
        _nowScene = SceneManager.GetActiveScene().name;
    }

    protected void Start()
    {
        _isPause = false;
        _nowSelectNumber = 1;
        _isInput = false;
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
        if (!_isInput)
        {
            InputSelector();
        }
        else
        {
            
        }
    }

    private void InputSelector()
    {
        // 決定
        if (_inputs.UI.Decide.triggered)
        {
            // シーン遷移する選択肢のとき
            if (_select[_nowSelectNumber].nextdoit.isTransition)
            {
                ChangeSceneCall(SceneDictionary[_select[_nowSelectNumber].nextdoit.nextScene] - 1);
                _isInput = false;
            }

            // シーン遷移しない選択肢のとき
            else
            {
                _isPause = false;
            }
        }

        // 上
        if (_inputs.UI.Select_Vertical.ReadValue<float>() > 0)
        {
            if (_select[_nowSelectNumber].nextdoit.up != 0)
            {
                _nowSelectNumber = _select[_nowSelectNumber].nextdoit.up;
                _isInput = false;
            }
        }
        // 下
        else if (_inputs.UI.Select_Vertical.ReadValue<float>() < 0)
        {
            if (_select[_nowSelectNumber].nextdoit.down != 0)
            {
                _nowSelectNumber = _select[_nowSelectNumber].nextdoit.down;
                _isInput = false;
            }
        }
        // 右
        else if (_inputs.UI.Select_Horizontal.ReadValue<float>() > 0)
        {
            if (_select[_nowSelectNumber].nextdoit.right != 0)
            {
                _nowSelectNumber = _select[_nowSelectNumber].nextdoit.right;
                _isInput = false;
            }
        }
        // 左
        else if (_inputs.UI.Select_Horizontal.ReadValue<float>() < 0)
        {
            if (_select[_nowSelectNumber].nextdoit.left != 0)
            {
                _nowSelectNumber = _select[_nowSelectNumber].nextdoit.left;
                _isInput = false;
            }
        }
        //else
        //{
        //    Debug.Log("none");
        //    _isInput = Enumerable.Repeat<bool>(false, _isInput.Length).ToArray();
        //    return;
        //}
    }

    protected void ChangeSceneCall(int sceneNumber)
    {
        UISceneManager loadScene = new UISceneManager();

        //// シーン名の取得
        //string sceneName = SceneManager.GetActiveScene().name;
        //// シーン名からシーンナンバー取得 ロードシーンの呼び出し時に使用
        //int sceneNumber = SceneDictionary[sceneName];

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
public class SelectorMove
{
    public nextDone nextdoit;
}

[System.Serializable]
public class nextDone
{
    public int left;
    public int right;
    public int up;
    public int down;

    public string nextScene;

    public bool isTransition;
}
