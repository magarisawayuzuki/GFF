using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chara : InGame
{
    protected Slider _slider;
    protected CharaParameter _chara;
    protected int _beforeLife;

    protected override void Awake()
    {
        base.Awake();
        _slider = this.GetComponentInChildren<Slider>();
    }

    protected void ChangeLife(int nowLife)
    {
        // beforelifeからnowlifeの差分をlerpで動かす
        _slider.value = nowLife / _chara.life;
    }
}
