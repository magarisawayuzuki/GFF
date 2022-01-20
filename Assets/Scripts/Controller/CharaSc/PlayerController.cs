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

    [SerializeField]
    private GameObject[] swordEffects = default;
    [SerializeField]
    private GameObject[] hammerEffects = default;


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
    // 攻撃のレベル
    private int _attackLevel = default;

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
    private float _peerlessTime = 1;
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

    // 攻撃方向でこの数を足す
    private const int _ATTACK_TRANSFORM = 3;

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
        if (!InGameToPauseUI_2._isStaticPause)
        {
            Debug.Log(_life);
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


            MemoryGet();

            MomoryGauge();

            Timer();
        }
        else
        {
            _charaStatus = CharacterStatus.Idle;
            //アニメーションを切り替える
            charaAnimCtrl.AnimationChenge(_charaStatus);
            old_charaStatus = _charaStatus;
        }
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
            if (-input._x >= 0)
            {
                _playerFlontScale.x = 1;
            }
            else if (-input._x < 0)
            {
                _playerFlontScale.x = -1;
            }

            transform.localScale =  _playerFlontScale;
        }

        _canNotRight = Physics.BoxCast(transform.position, _playerScale, Vector3.right, Quaternion.identity, _sidedistance, movelayer);
        _canNotLeft = Physics.BoxCast(transform.position, _playerScale, Vector3.left, Quaternion.identity, _sidedistance, movelayer);


        if (IC.Player.Jump.triggered && !input._isAttack && !_isDamage)
        {
            input._isJump = true;
        }

        if (IC.Player.Down.triggered)
        {
            input._isDown = true;
        }

        // 無双入力
        if (IC.Player.Invincible.triggered && _memoryGauge >= 95)
        {
            _isPeerless = true;
        }

        if (transform.position.y <= _ZERO)
        {
            Death();
        }
        
        // 左クリックで剣攻撃
        #region 剣攻撃入力時間加算
        if (IC.Player.SwordAttack.phase == UnityEngine.InputSystem.InputActionPhase.Started && !input._isAttack)
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
        if (IC.Player.HammerAttack.phase == UnityEngine.InputSystem.InputActionPhase.Started && _canHammerAttack && !input._isAttack)
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
        _attackDirection.x = -transform.localScale.x;
        RaycastHit[] attackHit;
        if (!_isStartAttack)
        {
            _isStartAttack = true;
            #region 敵の状態判定
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
                                _attackLevel = _TWO;
                                Debug.Log("剣強攻撃");
                            }
                            else if (_swordTime > _NORMALPOWERTIME)
                            {
                                _attackPower = charaData.basicPower * _swordMiddleAttack * _memoryGaugeAttackPoint;
                                _attackLevel = _ONE;
                                Debug.Log("剣中攻撃");
                            }
                            else
                            {
                                _attackPower = charaData.basicPower * _memoryGaugeAttackPoint;
                                _attackLevel = _ZERO;
                                Debug.Log("剣弱攻撃");
                            }
                            break;
                        case CharacterStatus.hammerAttack:
                            // 槌協攻撃2段階目
                            if (_hammerTime > _MIDDLEPOWERTIME)
                            {
                                _attackPower = charaData.basicPower * _hammerHeavyAttack * _memoryGaugeAttackPoint;
                                _attackLevel = _TWO;
                                Debug.Log("槌強攻撃");
                            }
                            // 槌強攻撃1段階目
                            else if (_hammerTime > _NORMALPOWERTIME)
                            {
                                _attackPower = charaData.basicPower * _hammerMiddleAttack * _memoryGaugeAttackPoint;
                                _attackLevel = _ONE;
                                Debug.Log("槌中攻撃");
                            }
                            // 槌弱攻撃
                            else
                            {
                                _attackPower = charaData.basicPower * _hammerLightAttack * _memoryGaugeAttackPoint;
                                _attackLevel = _ZERO;
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
                    _isSoft = true;
                    Debug.Log("ふつう当たった");

                }
                else if (raycastHit.collider.tag == _soft | raycastHit.collider.tag == "SoftS")
                {
                    Debug.Log("やわらかい当たった");
                    _isNormal = true;
                    raycastHit.collider.GetComponent<EnemyPlant>().CharaLifeCalculation(_attackPower, _knockBack, _weapon);
                }
                else if (raycastHit.collider.tag == _hard | raycastHit.collider.tag == "HardS")
                {
                    Debug.Log("硬い当たった");
                    _isHard = true;
                    raycastHit.collider.GetComponent<EnemyRock>().CharaLifeCalculation(_attackPower, _knockBack, _weapon);
                }
                else if (raycastHit.collider.tag == "BossObj")
                {
                    raycastHit.collider.GetComponent<Boss>().CharaLifeCalculation(_attackPower, _knockBack, _weapon);
                }
                #endregion

                #region 記憶ゲージ加算
                if (_isPeerless)
                {
                    return;
                }
                else
                {
                    //敵の種類によって加算する数を変える
                    if (_memoryGauge < _MAXMEMORYGAUGE)
                    {
                        switch (_weapon)
                        {
                            case 0:
                                if (_isSoft)
                                {
                                    _memoryGauge += _GOODMEMORYPLUS;
                                    _isSoft = false;
                                }
                                else if (_isNormal)
                                {
                                    _memoryGauge += _NORMALMEMORYPLUS;
                                    _isNormal = false;
                                }
                                else if (_isHard)
                                {
                                    _memoryGauge += _BADMEMORYPLUS;
                                    _isHard = false;
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
                                    _isHard = false;
                                }
                                else if (_isNormal)
                                {
                                    _memoryGauge += _NORMALMEMORYPLUS;
                                    _isNormal = false;
                                }
                                else if (_isSoft)
                                {
                                    _memoryGauge += _BADMEMORYPLUS;
                                    _isSoft = false;
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
        memoryAchievementController._nowMemoryToral = _ZERO;
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
        _memoryFragments = GameObject.FindGameObjectsWithTag("MemoryFragment");
        foreach (GameObject memoryFragment in _memoryFragments)
        {
            if (memoryFragment.transform.position.x >= this.transform.position.x + 1 || memoryFragment.transform.position.x <= this.transform.position.x - 1)
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

            _invincibleTime = 1;
        }

        #region 槌のクールダウン
        if (!_canHammerAttack)
        {
            _hammerCoolDown -= Time.deltaTime;
        }

        if (_hammerCoolDown <= _ZERO)
        {
            _canHammerAttack = true;
            _hammerCoolDown = 3;
        }
        #endregion
    }


    //==========================================================


    // 無敵時間だったらCharaLifeCalculationを実行しない
    public override void CharaLifeCalculation(float damage, int knockBack, int weapon)
    {
        if (_isInvincible)
        {
            return;
        }
        else
        {
            _isInvincible = true;
        }

        base.CharaLifeCalculation(damage, knockBack, weapon);
    }
    
    
    //==========================================================


    public void EndAttack()
    {
        Debug.Log("a");
        input._isAttack = false;
        
        if (_isInputSwordAttack)
        {
            _isInputSwordAttack = false;
            _swordTime = 0;
            switch (transform.localScale.x)
            {
                case _ONE:
                    swordEffects[_attackLevel + _ATTACK_TRANSFORM].SetActive(false);
                    break;
                case -_ONE:
                    swordEffects[_attackLevel].SetActive(false);
                    break;
            }
        }
        else
        {
            _isInputHammerAttack = false;
            _canHammerAttack = false;
            _hammerTime = 0;
            switch (transform.localScale.x)
            {
                case _ONE:
                    hammerEffects[_attackLevel + _ATTACK_TRANSFORM].SetActive(false);
                    break;
                case -_ONE:
                    hammerEffects[_attackLevel].SetActive(false);
                    break;
            }
        }
        _isStartAttack = false;
    }


    //==========================================================


    public void EffectStart()
    {
        if (_charaStatus == CharacterStatus.swordAttack)
        {

            switch (transform.localScale.x)
            {
                case _ONE:
                        swordEffects[_attackLevel + _ATTACK_TRANSFORM].SetActive(true);
                    break;
                case -_ONE:
                        swordEffects[_attackLevel].SetActive(true);
                    break;
            }
        }
        else
        {
            switch (transform.localScale.x)
            {
                case _ONE:
                    hammerEffects[_attackLevel + _ATTACK_TRANSFORM].SetActive(true);
                    break;
                case -_ONE:
                    hammerEffects[_attackLevel].SetActive(true);
                    break;
            }
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