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

    /// <summary>
    /// float
    /// </summary>
    private float _swordTime = 0;
    private float _hammerTime = 0;


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
        _ = base.InputMethod();

        // 入力値を _xに入れる
        input._x = IC.Player.Move.ReadValue<float>();

        if (IC.Player.Jump.triggered)
        {
            input._isJump = true;
        }

        // 左クリックで剣攻撃
        if (IC.Player.SwordAttack.ReadValue<float>() == 1)
        {
            _swordTime += Time.deltaTime;
        }
        else
        {
            _swordTime = 0;
        }

        if (IC.Player.HammerAttack.ReadValue<float>() == 1)
        {
            _swordTime += Time.deltaTime;
        }
        else
        {
            _hammerTime = 0;
        }

        /*
        charaStatus = CharacterStatus.hammerAttack;
        input._isAttack = true;
        */

        return input;
    }


    //=========================================================

    /*
    //移動
    public override void Move()
    {
        Debug.Log("aaaaaa");
        base.Move();
    }
    */

    //==========================================================

    //攻撃の追記とかあれば
    public override void Attack()
    {
        /*
        // 攻撃力を入力
        if (charaStatus == CharacterStatus.swordAttack)
        {
            _attackPower = charaData.basicPower;
        }
        else
        {
            _attackPower = charaData.basicPower * 1.5f;
        }
        */
        base.Attack();
    }


    //==========================================================


    //プレイヤーが死んだときの処理
    public override void Death()
    {
        base.Death();
    }


    //==========================================================


    //回避挙動
    protected void Evasion()
    {

    }


    //==========================================================


    //付近の記憶の欠片を自動取得
    protected void MemoryGet()
    {

    }


    //==========================================================


    //記憶ゲージの管理
    protected void MomoryGauge()
    {

    }


    //==========================================================


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
