using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScreenUI : Chara
{
    private int nowGaugeParcent;

    protected override void Awake()
    {
        base.Awake();
        nowGaugeParcent = 0;
    }

    private void Update()
    {
        InputManager();
        //ChangeMemoryGaugeUI();
        //ChangeWeaponUI();
        //ToPauseUI();
        //ChangeMemoryAchivementUI();
        //ChangePlayerLife();
    }

    protected override void InputManager()
    {
        base.InputManager();

        // 決定
        if (_isInput[0])
        {
            _isInput[0] = false;
        }

        // 上
        if (_isInput[1])
        {
            _isInput[1] = false;
        }

        // 下
        if (_isInput[2])
        {
            _isInput[2] = false;
        }

        // 右
        if (_isInput[3])
        {
            _isInput[3] = false;
        }

        // 左
        if (_isInput[4])
        {
            _isInput[4] = false;
        }
    }

    private void ChangeMemoryGaugeUI()
    {

    }

    private void ChangeWeaponUI()
    {

    }

    private void ToPauseUI()
    {
        ChangeSceneCall();
    }

    private void ChangeMemoryAchivementUI()
    {

    }

    private void ChangePlayerLife()
    {

    }
}
