using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユーザーのインプットを親に返す
/// </summary>
public class PlayerController : CharacterController
{
    private InputSystem IC;
    private RaycastHit _attackHit = default;

    #region Vecter3
    // 攻撃の範囲
    private Vector3 _attackScale = default;
    #endregion

    #region int

    #endregion

    #region float
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
    // Objectの半径
    private float _sidedistance = 0.5f;
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
            input._isAttack = false;
        }
    }

    //=====================================================


    //ユーザーの入力を追記または上書き
    public override PlayerInput InputMethod()
    {
        _ = base.InputMethod();

        // 入力値を _xに入れる
        input._x = IC.Player.Move.ReadValue<float>();

        _canNotRight = Physics.BoxCast(transform.position, new Vector3(0, 2, 0), Vector3.right, Quaternion.identity, _sidedistance);
        _canNotLeft = Physics.BoxCast(transform.position, new Vector3(0, 2, 0), Vector3.left, Quaternion.identity, _sidedistance);

        if (_canNotRight && input._x > 0)
        {
            input._x = 0;
        }
        if (_canNotLeft && input._x < 0)
        {
            input._x = 0;
        }

        if (IC.Player.Jump.triggered)
        {
            input._isJump = true;
        }

        if (IC.Player.invincible.triggered && _memoryGauge == _MAXMEMORYGAUGE)
        {
            _isInvincible = false;
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
                _weapon = 0;
                charaStatus = CharacterStatus.swordAttack;
            }
        }
        #endregion

        // 右クリックで槌攻撃
        #region 槌攻撃入力時間加算
        if (IC.Player.HammerAttack.phase == UnityEngine.InputSystem.InputActionPhase.Started)
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
                _weapon = 1;
                charaStatus = CharacterStatus.hammerAttack;
            }
        }
        #endregion

        return input;
    }


    //==========================================================


    //攻撃の追記とかあれば
    public override void Attack()
    {
        #region 敵の状態判定
        _isHit = Physics.BoxCast(transform.position, _attackScale, Vector3.right, out _attackHit, Quaternion.identity, _ONE);
        #endregion

        #region 攻撃力代入
        // 攻撃力を入力
        if (_isInvincible)
        {
            _attackPower = charaData.basicPower * 6;
        }
        else
        {
            if (charaStatus == CharacterStatus.swordAttack)
            {
                if (_swordTime > _MIDDLEPOWERTIME)
                {
                    _attackPower = charaData.basicPower * 3;
                    Debug.Log("剣強攻撃");
                    _isInputSwordAttack = false;
                }
                else if (_swordTime > _NORMALPOWERTIME)
                {
                    _attackPower = charaData.basicPower * 2;
                    Debug.Log("剣中攻撃");
                    _isInputSwordAttack = false;
                }
                else
                {
                    _attackPower = charaData.basicPower;
                    Debug.Log("剣弱攻撃");
                    _isInputSwordAttack = false;
                }
            }
            else if (charaStatus == CharacterStatus.hammerAttack)
            {
                // 槌協攻撃2段階目
                if (_hammerTime > _MIDDLEPOWERTIME)
                {
                    _attackPower = charaData.basicPower * 4.5f;
                    Debug.Log("槌強攻撃");
                    _isInputHammerAttack = false;
                }
                // 槌強攻撃1段階目
                else if (_hammerTime > _NORMALPOWERTIME)
                {
                    _attackPower = charaData.basicPower * 3;
                    Debug.Log("槌中攻撃");
                    _isInputHammerAttack = false;
                }
                // 槌弱攻撃
                else
                {
                    _attackPower = charaData.basicPower * 1.5f;
                    Debug.Log("槌弱攻撃");
                    _isInputHammerAttack = false;
                }
            }
        }
        #endregion

        if (_isHit)
        {
            if (_attackHit.collider.tag == "Normal")
            {
                Debug.Log("ふつう当たった");
                _attackHit.collider.GetComponent<EnemyNormal>().CharaLifeCalculation(_damage, _knockBack, _weapon);
                _isHit = false;
            }
            else if (_attackHit.collider.tag == "Soft")
            {
                Debug.Log("やわらかい当たった");
                _attackHit.collider.GetComponent<EnemyPlant>().CharaLifeCalculation(_damage, _knockBack, _weapon);
                _isHit = false;
            }
            else if (_attackHit.collider.tag == "Hard")
            {
                Debug.Log("硬い当たった");
                _attackHit.collider.GetComponent<EnemyRock>().CharaLifeCalculation(_damage, _knockBack, _weapon);
                _isHit = false;
            }
        }
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
        if (_memoryDownTimer > _ZERO)
        {
            _memoryDownTimer -= Time.deltaTime;
        }
        else
        {
            _memoryGauge -= _TIMEMEMORYDOWN;
            _memoryDownTimer = _ONE;
        }

        if (_memoryGauge > _MAXMEMORYGAUGE)
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
            else if (charaStatus == CharacterStatus.hammerAttack)
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
