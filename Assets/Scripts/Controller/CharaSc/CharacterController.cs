using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラに共通する処理を記述
/// </summary>
public class CharacterController : MonoBehaviour
{
    [SerializeField]
    protected CharaParameter charaData = default;

    protected CharacterAnimationController charaAnimCtrl;

    /*
    [SerializeField]
    Weapons[] weapon = null;
    
    [SerializeField]
    AnimationSpriteData animData = null;
    */
    [SerializeField]
    AnimationCurve jumpCurve = default;

    [SerializeField]
    protected PlayerInput input = default;
    protected SpriteRenderer spriteRenderer = default;

    // Charaのstatsを入れる
    protected CharacterStatus _charaStatus = default;
    public CharacterStatus characterStatus { get { return _charaStatus; } }
    public CharacterStatus old_charaStatus = default;

    protected RaycastHit grounddistance;

    #region Vector3
    // キャラの移動
    protected Vector3 CharacterMove = new Vector3();
    [SerializeField,Header("オブジェクトの大きさ")]
    protected Vector3 ObjectScale = new Vector3(0.25f,0);
    #endregion

    #region int
    /*
    private int _anim = 0;
    private int[] _maxAnimationCount = { 4, 8, 5, 13, 0 };
    */
    protected int _weapon = 0;
    protected int _knockBack = 0;
    protected float _damage = 0;
    #endregion

    #region float
    // 移動毒度低下時間
    protected float _speedDownTimer = 0;
    // 攻撃力
    protected float _attackPower = 0;
    // Rayの長さ
    private float[] _animationTime = { 0, 0, 0, 0, 0 };
    [SerializeField]
    protected float _groundDistance = 1.3f;

    [SerializeField]protected float _life = 0;

    public float GetLife{ get { return _life; }}

    #endregion

    #region bool
    // Damage状態
    protected bool _isDamage = false;
    // 落下状態
    protected bool _isFall = false;
    // 死亡判定
    [SerializeField]protected bool _isDeath = false;
    // 着地判定
    protected bool _isGround = false;
    // 記憶を持っているか
    protected bool _hasMemory = false;
    protected bool _isSpeedDown = false;
    // 無双状態
    protected bool _isPeerless = false;
    // 無敵状態
    protected bool _isInvincible = false;

    // 右に移動できるか
    protected bool _canNotRight = default;
    // 左に移動できるか
    protected bool _canNotLeft = default;

    // 落下死判定
    protected bool _isFallDeath = false;
    #endregion

    #region const
    protected const int _ZERO = 0;
    protected const int _ONE = 1;
    protected const int _TWO = 2;

    private const int layerMix = 1 << 8 | 1 << 11;
    private const int layerGround = 1 << 8;
    #endregion

    #region Jumpメソッド変数
    //加速度
    private float acceleration = default;
    //ジャンプを開始するフラグ
    private bool _startJump = false;
    //ジャンプ中のフラグ
    private bool _nowJump = false;
    //ジャンプの加速値
    private float jumpAccelerationValue = default;
    //落下の加速値
    private float fallAccelerationValue = default;
    //ジャンプ時間
    private float jumpTimeCount = default;
    //落下時間
    private float fallTimeCount = default;

    private const float TO_CEILING_RAY_LENGTH = 1.1f;
   
    [Header("ジャンプの速度倍率"), SerializeField]
    private float jumpSpeedScale = 1;
    #endregion

    //==========================================================


    /// <summary>
    /// CharaStatus(Charaの状態)
    /// </summary>
    public enum CharacterStatus
    {
        Idle,
        Move,
        Jump,
        Fall,
        swordAttack,
        hammerAttack,
        Death,
        Damage,
    }


    //==========================================================


    protected virtual void Awake()
    {
        _life = charaData.life;
    }

    protected virtual void Start()
    {
        charaAnimCtrl = this.GetComponentInChildren<CharacterAnimationController>();
    }

    //==========================================================


    /// <summary>
    /// 処理を呼び出す
    /// </summary>
    protected virtual void Update()
    {
        if (!InGameToPauseUI_2._isStaticPause)
        {
            input = InputMethod();

            // 移動
            Move();

            // CharacterAnimation
            //CharaViewControll();

            // ジャンプ処理
            Jump();

            //ジャンプの加速度をキャラのVector.yに
            CharacterMove.y = acceleration;

            // _isAttackがtrueの時攻撃
            if (input._isAttack)
            {
                Attack();
            }

        }
    }

    protected void FixedUpdate()
    {
        if (!InGameToPauseUI_2._isStaticPause)
        {
            // velocityへ入れる
            transform.position += CharacterMove;
        }
    }


    //=================================================================================


    /// <summary>
    /// 子クラスからの入力を受ける
    /// </summary>
    /// <returns></returns>
    public virtual PlayerInput InputMethod()
    {
        if (input._x != 0 && !input._isJump && !_startJump && !_nowJump && !input._isAttack && !_isDeath && !_isFall && !_isDamage)
        {
            _charaStatus = CharacterStatus.Move;
        }
        else if(!input._isJump && !_startJump && !_nowJump && !input._isAttack && !_isDeath && !_isFall && !_isDamage)
        {
            _charaStatus = CharacterStatus.Idle;
        }
        return null;
    }

    //=================================================================================


    /// <summary>
    /// プレイヤーの挙動で記述しておき、特殊な敵は子クラスで書き換える
    /// </summary>
    public virtual void Move()
    {

        if (_canNotRight && input._x > _ZERO)
        {
            input._x = _ZERO;
        }
        if (_canNotLeft && input._x < _ZERO)
        {
            input._x = _ZERO;
        }

        // 攻撃状態じゃなければ移動可
        if (!input._isAttack)
        {
            if (_isPeerless)
            {
                CharacterMove.x = input._x * 0.2f * 1.5f;
            }
            else
            {
                CharacterMove.x = input._x * 0.2f;
            }
        }
        else
        {
            CharacterMove.x = _ZERO;
        }
    }


    //==================================================================================


    /// <summary>
    /// 基本的な攻撃挙動のみ記述しておき、子クラスで書き換える
    /// </summary>
    public virtual void Attack()
    {

    }


    //===================================================================================


    /// <summary>
    /// ジャンプ関連の定義や状態の判定
    /// </summary>
    private void Jump()
    {
        #region Enum
        if (input._isJump | _startJump | _nowJump)
        {
            _charaStatus = CharacterStatus.Jump;
        }
        #endregion

        //攻撃中は停止
        if (input._isAttack)
        {
            StopJump();
            CharacterMove.y = _ZERO;
            return;
        }
        
        //上昇中には出さない
        // LayerMaskがGroundもしくはだったら着地ThroughGroundなら
        if (acceleration <= 0 && Physics.BoxCast(transform.position, ObjectScale, Vector3.down, out RaycastHit hit, Quaternion.identity, _groundDistance, layerMix))
        {
            //透過床の上でダウンが押された
            if (input._isDown && hit.collider.gameObject.layer == 11)
            {
                //落下
                CharaFallProcess();
                return;
            }

            // Playerで使用
            grounddistance = hit;

            _isGround = true;
            _isFall = false;
            input._isDown = false;
            acceleration = _ZERO;
            fallTimeCount = _ZERO;
            //this.transform.position = new Vector3(transform.position.x, hit.point.y + 0.5f, transform.position.z);

            //ジャンプ中に着地なら強制終了
            if (_nowJump)
            {
                _nowJump = false;
                jumpTimeCount = _ZERO;
            }
        }
        else
        {
            _isGround = false;

            //空中の入力を破棄する
            if (input._isJump)
            {
                input._isJump = false;
            }
        }


        //ジャンプ中ならジャンプ処理を呼び、この命令以下の判定は行わない
        if (_startJump || _nowJump)
        {

            //ジャンプ中のみ天井に向けRayを出す
            if (Physics.BoxCast(transform.position, ObjectScale, Vector3.up, Quaternion.identity, TO_CEILING_RAY_LENGTH, LayerMask.GetMask("Ground")))
            {
                acceleration = _ZERO;
                jumpTimeCount = _ZERO;
                _nowJump = false;
                _startJump = false;
                return;
            }

            //加速値を計算する
            AccelerationValueCalculation();
            return;
        }

        //着地状態
        if (_isGround)
        {
            //動かさない
            CharacterMove.y = _ZERO;

            //ジャンプ可能なタイミングでジャンプが押された
            if (!input._isAttack && input._isJump)
            {
                //ジャンプ開始
                _charaStatus = CharacterStatus.Jump;
                _startJump = true;
                input._isJump = false;
            }
        }
        //非着地状態
        else
        {
            //落下
            CharaFallProcess();
        }
    }

    /// <summary>
    /// ジャンプを一時停止する際に呼び出す
    /// </summary>
    private void StopJump()
    {
        _nowJump = false;
        _startJump = false;

        acceleration = _ZERO;

        jumpTimeCount = _ZERO;
        fallTimeCount = _ZERO;
    }

    /// <summary>
    /// ジャンプ処理の加速値を計算し加速度に加算する
    /// </summary>
    /// <param name="returnValue"></param>
    /// <returns></returns>
    private void AccelerationValueCalculation()
    {
        const int MAX_JUMP_TIME_COUNT = 3;
        const float MINIMUM_JUMP_TIME_COUNT = 0.1f;

        //規定時間よりも長くジャンプしていたら強制終了
        if (jumpTimeCount >= MAX_JUMP_TIME_COUNT)
        {
            _nowJump = false;
            jumpTimeCount = _ZERO;
            acceleration = _ZERO;
            return;
        }
        //最低ジャンプ継続時間(Rayによる誤判定を防ぐ)
        else if (_startJump && jumpTimeCount >= MINIMUM_JUMP_TIME_COUNT)
        {
            _startJump = false;
            _nowJump = true;
        }

        //カーブの値を加算する
        jumpTimeCount += Time.deltaTime;
        jumpAccelerationValue = jumpSpeedScale * jumpCurve.Evaluate(jumpTimeCount);

        //加速度にジャンプの加速値を加算する
        acceleration = jumpAccelerationValue;
    }

    /// <summary>
    /// キャラの落下処理(擬似重力処理)の加速値を計算し加速度から減算する
    /// </summary>
    private void CharaFallProcess()
    {
        #region Enum
        _isFall = true;
        _charaStatus = CharacterStatus.Fall;
        #endregion

        //落下時間
        fallTimeCount += Time.deltaTime;

        //重力計算式 時間s*重力加速度m/s²=速度m/s
        fallAccelerationValue = (fallTimeCount * 9.8f) / 6;

        //加速度にジャンプの加速値を加算する
        acceleration = -fallAccelerationValue;
    }


    //=====================================================================


    public virtual void Death()
    {
        _isDeath = true;
        _charaStatus = CharacterStatus.Death;
    }


    //=====================================================================


    /// <summary>
    /// 自機のライフ計算を行う
    /// </summary>
    /// <param name="power"></param>
    public virtual void CharaLifeCalculation(float damage, int knockBack, int weapon)
    {
        _isDamage = true;
        if (!input._isAttack)
        {
            _charaStatus = CharacterStatus.Damage;
        }

        if (!_isDeath)
        {
            _life -= (int)Mathf.Floor(damage);

            if (_life <= 0)
            {
                Death();
            }

        }
    }
    //=====================================================================


    /// <summary>
    /// 自機の状態からViewクラスを管理
    /// </summary>
    private void CharaViewController()
    {

    }
}


//=================================================================================


/// <summary>
/// inputを可視化するための変数定義クラス
/// </summary>
[System.Serializable]
public class PlayerInput
{
    public float _x = 0;
    public float _wasx = 0;
    public bool _isAttack = false;
    public bool _isJump = false;
    public bool _isDown = false;
}

[System.Serializable]
public class Weapons
{
    public string name;
    public WeaponParameter param;
}