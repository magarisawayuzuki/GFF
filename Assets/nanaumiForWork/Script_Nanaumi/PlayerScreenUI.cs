using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScreenUI : Chara
{
    private int nowGaugeParcent;
    [SerializeField, Tooltip("playerのタグを入れてください")] private string playerTag;

    [SerializeField] private Text pauseText;

    protected override void Awake()
    {
        base.Awake();
        nowGaugeParcent = 0;
        //_chara = GameObject.FindGameObjectWithTag(playerTag).GetComponent<CharaParameter>();
        //_beforeLife = _chara.life;
    }

    private void Update()
    {
        // 親メソッド
        //ChangeLife(_chara.life);

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
            pauseText.text = "Pause";
            base.InputManager();
        }
        else
        {
            pauseText.text = "unPause";
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