using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScreenUI : Chara
{
    private int nowGaugeParcent;
    [SerializeField, Tooltip("playerのタグを入れてください")] private string playerTag;

    protected override void Awake()
    {
        base.Awake();
        nowGaugeParcent = 0;
        _chara = GameObject.FindGameObjectWithTag(playerTag).GetComponent<CharaParameter>();
        _beforeLife = _chara.life;
    }

    private void Update()
    {
        // 親メソッド
        ChangeLife(_chara.life);

        // 子メソッド
        InputManager();
        //ChangeMemoryGaugeUI();
        //ChangeWeaponUI();
        //ChangeMemoryAchivementUI();
        //ChangePlayerLife();
    }

    protected override void InputManager()
    {
        if (_isPause)
        {
            base.InputManager();
        }
        else
        {
            if (_inputs.UI.Pause.triggered)
            {
                _isPause = true;
            }
        }
    }

    private void ChangeMemoryGaugeUI()
    {

    }

    private void ChangeWeaponUI()
    {

    }

    private void ChangeMemoryAchivementUI()
    {

    }

    private void ChangePlayerLife()
    {

    }
}