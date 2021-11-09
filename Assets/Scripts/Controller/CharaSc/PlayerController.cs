using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユーザーのインプットを親に返す
/// </summary>
public class PlayerController : CharacterController
{
    private InputSystem IC;

    private void Awake()
    {
        IC = new InputSystem();
    }


    //=====================================================


    //ユーザーの入力を追記または上書き
    public override PlayerInput InputMethod()
    {
        
        return input;
    }

    //攻撃の追記とかあれば
    public override void Attack()
    {
        base.Attack();
    }

    //プレイヤーが死んだときの処理
    public override void Death()
    {
        base.Death();
    }

    //回避挙動
    protected void Evasion()
    {

    }

    //付近の記憶の欠片を自動取得
    protected void MemoryGet()
    {

    }

    //記憶ゲージの管理
    protected void MomoryGauge()
    {

    }

    //==================InputSystem========================

    private void OnEnable()
    {
        IC.Enable();
    }
    private void OnDisable()
    {
        IC.Disable();
    }

    //=====================================================
}
