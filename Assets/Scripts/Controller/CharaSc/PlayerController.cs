using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ユーザーのインプットを親に返す
/// </summary>
public class PlayerController : CharacterController
{
    private InputSystem IC;

    private LayerMask movelayer = 1 << 8 | 1 << 9;

    [SerializeField]
    private GameObject[] _memoryFragments = default;

    private CheckPointSystem checkPointSys;
    private MemoryAchievementController memoryAchievementController;

    #region Vecter3
    // 攻撃の範囲
    private Vector3 _attackScale = new Vector3(1, 2.5f);
    private Vector3 _playerScale = new Vector3(0, 2.5f);
    private Vector3 _attackDirection = new Vector3(1, 0);
    private Vector3 _playerFlontScale = new Vector3(1, 1);
    #endregion

    #region int
    // 武器の記憶の個数
    protected int _weaponMemoryCount = 0;
    public int _WeaponMemoryCount { get { return _weaponMemoryCount; } }
    // 記憶の個数
    protected int _memoryCount = 0;
    #endregion

    #region float
    // 剣攻撃時間
    private float _swordTime = 0;
    // 槌攻撃経過時間
    private float _hammerTime = 0;
    // 槌攻撃のクールダウン
    private float _hammerCoolDown = 3;

    [SerializeField, Header("記憶ゲージ")]
    private float _memoryGauge = 50;
    public float _MemoryGauge { get { return _memoryGauge; } }

    // 記憶ゲージ減少時間
    private float _memoryDownTimer = 1;
    // player
    private float _sidedistance = 0.45f;
    [SerializeField, Header("無双時間")]
    private float _peerlessTime = 5;
    [SerializeField, Header("無敵時間")]
    private float _invincibleTime = 0.5f;

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

    // 槌攻撃のクールタイムが終わっているか
    private bool _canHammerAttack = true;

    // 攻撃開始
    private bool _isStartAttack = false;

    #endregion

    #region string
    private string _soft = "Soft";
    private string _normal = "Normal";
    private string _hard = "Hard";
    private string _weaponMemory = "WeaponMemory";
    private string _eventSystem = "EventSystem";
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


    protected override void Awake()
    {
        base.Awake();
        IC = new InputSystem();
        checkPointSys = GameObject.FindWithTag(_eventSystem).GetComponent<CheckPointSystem>();
        memoryAchievementController = GameObject.FindWithTag(_eventSystem).GetComponent<MemoryAchievementController>();
    }

    //=====================================================


    protected override void Update()
    {
        base.Update();
        /*
        print("今" + _charaStatus);
        print("古い" + old_charaStatus);
        */
        ////前フレームと状態が違ったら
        if (old_charaStatus != _charaStatus)
        {
            //アニメーションを切り替える
            charaAnimCtrl.AnimationChenge(_charaStatus);
            old_charaStatus = _charaStatus;
        }

        Debug.Log(_canNotRight);

        MemoryGet();

        MomoryGauge();

        Timer();
    }

    //=====================================================


    //ユーザーの入力を追記または上書き
    public override PlayerInput InputMethod()
    {
        _ = base.InputMethod();

        // 入力値を_xに入れる
        input._x = IC.Player.Move.ReadValue<float>();

        if (input._x != 0 && !input._isAttack)
        {
            _playerFlontScale.x = -input._x;
            transform.localScale = _playerFlontScale;
        }

        _canNotRight = Physics.BoxCast(transform.position, _playerScale, Vector3.right, Quaternion.identity, _sidedistance, movelayer);
        _canNotLeft = Physics.BoxCast(transform.position, _playerScale, Vector3.left, Quaternion.identity, _sidedistance, movelayer);


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
            _isPeerless = true;
        }

        if (transform.position.y <= _ZERO)
        {
            checkPointSys.Respawn();
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
                _charaStatus = CharacterStatus.swordAttack;
            }
        }
        #endregion



        // 右クリックで槌攻撃
        #region 槌攻撃入力時間加算
        if (IC.Player.HammerAttack.phase == UnityEngine.InputSystem.InputActionPhase.Started && _canHammerAttack)
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
                _charaStatus = CharacterStatus.hammerAttack;
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
        Debug.Log("攻撃");
        _attackDirection.x = -transform.localScale.x;
        RaycastHit[] attackHit;
        #region 敵の状態判定
        if (!_isStartAttack)
        {
            _isStartAttack = true;
            attackHit = Physics.BoxCastAll(transform.position, _attackScale, _attackDirection, Quaternion.identity, _ONE, LayerMask.GetMask("Enemy"));
            Debug.Log(attackHit.Length);
            #endregion

            foreach (RaycastHit raycastHit in attackHit)
            {
                _isHit = true;
                #region 攻撃力代入
                if (_isPeerless)
                {
                    _attackPower = charaData.basicPower * _invincibleAttack;
                }
                else
                {
                    switch (_charaStatus)
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

                #endregion

                #region 敵の種別分け
                if (raycastHit.collider.tag == _normal)
                {
                    raycastHit.collider.GetComponent<EnemyNormal>().CharaLifeCalculation(_attackPower, _knockBack, _weapon);
                    Debug.Log("ふつう当たった");

                }
                else if (raycastHit.collider.tag == _soft)
                {
                    Debug.Log("やわらかい当たった");
                    raycastHit.collider.GetComponent<EnemyPlant>().CharaLifeCalculation(_attackPower, _knockBack, _weapon);
                }
                else if (raycastHit.collider.tag == _hard)
                {
                    Debug.Log("硬い当たった");
                    raycastHit.collider.GetComponent<EnemyRock>().CharaLifeCalculation(_attackPower, _knockBack, _weapon);
                }
                #endregion
            }
        }

        base.Attack();
    }


    //==========================================================


    //プレイヤーが死んだときの処理
    public override void Death()
    {
        base.Death();
        checkPointSys.Respawn();
        _life = charaData.life;
        _isDeath = false;
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
        foreach (GameObject memoryFragment in _memoryFragments)
        {
            if (this.transform.position.x - 1 >= memoryFragment.transform.position.x || this.transform.position.x <= memoryFragment.transform.position.x + 1)
                return;

            memoryAchievementController._nowMemoryToral++;
            memoryFragment.SetActive(false);
        }
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

        if (_isPeerless)
        {
            _memoryGauge = _MAXMEMORYGAUGE / _peerlessTime * Time.deltaTime;
        }
        else
        {
            // 時間での減少
            if (_memoryDownTimer > _ZERO)
            {
                _memoryDownTimer -= Time.deltaTime;
            }
            
            if (_memoryDownTimer <= _ZERO && _memoryGauge > _ZERO)
            {
                _memoryGauge -= _TIMEMEMORYDOWN;
                _memoryDownTimer = _ONE;
            }
        }

        #region 記憶ゲージ加算
        if (_isPeerless)
        {
            return;
        }
        else
        {
            //敵の種類によって加算する数を変える
            if (_memoryGauge < _MAXMEMORYGAUGE && _isHit)
            {
                switch (_weapon)
                {
                    case 0:
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
                    case 1:
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
                _isHit = false;
            }
            else
            {
                return;
            }
        }
        #endregion

        if (_memoryGauge <= _ZERO && _isPeerless)
        {
            _isPeerless = false;
        }
    }


    //==========================================================


    // Playerで行うタイマー処理
    private void Timer()
    {
        if (_isPeerless)
        {
            _peerlessTime -= Time.deltaTime;
        }

        if (_peerlessTime <= _ZERO)
        {
            _isPeerless = false;
        }

        if (_isInvincible)
        {
            _invincibleTime -= Time.deltaTime;
        }

        if (_invincibleTime <= _ZERO)
        {
            _isInvincible = false;
            _isDamage = false;
        }

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
    }


    //==========================================================


    // 無敵時間だったらCharaLifeCalculationを実行しない
    public override void CharaLifeCalculation(float damage, int knockBack, int weapon)
    {
        return;
        base.CharaLifeCalculation(damage, knockBack, weapon);
    }
    
    
    //==========================================================


    public void EndAttack( GameObject weapon)
    {
        Debug.Log("a");
        input._isAttack = false;
        weapon.SetActive(false);
        
        if (_isInputSwordAttack)
        {
            _isInputSwordAttack = false;
        }
        else
        {
            _isInputHammerAttack = false;
            _canHammerAttack = false;
        }
        _isStartAttack = false;

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