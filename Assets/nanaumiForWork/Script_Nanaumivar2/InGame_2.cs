using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGame_2 : Chara_2
{

    [SerializeField] private Image _damageImage = default;
    /// <summary>
    /// ダメージ演出のα値を格納する
    /// </summary>
    private float _damagealpha = default;
    /// <summary>
    /// ダメージ演出の赤色＋α値の設定する箱
    /// </summary>
    private Color _damageColor = new Color(1, 0, 0, 0);

    #region 記憶ゲージ
    [SerializeField,Tooltip("0無双 1通常")] private RectMask2D[] _memoryGaugeBarMask = default;
    [SerializeField] private Image[] _memoryGaugeBar = default;
    /// <summary>
    /// 記憶ゲージの最大値
    /// </summary>
    private const float _MAX_MEMORY = 100;

    private float _beforeMemory = default;
    private float _afterMemory = default;

    /// <summary>
    /// 記憶ゲージを変更するLerp時間
    /// </summary>
    private float _memoryChangeTime = default;
    /// <summary>
    /// 記憶ゲージのLerp倍率
    /// </summary>
    private const float _MEMORY_LERPMAGNITUDE = 2.21f;
    /// <summary>
    /// 記憶ゲージのLerp倍率 無双のとき
    /// </summary>
    private const float _MEMORY_LERPMAGNITUDE_MUSOU = 6f;
    /// <summary>
    /// 記憶ゲージを変更している
    /// </summary>
    private bool _isMemoryChange = default;
    #endregion

    #region 記憶達成度
    /// <summary>
    /// 記憶達成度のGameObject
    /// </summary>
    private GameObject _memoryAchinementObj = default;
    /// <summary>
    /// 記憶達成度のMask
    /// </summary>
    private RectMask2D _memoryGaugeAchievementBar = default;
    /// <summary>
    /// 記憶達成度の達成度を出すText
    /// </summary>
    private Text _memoryAchivementText = default;
    /// <summary>
    /// MeoryAchivementイベントの取得
    /// </summary>
    private MemoryAchievementController memoAchi = default;
    /// <summary>
    /// 記憶達成度の最大値
    /// </summary>
    private const float MAX_MEMORY_ACHIVEMENT = 100;

    private float _beforeMemoryAchievement = default;
    private float _afterMemoryAchievement = default;

    /// <summary>
    /// 記憶達成度のLerp時間
    /// </summary>
    private float _memoryAchievementChangeTime = default;
    /// <summary>
    /// 記憶達成度を変更している
    /// </summary>
    private bool _isMemoryAchievementChange = default;
    /// <summary>
    /// 記憶達成度のLerp倍率
    /// </summary>
    private const float _MEMORYACHIEVEMENT_LERPMAGNITUDE = 1.4f;
    /// <summary>
    /// 記憶達成度ののMaskPadding用のVector4
    /// </summary>
    private Vector4 VECTOR_UP = new Vector4(0, 0, 0, 1);
    #endregion

    /// <summary>
    /// 武器表示Sprite 現在使用していない
    /// </summary>
    [SerializeField, Tooltip("0sord/1hummer")] private GameObject[] _weaponImage = default;

    public bool _isDamage = default;
    private float _DamageTime = default;
    /// <summary>
    /// ダメージ受けたかどうかの比較用のLife保持
    /// </summary>
    private int _beforeLifeChild = default;

    private PlayerController _playerCon = default;

    private void Awake()
    {
        // 記憶達成度の取得(静的)
        _memoryAchinementObj = this.transform.GetChild(4).gameObject;
        // 記憶達成度のMask取得
        _memoryGaugeAchievementBar = _memoryAchinementObj.GetComponentInChildren<RectMask2D>();
        // 記憶達成度のText取得
        _memoryAchivementText = _memoryAchinementObj.GetComponentInChildren<Text>();
        // MemoryAchivementイベントの取得
        memoAchi = GameObject.FindWithTag("EventSystem").GetComponent<MemoryAchievementController>();

        // 記憶達成度を50％に設定
        _memoryGaugeBarMask[0].padding = _vectorHP * 5 * _MEMORY_LERPMAGNITUDE_MUSOU;
        _memoryGaugeBarMask[1].padding = _vectorHP * 45 * _MEMORY_LERPMAGNITUDE;
    }

    private void Start()
    {
        _isDamage = false;

        // 記憶達成度を隠す
        _memoryAchinementObj.SetActive(false);

        // PlayerControllerの取得 HPBarの設定
        _playerCon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        SetChara(_playerCon);
        _beforeLifeChild = (int)_playerCon.GetLife;

        // 記憶ゲージと記憶達成度の、減少時に変更する前の値を設定
        _beforeMemory = _playerCon._MemoryGauge - 5;
        _beforeMemoryAchievement = MAX_MEMORY_ACHIVEMENT;
    }

    private void Update()
    {
        PlayerHPUI();

        // 記憶ゲージが変更された判定
        if (_beforeMemory != _playerCon._MemoryGauge || _isMemoryChange)
        {
            if (!_isMemoryChange)
            {
                _isMemoryChange = true;
                _memoryChangeTime = 0;
            }
            PlayerMemoryUI();
        }

        // 記憶達成度が変更された判定
        if (_beforeMemoryAchievement != MAX_MEMORY_ACHIVEMENT - memoAchi._nowMemoryToral && !_isMemoryAchievementChange)
        {
            _isMemoryAchievementChange = true;
            _memoryAchievementChangeTime = 0;
        }

        // 記憶達成度が変更されてから3秒間記憶達成度を表示する
        if (_isMemoryAchievementChange)
        {
            _memoryAchinementObj.SetActive(true);
            PlayerMemoryAchievementUI();
        }
        else
        {
            _memoryAchinementObj.SetActive(false);
        }

        //WeaponChangeUI();
    }

    /// <summary>
    /// 武器の表示変更処理
    /// </summary>
    private void WeaponChangeUI()
    {
        switch (_playerCon._WeaponMemoryCount)
        {
            // 全て無効化
            case 0:
                _weaponImage[0].SetActive(false);
                _weaponImage[1].SetActive(false);
                break;
            // 剣だけ表示
            case 1:
                _weaponImage[0].SetActive(true);
                _weaponImage[1].SetActive(false);
                break;
            // 剣と槌表示
            case 2:
                _weaponImage[0].SetActive(true);
                _weaponImage[1].SetActive(true);
                break;
        }
    }

    private bool _isMusou = default;
    /// <summary>
    /// 記憶ゲージの変更処理
    /// </summary>
    private void PlayerMemoryUI()
    {
        // _afterMemoryの更新
        if (_isMusou)
        {
            _afterMemory = _MAX_MEMORY - _playerCon._MemoryGauge;
        }
        else
        {
            _afterMemory = _MAX_MEMORY - _playerCon._MemoryGauge - 5;
        }

        // _afterMemoryの更新
        if (_playerCon._MemoryGauge >= 95 && !_isMusou)
        {
            _afterMemory = _MAX_MEMORY - _playerCon._MemoryGauge;
            _beforeMemory = 5;
            _isMusou = true;
        }
        else if (_playerCon._MemoryGauge < 95 && _isMusou)
        {
            _afterMemory = _MAX_MEMORY - _playerCon._MemoryGauge - 5;
            _beforeMemory = 0;
            _isMusou = false;
        }

        // Lerp処理
        if (_memoryChangeTime <= 1f)
        {
            _memoryChangeTime += Time.deltaTime * 2;
            // 記憶ゲージが95％以上のとき(無双できるとき)
            if (_isMusou)
            {
                _memoryGaugeBarMask[0].padding = Vector4.Lerp(_vectorHP * _beforeMemory * _MEMORY_LERPMAGNITUDE_MUSOU, _vectorHP * _afterMemory * _MEMORY_LERPMAGNITUDE_MUSOU, _memoryChangeTime);
                _memoryGaugeBarMask[1].padding = _vectorHP * 0 * _MEMORY_LERPMAGNITUDE;
                _memoryGaugeBar[0].color = Color.red;
                _memoryGaugeBar[1].color = Color.red;
            }
            else
            {
                _memoryGaugeBarMask[0].padding = _vectorHP * 5 * _MEMORY_LERPMAGNITUDE_MUSOU;
                _memoryGaugeBar[0].color = Color.white;
                _memoryGaugeBarMask[1].padding = Vector4.Lerp(_vectorHP * _beforeMemory * _MEMORY_LERPMAGNITUDE, _vectorHP * _afterMemory * _MEMORY_LERPMAGNITUDE, _memoryChangeTime);
                _memoryGaugeBar[1].color = Color.white;
            }
        }
        else
        {
            // _beforeMemoryの更新
            _beforeMemory = _afterMemory;
            _isMemoryChange = false;
        }
    }

    /// <summary>
    /// 記憶達成度の変更処理
    /// </summary>
    private void PlayerMemoryAchievementUI()
    {
        // _afterMemoryAchievementの更新
        _afterMemoryAchievement = MAX_MEMORY_ACHIVEMENT - memoAchi._nowMemoryToral;

        // Lerp処理 表示時間3秒
        if (_memoryAchievementChangeTime <= 3f)
        {
            _memoryAchievementChangeTime += Time.deltaTime * 2;
            _memoryGaugeAchievementBar.padding = Vector4.Lerp(VECTOR_UP * _beforeMemoryAchievement * _MEMORYACHIEVEMENT_LERPMAGNITUDE, VECTOR_UP * _afterMemoryAchievement * _MEMORYACHIEVEMENT_LERPMAGNITUDE, _memoryAchievementChangeTime);
            _memoryAchivementText.text = (MAX_MEMORY_ACHIVEMENT - (int)Mathf.Lerp(_beforeMemoryAchievement, _afterMemoryAchievement, _memoryAchievementChangeTime)).ToString();
        }
        else
        {
            // _beforeMemoryAchievementの更新
            _beforeMemoryAchievement = _afterMemoryAchievement;
            _memoryAchivementText.text = (MAX_MEMORY_ACHIVEMENT - (int)_afterMemoryAchievement).ToString();
            _isMemoryAchievementChange = false;
        }
    }

    /// <summary>
    /// HPBarの変更処理
    /// </summary>
    private void PlayerHPUI()
    {
        // ダメージを受けたかの判定
        if (_beforeLifeChild != (int)_playerCon.GetLife)
        {
            _isDamage = true;
            _DamageTime = 0;
            _beforeLifeChild = (int)_playerCon.GetLife;
        }

        // 一気に減らす
        if (_isDamage)
        {
            ChangeLife(_playerCon, true);
            _DamageTime += Time.deltaTime;

            // ダメージ演出 Image
            _damagealpha = Mathf.Sin(_DamageTime * Mathf.PI * 2) * 0.15f;
            _damageColor.a = _damagealpha;
            _damageImage.color = _damageColor;

            if (_DamageTime >= 0.5f)
            {
                _isDamage = false;
            }
        }
        // 差分をLerpで減らす
        else
        {
            _damageImage.color = Color.clear;
            ChangeLife(_playerCon, false);
        }
    }
}
