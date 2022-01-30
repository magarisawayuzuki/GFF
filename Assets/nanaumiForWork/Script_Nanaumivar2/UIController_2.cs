using System;
using UnityEngine;
using UnityEngine.UI;

[AttributeUsage(AttributeTargets.Method)]
public class CalledFromSendMessageAttribute : Attribute { }

public class UIController_2 : MonoBehaviour
{
    protected InputController _inputs = default;
    protected UISceneManager_2 sceneMan = default;
    protected AudioManager audios = default;

    #region カーソル関係
    /// <summary>
    /// 次にカーソルを動かすオブジェクトの数字
    /// </summary>
    [SerializeField] SelectorMove_2[] _select = default;
    /// <summary>
    /// 現在選択されているオブジェクトの数字 0は次に何も選択されていない状態
    /// </summary>
    protected int _nowSelectNumber = 1;
    /// <summary>
    /// 選択されるオブジェクトの配列
    /// </summary>
    [SerializeField] protected RectTransform[] _selectPoint = default;

    /// <summary>
    /// カーソルのオブジェクト
    /// </summary>
    [SerializeField] protected RectTransform _selector = default;
    /// <summary>
    /// カーソルの動く速度
    /// </summary>
    [SerializeField, Tooltip("カーソルの動く速度")] private float _selectorMagnitude = default;
    /// <summary>
    /// カーソルのリサイズしている時間
    /// </summary>
    private float _selectorResizeProgressTime = default;
    /// <summary>
    /// カーソルを指定されたオブジェクトより少し大きくする倍率
    /// </summary>
    protected float _selectorSizeDeltaMagnitude = 1.1f;
    
    /// <summary>
    /// カーソルのImage
    /// </summary>
    [SerializeField] private Image _selectorImage = default;
    /// <summary>
    /// カーソルのImage透明度
    /// </summary>
    private Color _selectorColorTransparency = default;
    /// <summary>
    /// カーソルが点滅するサイクル
    /// </summary>
    private float _selectorFlashCycle = 1;
#endregion

    /// <summary>
    /// 0上下左右 1decide の入力
    /// </summary>
    protected bool[] _isInput = { false, false };

    private const int ONE = 1;

    #region シーン遷移
    [SerializeField] protected Image bookimage;
    /// <summary>
    /// めくる時間
    /// </summary>
    protected float _imageFlipTime = default;
    /// <summary>
    /// めくっていない
    /// </summary>
    protected bool _isNotFlip = default;
    /// <summary>
    /// ロード中
    /// </summary>
    protected bool _isLoaded = default;
    #endregion

    protected virtual void Awake()
    {
        // 本をめくる前の状態にする 
        bookimage.material.SetFloat("_Flip", 1f);

        _inputs = new InputController();
        sceneMan = gameObject.AddComponent<UISceneManager_2>();
    }

    protected virtual void Start()
    {
        audios = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    /// <summary>
    /// 入力可能・シーン遷移フラグ処理
    /// </summary>
    protected void InputManager()
    {
        // シーン遷移(ページめくる)が終わったら
        if (_isNotFlip)
        {
            // 入力がないとき
            if (!_isInput[0] && !_isInput[1])
            {
                // 入力処理
                InputSelector();

                CursolTransparency();
            }
            // 上下左右が押されたとき
            else if (_isInput[0] && !_isInput[1])
            {
                SelectorResize();
            }
        }
        // シーン遷移最中だったら
        else if (!_isNotFlip)
        {
            // materialのFloat(_Flip)を変更して本をめくらせる
            bookimage.material.SetFloat("_Flip", bookimage.material.GetFloat("_Flip") - Time.deltaTime * 4);
            _imageFlipTime += Time.deltaTime;

            // 本をめくる時間が0.6秒を超えたとき
            if (!(_imageFlipTime < 0.6f && _imageFlipTime > -0.6f) && !_isLoaded)
            {
                // 本をめくる処理の終了
                _isNotFlip = true;
            }
        }
    }

    /// <summary>
    /// カーソルの点滅処理
    /// </summary>
    private void CursolTransparency()
    {
        float a = Mathf.Sin(_selectorFlashCycle * Mathf.PI);
        _selectorFlashCycle += Time.deltaTime;
        _selectorColorTransparency = _selectorImage.color;

        // カーソルを透明にさせないための処理
        if (a >= 0.65f)
        {
            _selectorColorTransparency.a = a;
        }
        // カーソルに代入
        _selectorImage.color = _selectorColorTransparency;
    }

    /// <summary>
    /// Playerの入力処理
    /// </summary>
    private void InputSelector()
    {
        // 決定
        if (_inputs.UI.Decide.triggered)
        {
            _isInput[1] = true;
        }

        // 上
        if (_inputs.UI.Select_Vertical.ReadValue<float>() > 0)
        {
            // upが0でないとき
            if (_select[_nowSelectNumber - ONE].up != 0)
            {
                // 次に移動する場所を代入
                _nowSelectNumber = _select[_nowSelectNumber - ONE].up;
                _isInput[0] = true;

                // Audio再生
                audios.uiSE = (AudioManager.UISE)0;
                audios.AudioChanger("UI");
            }
        }
        // 下
        else if (_inputs.UI.Select_Vertical.ReadValue<float>() < 0)
        {
            // downが0でないとき
            if (_select[_nowSelectNumber - ONE].down != 0)
            {
                // 次に移動する場所を代入
                _nowSelectNumber = _select[_nowSelectNumber - ONE].down;
                _isInput[0] = true;

                // Audio再生
                audios.uiSE = (AudioManager.UISE)0;
                audios.AudioChanger("UI");
            }
        }
        // 右
        else if (_inputs.UI.Select_Horizontal.ReadValue<float>() > 0)
        {
            // rightが0でないとき
            if (_select[_nowSelectNumber - ONE].right != 0)
            {
                // 次に移動する場所を代入
                _nowSelectNumber = _select[_nowSelectNumber - ONE].right;
                _isInput[0] = true;

                // Audio再生
                audios.uiSE = (AudioManager.UISE)0;
                audios.AudioChanger("UI");
            }
        }
        // 左
        else if (_inputs.UI.Select_Horizontal.ReadValue<float>() < 0)
        {
            // leftが0でないとき
            if (_select[_nowSelectNumber - ONE].left != 0)
            {
                // 次に移動する場所を代入
                _nowSelectNumber = _select[_nowSelectNumber - ONE].left;
                _isInput[0] = true;

                // Audio再生
                audios.uiSE = (AudioManager.UISE)0;
                audios.AudioChanger("UI");
            }
        }
    }

    /// <summary>
    /// カーソルのリサイズ処理
    /// </summary>
    protected void SelectorResize()
    {
        // 指定時間内にリサイズ
        if (_selectorResizeProgressTime < 1)
        {
            // リサイズ経過時間の加算、倍率は増やせば早くなる
            _selectorResizeProgressTime += Time.deltaTime * _selectorMagnitude;
            _selector.anchoredPosition = Vector2.Lerp(_selector.anchoredPosition, _selectPoint[_nowSelectNumber - ONE].anchoredPosition, _selectorResizeProgressTime);
            _selector.sizeDelta = Vector2.Lerp(_selector.sizeDelta, _selectPoint[_nowSelectNumber - ONE].sizeDelta * _selectorSizeDeltaMagnitude, _selectorResizeProgressTime);
        }

        // リサイズ完了、次の入力受け付けていい
        if (_selectorResizeProgressTime >= 1)
        {
            _selectorResizeProgressTime = 0;
            _isInput[0] = false;
        }
    }

    /// <summary>
    /// 仮処理、マウスカーソルの場所にカーソルを動かす
    /// </summary>
    /// <param name="objName">次に動かすべきオブジェクトの名前</param>
    [CalledFromSendMessage]
    public void MouseMove(string objName)
    {
        Debug.Log(objName);
        // 配列からオブジェクト探索
        for (int i = 0; i < _select.Length; i++)
        {
            if (_selectPoint[i].name == objName)
            {
                _nowSelectNumber = i + 1;
                _isInput[0] = true;
                break;
            }
        }
    }

    /// <summary>
    /// 仮処理、マウスでの決定
    /// </summary>
    /// <param name="objName">次に動かすべきオブジェクトの名前</param>
    public void MouseDecide(string objName)
    {
        _isInput[1] = true;
    }

    protected virtual void OnEnable()
    {
        _inputs.Enable();
    }
    private void OnDisable()
    {
        _inputs.Disable();
    }
}

[System.Serializable]
public class SelectorMove_2
{
    public int left;
    public int right;
    public int up;
    public int down;
}