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

    // selector自体のオブジェクト
    [SerializeField] private GameObject _selector;
    // selectorの移動先オブジェクト
    [SerializeField] private GameObject[] selectPoint;
    // selectorの移動速度
    [SerializeField] private float selectorMagnitude;

    protected bool _isPause = false;
    protected int _nowSelectNumber = 1;
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
            SelectorResize();
        }
    }

    private const int ONE = 1;
    private void InputSelector()
    {
        // 決定
        if (_inputs.UI.Decide.triggered)
        {
            // シーン遷移する選択肢のとき
            if (_select[_nowSelectNumber - ONE].nextdoit.isTransition)
            {
                ChangeSceneCall(SceneDictionary[_select[_nowSelectNumber - ONE].nextdoit.nextScene] - 1);
                _isInput = true;
            }

            // シーン遷移しない選択肢のとき
            else
            {
                _isPause = true;
            }
        }

        // 上
        if (_inputs.UI.Select_Vertical.ReadValue<float>() > 0)
        {
            if (_select[_nowSelectNumber - ONE].nextdoit.up != 0)
            {
                _nowSelectNumber = _select[_nowSelectNumber - ONE].nextdoit.up;
                _isInput = true;
            }
        }
        // 下
        else if (_inputs.UI.Select_Vertical.ReadValue<float>() < 0)
        {
            if (_select[_nowSelectNumber - ONE].nextdoit.down != 0)
            {
                _nowSelectNumber = _select[_nowSelectNumber - ONE].nextdoit.down;
                _isInput = true;
            }
        }
        // 右
        else if (_inputs.UI.Select_Horizontal.ReadValue<float>() > 0)
        {
            if (_select[_nowSelectNumber - ONE].nextdoit.right != 0)
            {
                _nowSelectNumber = _select[_nowSelectNumber - ONE].nextdoit.right;
                _isInput = true;
            }
        }
        // 左
        else if (_inputs.UI.Select_Horizontal.ReadValue<float>() < 0)
        {
            if (_select[_nowSelectNumber - ONE].nextdoit.left != 0)
            {
                _nowSelectNumber = _select[_nowSelectNumber - ONE].nextdoit.left;
                _isInput = true;
            }
        }
        //else
        //{
        //    Debug.Log("none");
        //    _isInput = Enumerable.Repeat<bool>(false, _isInput.Length).ToArray();
        //    return;
        //}
    }

    private float _selectorResizeProgressTime = 0;
    protected void SelectorResize()
    {
        if (_selectorResizeProgressTime < 1)
        {
            _selectorResizeProgressTime += Time.deltaTime * selectorMagnitude;
            _selector.transform.localPosition = Vector2.Lerp(_selector.transform.localPosition, selectPoint[_nowSelectNumber - 1].transform.localPosition, _selectorResizeProgressTime);

            Debug.Log("selectPoint : " + _selectorResizeProgressTime);
        }
        
        if(_selectorResizeProgressTime >= 1)
        {
            _selectorResizeProgressTime = 0;
            _isInput = false;
        }
    }

    protected void ChangeSceneCall(int sceneNumber)
    {
        UISceneManager loadScene = new UISceneManager();

        //// シーン名の取得
        //string sceneName = SceneManager.GetActiveScene().name;
        //// シーン名からシーンナンバー取得 ロードシーンの呼び出し時に使用
        //int sceneNumber = SceneDictionary[sceneName];

        loadScene.LoadScene(sceneNumber);

        Debug.Log("LoadingOK");
        loadScene.asyncLoad[sceneNumber].allowSceneActivation = true;
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
    public NextDone nextdoit;
}

[System.Serializable]
public class NextDone
{
    public int left;
    public int right;
    public int up;
    public int down;

    // 遷移先シーン名
    public string nextScene;

    // 遷移するフラグ
    public bool isTransition;
}
