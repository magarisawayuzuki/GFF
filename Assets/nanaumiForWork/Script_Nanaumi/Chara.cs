using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chara : InGame
{
    protected Slider slider;

    protected override void Awake()
    {
        base.Awake();
        slider = this.GetComponentInChildren<Slider>();
    }

    protected void ChangeLife(int nowLife)
    {

    }
}
