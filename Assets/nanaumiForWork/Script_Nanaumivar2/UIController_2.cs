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
    protected InputController _inputs;
    protected UISceneManager_2 sceneMan;

    [SerializeField] SelectorMove_2[] _select;
    protected int _nowSelectNumber = 1;
    [SerializeField] protected RectTransform[] _selectPoint;

    [SerializeField] protected RectTransform _selector;
    [SerializeField] private float _selectorMagnitude;
    private float _selectorResizeProgressTime = 0;

    /// <summary>
    /// 0上下左右 1decide
    /// </summary>
    protected bool[] _isInput = { false, false };

    private const int ONE = 1;
    protected virtual void Awake()
    {
        _inputs = new InputController();
        //sceneMan = new UISceneManager_2();
        sceneMan = gameObject.AddComponent<UISceneManager_2>();
    }

    protected void InputManager()
    {
        if (!_isInput[0] && !_isInput[1])
        {
            InputSelector();
        }
        else if (_isInput[0] && !_isInput[1])
        {
            SelectorResize();
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
            _selector.sizeDelta = Vector2.Lerp(_selector.sizeDelta, _selectPoint[_nowSelectNumber - 1].sizeDelta, _selectorResizeProgressTime);
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