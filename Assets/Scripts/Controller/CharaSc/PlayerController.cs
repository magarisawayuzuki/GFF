﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユーザーのインプットを親に返す
/// </summary>
public class PlayerController : CharacterController
{
    private InputSystem IC;
    private RaycastHit _attackHit = default;

    private LayerMask movelayer = 1 << 8 | 1 << 9;

    #region Vecter3
    // 攻撃の範囲
    private Vector3 _attackScale = default;
    #endregion

    #region int
    // 武器の記憶の個数
    protected int _weaponMemoryCount = 0;
    public int _WeaponMemoryCount { get{ return _weaponMemoryCount; } }
    // 記憶の個数
    protected int _memoryCount = 0;

    #endregion

    #region float
    // 剣攻撃時間
    private float _swordTime = 0;
    // 槌攻撃経過時間
    private float _hammerTime = 0;
    //
    private float _hammerCoolDown = 3;

    [SerializeField,Header("記憶ゲージ")]
    private float _memoryGauge = 50;
    public float _MemoryGauge{ get { return _memoryGauge; } }

    // 記憶ゲージ減少時間
    private float _memoryDownTimer = 1;
    // Objectの半径
    private float _sidedistance = 0.5f;
    [SerializeField,Header("無双時間")]
    private float _invincibleTime = 5;

    // 攻撃力の記憶ゲージ倍率
    private float _memoryGaugeAttackPoint = 0;
    [SerializeField, Header("記憶ゲージ倍率")]
    private float _maxMemoryGaugeAttack = 1.5f;
    [SerializeField]
    private float _mediumMemoryGaugeAttack = 1.3f;
    [SerializeField]
    private float _minMemoryGaugeAttack = 1;

    [SerializeField, Header("基礎攻撃力の倍率")]
    // 剣中攻撃
    private float _swordMiddleAttack = 2;
    [SerializeField]
    // 剣強攻撃
    private float _swordHeavyAttack = 3;
    [SerializeField]
    // 槌弱攻撃
    private float _hammerLightAttack = 1.5f;
    [SerializeField]
    // 槌中攻撃
    private float _hammerMiddleAttack = 3f;
    [SerializeField]
    // 槌強攻撃
    private float _hammerHeavyAttack = 4.5f;

    [SerializeField, Header("無双時の攻撃力倍率")]
    private float _invincibleAttack = 6;
    #endregion

    #region bool
    // 硬い敵か 
    private bool _isHard = default;
    // 普通の敵か
    private bool _isNormal = default;
    // やわらかい敵か
    private bool _isSoft = default;

    // 攻撃が当たったかどうか
    protected bool _isHit = default;
    // 剣の攻撃入力判定
    private bool _isInputSwordAttack = default;
    // 槌の攻撃入力判定
    private bool _isInputHammerAttack = default;

    // 右に移動できるか
    private bool _canNotRight = default;
    // 左に移動できるか
    private bool _canNotLeft = default;

    // 槌攻撃のクールタイムが終わっているか
    private bool _canHammerAttack = true;
    #endregion

    #region const
    // 最大値
    private const int _MAXMEMORYGAUGE = 100;
    [SerializeField, Header("記憶ゲージ加算")]
    // 〇
    private const int _GOODMEMORYPLUS = 5;
    [SerializeField]
    // △
    private const int _NORMALMEMORYPLUS = 1;
    [SerializeField]
    // ×
    private const int _BADMEMORYPLUS = 1;

    // 時間で減少
    private const float _TIMEMEMORYDOWN = 0.5f;

    // 攻撃の押してる時間の最大値
    [SerializeField, Header("押している時間の最大値")]
    // 強攻撃2段階目
    private const float _MIDDLEPOWERTIME = 3;
    [SerializeField]
    // 強攻撃1段階目
    private const float _NORMALPOWERTIME = 1.5f;
    #endregion

    #region デバック用
    //攻撃時間
    private float _AttackTime = default;
    #endregion


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

        // デバッグ用
        if (input._isAttack)
        {
            _AttackTime += Time.deltaTime;
        }
        else
        {
            _AttackTime = _ZERO;
        }

        if (_AttackTime > _ONE)
        {
            _swordTime = _ZERO;
            _hammerTime = _ZERO;
            if (_isInputSwordAttack)
            {
                _isInputSwordAttack = false;
            }
            else
            {
                _isInputHammerAttack = false;
                _canHammerAttack = false;
            }
        }
    }

    //=====================================================


    //ユーザーの入力を追記または上書き
    public override PlayerInput InputMethod()
    {
        _ = base.InputMethod();

        // 入力値を_xに入れる
        input._x = IC.Player.Move.ReadValue<float>();

        _canNotRight = Physics.BoxCast(transform.position, new Vector3(0, 2, 0), Vector3.right, Quaternion.identity, _sidedistance, movelayer);
        _canNotLeft = Physics.BoxCast(transform.position, new Vector3(0, 2, 0), Vector3.left, Quaternion.identity, _sidedistance, movelayer);

        if (_canNotRight && input._x > _ZERO)
        {
            input._x = _ZERO;
        }
        if (_canNotLeft && input._x < _ZERO)
        {
            input._x = _ZERO;
        }

        if (IC.Player.Jump.triggered)
        {
            input._isJump = true;
        }

        /*
        if (IC.Player.Down.triggered)
        {
            input._isDown = true;
        }
        */

        if (IC.Player.Invincible.triggered && _memoryGauge == _MAXMEMORYGAUGE)
        {
            Debug.Log("無双");
            _isInvincible = true;
        }

        // 左クリックで剣攻撃
        #region 剣攻撃入力時間加算
        if (IC.Player.SwordAttack.phase == UnityEngine.InputSystem.InputActionPhase.Started)
        {
            _swordTime += Time.deltaTime;

            if (!_isInputSwordAttack)
            {
                _isInputSwordAttack = true;
            }
        }
        else
        {
            if (_isInputSwordAttack)
            {
                input._isAttack = true;
                _weapon = _ZERO;
                charaStatus = CharacterStatus.swordAttack;
            }
        }
        #endregion

        #region 槌のクールダウン
        if (!_canHammerAttack)
        {
            _hammerCoolDown -= Time.deltaTime;
        }

        if (_hammerCoolDown <= _ZERO)
        {
            _canHammerAttack = true;
        }
        #endregion

        // 右クリックで槌攻撃
        #region 槌攻撃入力時間加算
        if (IC.Player.HammerAttack.phase == UnityEngine.InputSystem.InputActionPhase.Started　&& _canHammerAttack)
        {
            _hammerTime += Time.deltaTime;

            if (!_isInputHammerAttack)
            {
                _isInputHammerAttack = true;
            }
        }
        else
        {
            if (_isInputHammerAttack)
            {
                input._isAttack = true;
                _weapon = _ONE;
                charaStatus = CharacterStatus.hammerAttack;
            }
        }
        #endregion

        if (input._x != input._wasx)
        {
            _isSpeedDown = true;
            _speedDownTimer = _ZERO;
        }

        if (input._x == _ZERO || _speedDownTimer > _ONE)
        {
            _isSpeedDown = false;
        }

        return input;
    }


    //==========================================================


    //攻撃の追記とかあれば
    public override void Attack()
    {
        #region 敵の状態判定
        _isHit = Physics.BoxCast(transform.position, _attackScale, Vector3.right, out _attackHit, Quaternion.identity, _ONE, LayerMask.GetMask("Enemy"));
        #endregion

        #region 攻撃力代入
        if (_isHit)
        {
            if (_isInvincible)
            {
                _attackPower = charaData.basicPower * _invincibleAttack;
            }
            else
            {
                switch (charaStatus)
                {
                    case CharacterStatus.swordAttack:
                        if (_swordTime > _MIDDLEPOWERTIME)
                        {
                            _attackPower = charaData.basicPower * _swordHeavyAttack * _memoryGaugeAttackPoint;
                            Debug.Log("剣強攻撃");
                        }
                        else if (_swordTime > _NORMALPOWERTIME)
                        {
                            _attackPower = charaData.basicPower * _swordMiddleAttack * _memoryGaugeAttackPoint;
                            Debug.Log("剣中攻撃");
                        }
                        else
                        {
                            _attackPower = charaData.basicPower * _memoryGaugeAttackPoint;
                            Debug.Log("剣弱攻撃");
                        }
                        break;
                    case CharacterStatus.hammerAttack:
                        // 槌協攻撃2段階目
                        if (_hammerTime > _MIDDLEPOWERTIME)
                        {
                            _attackPower = charaData.basicPower * _hammerHeavyAttack * _memoryGaugeAttackPoint;
                            Debug.Log("槌強攻撃");
                        }
                        // 槌強攻撃1段階目
                        else if (_hammerTime > _NORMALPOWERTIME)
                        {
                            _attackPower = charaData.basicPower * _hammerMiddleAttack * _memoryGaugeAttackPoint;
                            Debug.Log("槌中攻撃");
                        }
                        // 槌弱攻撃
                        else
                        {
                            _attackPower = charaData.basicPower * _hammerLightAttack * _memoryGaugeAttackPoint;
                            Debug.Log("槌弱攻撃");
                        }
                        break;
                }
            }
        }
        #endregion

        #region 敵の種別分け
        if (_isHit)
        {
            if (_attackHit.collider.tag == "Normal")
            {
                Debug.Log("ふつう当たった");
                _attackHit.collider.GetComponent<EnemyNormal>().CharaLifeCalculation(_damage, _knockBack, _weapon);
            }
            else if (_attackHit.collider.tag == "Soft")
            {
                Debug.Log("やわらかい当たった");
                _attackHit.collider.GetComponent<EnemyPlant>().CharaLifeCalculation(_damage, _knockBack, _weapon);
            }
            else if (_attackHit.collider.tag == "Hard")
            {
                Debug.Log("硬い当たった");
                _attackHit.collider.GetComponent<EnemyRock>().CharaLifeCalculation(_damage, _knockBack, _weapon);
            }
            _isHit = false;
        }
        #endregion

        base.Attack();
    }


    //==========================================================


    //プレイヤーが死んだときの処理
    public override void Death()
    {
        base.Death();
        OnDisable();
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
        #region 記憶ゲージ倍率変更
        // 2段階目
        if (_memoryGauge > 30)
        {
            _memoryGaugeAttackPoint = _mediumMemoryGaugeAttack;

        }
        // 3段階目
        else if (_memoryGauge > 60)
        {
            _memoryGaugeAttackPoint = _maxMemoryGaugeAttack;
        }
        // 1段階目
        else
        {
            _memoryGaugeAttackPoint = _minMemoryGaugeAttack;
        }
        #endregion

        if (_isInvincible)
        {
            _memoryGauge = _MAXMEMORYGAUGE / _invincibleTime * Time.deltaTime;
        }
        else
        {
            // 時間での減少
            if (_memoryDownTimer > _ZERO)
            {
                _memoryDownTimer -= Time.deltaTime;
            }
            else
            {
                _memoryGauge -= _TIMEMEMORYDOWN;
                _memoryDownTimer = _ONE;
            }
        }

        #region 記憶ゲージ加算
        if (_isInvincible)
        {
            return;
        }
        else
        {
            //敵の種類によって加算する数を変える
            if (_memoryGauge < _MAXMEMORYGAUGE)
            {
                switch (charaStatus)
                {
                    case CharacterStatus.swordAttack:
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

                        if (_memoryGauge > _MAXMEMORYGAUGE)
                        {
                            _memoryGauge = _MAXMEMORYGAUGE;
                        }
                        break;
                    case CharacterStatus.hammerAttack:
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

                        if (_memoryGauge > _MAXMEMORYGAUGE)
                        {
                            _memoryGauge = _MAXMEMORYGAUGE;
                        }
                        break;
                }
            }
            else
            {
                return;
            }
        }
        #endregion

        if (_memoryGauge <= _ZERO && _isInvincible)
        {
            _isInvincible = false;
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
