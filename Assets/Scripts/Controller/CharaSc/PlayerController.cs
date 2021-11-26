using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユーザーのインプットを親に返す
/// </summary>
public class PlayerController : CharacterController
{
    private InputSystem IC;


    /// <summary>
    /// int
    /// </summary>
    // 現在武器
    private int _weaponNumber = 0;


    /// <summary>
    /// float
    /// </summary>
    // 武器の記憶の個数
    protected int _weaponMemoryCount = 0;
    // 記憶の個数
    protected int _memoryCount = 0;
    // 剣攻撃時間
    private float _swordTime = 0;
    // 槌攻撃経過時間
    private float _hammerTime = 0;
    // 記憶ゲージ
    private float _memoryGauge = 50;
    // 記憶ゲージ減少時間
    private float _memoryDownTimer = 1;


    /// <summary>
    /// bool
    /// </summary>
    private bool _isHard = false;
    private bool _isNormal = false;
    private bool _isSoft = false;

    /// <summary>
    /// const
    /// </summary>
    [Header("記憶ゲージ")]
    // 最大値
    private const int _MAXMEMORYCAUGE = 100;
    // 〇
    private const int _GOODMEMORYPLUS = 5;
    // △
    private const int _NORMALMEMORYPLUS = 1;
    // ×
    private const int _BADMEMORYPLUS = 1;
    // 時間で減少
    private const float _TIMEMEMORYDOWN = 0.5f;

    // 攻撃の押してる時間のMax値
    // 強攻撃2段階目
    private const float _MAXPOWERTIME = 3;
    // 強攻撃1段階目
    private const float _MIDDLEPOWERTIME = 1.5f;

    // 攻撃の距離
    private const float _ATTACKDISTANCE = 1.5f;


    //=====================================================


    private void Awake()
    {
        IC = new InputSystem();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //=====================================================


    protected override void Update()
    {
        base.Update();
        MomoryGauge();
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
        }
        else
        {
            _swordTime = 0;
        }

        if (IC.Player.HammerAttack.ReadValue<float>() == 1)
        {

        }
        else
        {
            _hammerTime = 0;
        }

        if (IC.Player.SwordAttack.phase == UnityEngine.InputSystem.InputActionPhase.Performed)
        {
            _swordTime += Time.deltaTime;
            charaStatus = CharacterStatus.swordAttack;

            if (!input._isAttack)
            {
                input._isAttack = true;
            }
        }
        else
        {

        }

        if (IC.Player.HammerAttack.triggered)
        {
            input._isAttack = true;
            _swordTime += Time.deltaTime;
            charaStatus = CharacterStatus.hammerAttack;
        }

        return input;
    }


    //==========================================================

    //攻撃の追記とかあれば
    public override void Attack()
    {
        _isHard = Physics.BoxCast(transform.position, Vector3.one, Vector3.right, Quaternion.identity, _ATTACKDISTANCE, LayerMask.GetMask("HardEnemy"));
        _isNormal = Physics.BoxCast(transform.position, Vector3.one, Vector3.right, Quaternion.identity, _ATTACKDISTANCE, LayerMask.GetMask("NormalEnemy"));
        _isSoft = Physics.BoxCast(transform.position, Vector3.one, Vector3.right, Quaternion.identity, _ATTACKDISTANCE, LayerMask.GetMask("SoftEnemy"));
        
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
        if (_memoryDownTimer >= _ZERO)
        {
            _memoryDownTimer -= Time.deltaTime;
        }
        else
        {
            _memoryGauge -= _TIMEMEMORYDOWN;
            _memoryDownTimer = _ONE;
        }

        if (_memoryGauge > _MAXMEMORYCAUGE)
        {
            if (charaStatus == CharacterStatus.swordAttack)
            {
                if (_isSoft)
                {
                    _memoryGauge += _GOODMEMORYPLUS;
                }
                else if (_isNormal)
                {
                    _memoryGauge += _NORMALMEMORYPLUS;

                }
                else if (_isHard)
                {
                    _memoryGauge += _BADMEMORYPLUS;
                }
            }
            else if (charaStatus == CharacterStatus.swordAttack)
            {
                if (_isHard)
                {
                    _memoryGauge += _GOODMEMORYPLUS;
                }
                else if (_isNormal)
                {
                    _memoryGauge += _NORMALMEMORYPLUS;
                }
                else if (_isSoft)
                {
                    _memoryGauge += _BADMEMORYPLUS;
                }
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }


    //==========================================================


    /// <summary>
    /// 武器切り替え
    /// </summary>
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
