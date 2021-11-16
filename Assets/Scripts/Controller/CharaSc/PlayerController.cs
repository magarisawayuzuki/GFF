using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユーザーのインプットを親に返す
/// </summary>
public class PlayerController : CharacterController
{
    private InputSystem IC;

    // 現在武器
    private int _weaponNumber = 0;

    private void Awake()
    {
        IC = new InputSystem();
        rb = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //=====================================================


    //ユーザーの入力を追記または上書き
    public override PlayerInput InputMethod()
    {
        // 入力値を _xに入れる
        input._x = IC.Player.Move.ReadValue<float>();
        if (IC.Player.Jump.triggered)
        {
            input._isJump = true;
        }

        // 左クリックで剣攻撃
        if (IC.Player.SwordAttack.triggered)
        {
            input._isAttack = true;
        }
        if (IC.Player.HammerAttack.triggered)
        {

        }
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

    protected void Change()
    {
        if (_weaponMemoryCount > _weaponNumber)
        {
            _weaponNumber++;
        }
        else
        {
            _weaponNumber = _ZERO;
        }
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
