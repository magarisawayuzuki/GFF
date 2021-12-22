using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    private Slider _slider;
    private CharaParameter _chara;
    private int _beforeLife;

    private void Awake()
    {
        _slider = this.GetComponentInChildren<Slider>();
        _chara = this.transform.parent.GetComponent<CharaParameter>();
        _beforeLife = _chara.life;
    }

    private void Update()
    {
        ChangeLife(_chara.life);
    }

    private void ChangeLife(int nowLife)
    {
        // beforelifeからnowlifeの差分をlerpで動かす
        _slider.value = nowLife / _chara.life;
    }
}
