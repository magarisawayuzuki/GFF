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
    private float _memoryGauge = 0;

    private const float _MAXPOWERTIME = 3;
    private const float _MIDDLEPOWERTIME = 1.5f;
    // 記憶ゲージ減らす量
    // 〇
    private const int _GOODMEMORYDOWN = 5;
    // △
    private const int _NORMALMEMORYDOWN = 1;
    // ×
    private const int _BADMEMORYDOWN = 1;
    //時間減少
    private const float _TIMEMEMORYDOWN = 0.5f;


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
            charaStatus = CharacterStatus.swordAttack;
            _swordTime += Time.deltaTime;
        }
        else
        {
            _swordTime = 0;
        }

        if (IC.Player.HammerAttack.ReadValue<float>() == 1)
        {
            charaStatus = CharacterStatus.hammerAttack;
            _swordTime += Time.deltaTime;
        }
        else
        {
            _hammerTime = 0;
        }

        if (IC.Player.HammerAttack.triggered)
        {
            input._isAttack = true;
        }

        return input;
    }


    //==========================================================

    //攻撃の追記とかあれば
    public override void Attack()
    {
        // 攻撃力を入力
        if (charaStatus == CharacterStatus.swordAttack)
        {
            if (_swordTime >= _MAXPOWERTIME)
            {
                _attackPower = charaData.basicPower * 3;
            }
            else if (_swordTime >= _MIDDLEPOWERTIME)
            {
                _attackPower = charaData.basicPower * 2;
            }
            else
            {
                _attackPower = charaData.basicPower;
            }
        }
        else
        {
            // 槌協攻撃2段階目
            if (_hammerTime >= _MAXPOWERTIME)
            {
                _attackPower = charaData.basicPower * 4.5f;
            }
            // 槌強攻撃1段階目
            else if (_hammerTime >= _MIDDLEPOWERTIME)
            {
                _attackPower = charaData.basicPower * 3;
            }
            // 槌弱攻撃
            else
            {
                _attackPower = charaData.basicPower * 1.5f;
            }
        }
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
