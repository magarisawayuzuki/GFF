using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGame_2 : Chara_2
{
    [SerializeField] private float _aaa = default;
    [SerializeField] private CharaParameter playerPara = default;
    [SerializeField] private float _bbb = default;

    [SerializeField] private RectMask2D _memoryGaugeBar = default;

    [SerializeField] private GameObject[] weaponImage = default;

    private bool _isDamage = default;
    private float _DamageTime = default;
    private int beforeLifeChild = 0;

    private void Awake()
    {
        //HPScroll = this.GetComponentInChildren<RectMask2D>();
        _isDamage = false;
    }

    private void Start()
    {
        SetChara(playerPara);
        beforeLifeChild = playerPara.life;
    }

    private void FixedUpdate()
    {
        if(beforeLifeChild != /*playerPara.*/life)
        {
            _isDamage = true;
            _DamageTime = 0;
            beforeLifeChild = /*playerPara.*/life;
        }

        if (_isDamage)
        {
            ChangeLife(playerPara, true);
            _DamageTime += Time.deltaTime;

            if(_DamageTime >= 2)
            {
                _isDamage = false;
            }
        }
        else
        {
            ChangeLife(playerPara, false);
        }
    }
}
