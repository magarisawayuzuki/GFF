using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHPUIManager : Chara_2
{
    private int _beforeLifeChild = default;
    private Boss _bossCon = default;

    private bool _isDamage = default;
    private float _DamageTime = default;

    private void Start()
    {
        // PlayerControllerの取得 HPBarの設定
        _bossCon = GameObject.FindGameObjectWithTag("BossObj").GetComponent<Boss>();
        SetChara(_bossCon);
        _beforeLifeChild = (int)_bossCon.GetLife;
    }

    private void Update()
    {
        BossHPUI();
    }

    /// <summary>
    /// HPBarの変更処理
    /// </summary>
    private void BossHPUI()
    {
        // ダメージを受けたかの判定
        if (_beforeLifeChild != (int)_bossCon.GetLife)
        {
            _isDamage = true;
            _DamageTime = 0;
            _beforeLifeChild = (int)_bossCon.GetLife;
        }

        // 一気に減らす
        if (_isDamage)
        {
            ChangeLife(_bossCon, true);
            _DamageTime += Time.deltaTime;

            if (_DamageTime >= 0.5f)
            {
                _isDamage = false;
            }
        }
        // 差分をLerpで減らす
        else
        {
            ChangeLife(_bossCon, false);
        }
    }

    private void OnEnable()
    {
        _bossCon = GameObject.FindGameObjectWithTag("BossObj").GetComponent<Boss>();
    }
}
