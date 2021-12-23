using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

[AttributeUsage(AttributeTargets.Method)]
public class CalledFromSendMessageAttribute : Attribute { }

public class UIController_2 : MonoBehaviour
{
    protected InputController _inputs = default;
    protected UISceneManager_2 sceneMan = default;

    [SerializeField] SelectorMove_2[] _select = default;
    protected int _nowSelectNumber = 1;
    [SerializeField] protected RectTransform[] _selectPoint = default;

    [SerializeField] protected RectTransform _selector = default;
    [SerializeField] private float _selectorMagnitude = default;
    private float _selectorResizeProgressTime = default;
    protected float _selectorSizeDeltaMagnitude = 1.1f;

    /// <summary>
    /// 0上下左右 1decide
    /// </summary>
    protected bool[] _isInput = { false, false };

    private const int ONE = 1;

    [SerializeField] private Image _selectorImage = default;
    private Color _selectorColorTransparency = default;
    private float _selectorFlashCycle = 1;

    [SerializeField] protected Image bookimage;
    protected float _imageFlipTime = default;
    protected bool _isFlip = default;
    protected bool _isLoaded = default;

    protected virtual void Awake()
    {
        bookimage.material.SetFloat("_Flip", 1f);

        _inputs = new InputController();
        sceneMan = gameObject.AddComponent<UISceneManager_2>();
    }

    protected void InputManager()
    {
        if (_isFlip)
        {
            if (!_isInput[0] && !_isInput[1])
            {
                InputSelector();
                var a = _selectorImage.color.a;
                a = Mathf.Sin(_selectorFlashCycle * Mathf.PI);
                _selectorFlashCycle += Time.deltaTime;
                _selectorColorTransparency = _selectorImage.color;
                if (a >= 0.65f)
                {
                    _selectorColorTransparency.a = a;
                }
                _selectorImage.color = _selectorColorTransparency;
            }
            else if (_isInput[0] && !_isInput[1])
            {
                SelectorResize();
            }
        }
        else if(!_isFlip)
        {
            bookimage.material.SetFloat("_Flip", bookimage.material.GetFloat("_Flip") - Time.deltaTime * 4);
            _imageFlipTime += Time.deltaTime;
            if (!(_imageFlipTime < 0.6f && _imageFlipTime > -0.6f) && !_isLoaded)
            {
                _isFlip = true;
            }
        }
    }

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
            if (_select[_nowSelectNumber - ONE].up != 0)
            {
                _nowSelectNumber = _select[_nowSelectNumber - ONE].up;
                _isInput[0] = true;
            }
        }
        // 下
        else if (_inputs.UI.Select_Vertical.ReadValue<float>() < 0)
        {
            if (_select[_nowSelectNumber - ONE].down != 0)
            {
                _nowSelectNumber = _select[_nowSelectNumber - ONE].down;
                _isInput[0] = true;
            }
        }
        // 右
        else if (_inputs.UI.Select_Horizontal.ReadValue<float>() > 0)
        {
            if (_select[_nowSelectNumber - ONE].right != 0)
            {
                _nowSelectNumber = _select[_nowSelectNumber - ONE].right;
                _isInput[0] = true;
            }
        }
        // 左
        else if (_inputs.UI.Select_Horizontal.ReadValue<float>() < 0)
        {
            if (_select[_nowSelectNumber - ONE].left != 0)
            {
                _nowSelectNumber = _select[_nowSelectNumber - ONE].left;
                _isInput[0] = true;
            }
        }
    }

    protected void SelectorResize()
    {
        if (_selectorResizeProgressTime < 1)
        {
            _selectorResizeProgressTime += Time.deltaTime * _selectorMagnitude;
            _selector.anchoredPosition = Vector2.Lerp(_selector.anchoredPosition, _selectPoint[_nowSelectNumber - 1].anchoredPosition, _selectorResizeProgressTime);
            _selector.sizeDelta = Vector2.Lerp(_selector.sizeDelta, _selectPoint[_nowSelectNumber - 1].sizeDelta * _selectorSizeDeltaMagnitude, _selectorResizeProgressTime);
        }

        if (_selectorResizeProgressTime >= 1)
        {
            _selectorResizeProgressTime = 0;
            _isInput[0] = false;
        }
    }

    [CalledFromSendMessage]
    public void MouseMove(string objName)
    {
        Debug.Log(objName);
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