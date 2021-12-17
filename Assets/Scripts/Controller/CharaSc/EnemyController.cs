﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵機のインターフェース
/// </summary>
public class EnemyController : CharacterController
{
    protected float damageScaleSword;
    protected float damageScaleHammer;

    protected SpriteRenderer EnemySprite;　//  エネミー
    public GameObject player; //追跡ターゲット                              

    [SerializeField]
    public EnemyAnime Anime; // spriteデータ
    [SerializeField]
    public EnemyData data; //変数データ   
    [SerializeField]
    protected float AttackRange;   
    [SerializeField]
    protected int[] MaxLeng; //spriteのマックス
    [SerializeField]
    public float[] Spritetime;   // spriteループ用の変数

    public int anime;　//switch変数

    [HideInInspector]
    public bool _Retrcking;
    [HideInInspector]
    public bool _IsJump;
    [HideInInspector]
    public bool _IsAttack = true; //攻撃するか判定 
    [HideInInspector]
    public bool _IsReturn; //元の位置に戻っているか判定
    [HideInInspector]
    public float num = 1; //反転するときに使う数字（1で固定

    protected Vector2 pos;　//自身の位置
    protected Vector2 DefaultPos; //最初の位置
    protected int DefaultPosInt;
    protected float GetAttackRange;

    public bool _InEnemy;
    public bool _IsTrackingWait;
    public bool _IsTracking;
    public bool _IsMove;

    protected int EnemyPositionX = 0; //二次元配列の横
    protected int EnemyPositionY = 0; //二次元配列の縦

    protected Maping map;
        
    /// <summary>
    /// 子クラスのinputを用いてinputを返す。ここでは共通処理のみ記述可
    /// </summary>
    /// <returns></returns>
    public override PlayerInput InputMethod()
    {       
        return input;
    }

    protected override void Update()
    {
        base.Update();        
    }

    public override void CharaLifeCalculation(float damage, int knockBack, int weapon)
    {
        //攻撃された武器により倍率変更
        switch (weapon)
        {
            //剣
            case 0:
                damage *= damageScaleSword;
                break;

            //槌
            case 1:
                damage *= damageScaleHammer;
                break;
        }


        base.CharaLifeCalculation(damage, knockBack,weapon);
    }

    //記憶の欠片を落とす処理を追記
    public override void Death()
    {
        base.Death();


    }
   
}