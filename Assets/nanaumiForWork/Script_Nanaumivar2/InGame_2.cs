using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGame_2 : Chara_2
{
    //[SerializeField, Range(0, 2)] private int _testaaa = default;
    [SerializeField] private CharaParameter _playerPara = default;
    //[SerializeField, Range(0, 100)] private int _testbbb = default;

    #region 記憶ゲージ
    [SerializeField] private RectMask2D _memoryGaugeBar = default;
    private const float MAX_MEMORY = 100;
    private float _beforeMemory = default;
    private float _afterMemory = default;
    private float _memoryChangeTime = default;
    private bool _isMemoryChange = default;
    #endregion

    #region 記憶達成度
    private GameObject _memoryAchinementObj = default;
    private RectMask2D _memoryGaugeAchievementBar = default;
    private Text _memoryAchivementText = default;
    private MemoryAchievementController memoAchi = default;
    private const float MAX_MEMORY_ACHIVEMENT = 100;
    private float _beforeMemoryAchievement = default;
    private float _afterMemoryAchievement = default;
    private float _memoryAchievementChangeTime = default;
    private bool _isMemoryAchievementChange = default;
    private Vector4 VECTOR_UP = new Vector4(0, 0, 0, 1);
    #endregion

    [SerializeField, Tooltip("0sord/1hummer")] private GameObject[] _weaponImage = default;

    private bool _isDamage = default;
    private float _DamageTime = default;
    private int _beforeLifeChild = default;

    private PlayerController _playerCon = default;

    private void Awake()
    {
        _memoryAchinementObj = this.transform.GetChild(3).gameObject;
        _memoryGaugeAchievementBar = _memoryAchinementObj.GetComponentInChildren<RectMask2D>();
        _memoryAchivementText = _memoryAchinementObj.GetComponentInChildren<Text>();
        memoAchi = GameObject.FindWithTag("EventSystem").GetComponent<MemoryAchievementController>();

        _memoryGaugeBar.padding = vectorHP * 50 * 2.4f;
    }

    private void Start()
    {
        _isDamage = false;

        _memoryAchinementObj.SetActive(false);

        _playerCon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        SetChara(_playerCon);
        _beforeLifeChild = (int)_playerCon.GetLife;

        _beforeMemory = _playerCon._MemoryGauge;
        _beforeMemoryAchievement = MAX_MEMORY_ACHIVEMENT;
    }

    private void Update()
    {
        PlayerHPUI();

        if (_beforeMemory != _playerCon._MemoryGauge/*_testbbb*/ || _isMemoryChange)
        {
            PlayerMemoryUI();
            if (!_isMemoryChange)
            {
                _isMemoryChange = true;
                _memoryChangeTime = 0;
            }
        }

        if (_beforeMemoryAchievement != 100 - memoAchi._nowMemoryToral/*_testbbb*/ && !_isMemoryAchievementChange)
        {
            _isMemoryAchievementChange = true;
            _memoryAchievementChangeTime = 0;
        }

        if (_isMemoryAchievementChange)
        {
            _memoryAchinementObj.SetActive(true);
            PlayerMemoryAchievementUI();
        }
        else
        {
            _memoryAchinementObj.SetActive(false);
        }

        WeaponChangeUI();
    }

    private void WeaponChangeUI()
    {
        switch (/*_testaaa*/_playerCon._WeaponMemoryCount)
        {
            case 0:
                _weaponImage[0].SetActive(false);
                _weaponImage[1].SetActive(false);
                break;
            case 1:
                _weaponImage[0].SetActive(true);
                _weaponImage[1].SetActive(false);
                break;
            case 2:
                _weaponImage[0].SetActive(true);
                _weaponImage[1].SetActive(true);
                break;
        }
    }

    private void PlayerMemoryUI()
    {
        _afterMemory = MAX_MEMORY - _playerCon._MemoryGauge/*_testbbb*/;
        if (_memoryChangeTime <= 1f)
        {
            _memoryChangeTime += Time.deltaTime * 2;
            _memoryGaugeBar.padding = Vector4.Lerp(vectorHP * _beforeMemory * 2.4f, vectorHP * _afterMemory * 2.4f, _memoryChangeTime);
        }
        else
        {
            _beforeMemory = _afterMemory;
            _isMemoryChange = false;
        }
    }

    // メモリーアチーブメント イベントから取得
    private void PlayerMemoryAchievementUI()
    {
        _afterMemoryAchievement = MAX_MEMORY_ACHIVEMENT - memoAchi._nowMemoryToral/*_testbbb*/;
        if (_memoryAchievementChangeTime <= 3f)
        {
            _memoryAchievementChangeTime += Time.deltaTime * 2;
            _memoryGaugeAchievementBar.padding = Vector4.Lerp(VECTOR_UP * _beforeMemoryAchievement * 1.4f, VECTOR_UP * _afterMemoryAchievement * 1.4f, _memoryAchievementChangeTime);
            _memoryAchivementText.text = (100 - (int)Mathf.Lerp(_beforeMemoryAchievement, _afterMemoryAchievement, _memoryAchievementChangeTime)).ToString();
        }
        else
        {
            _beforeMemoryAchievement = _afterMemoryAchievement;
            _memoryAchivementText.text = (100 - (int)_afterMemoryAchievement).ToString();
            _isMemoryAchievementChange = false;
        }
    }

    private void PlayerHPUI()
    {
        if (_beforeLifeChild != (int)_playerCon.GetLife)
        {
            _isDamage = true;
            _DamageTime = 0;
            _beforeLifeChild = (int)_playerCon.GetLife;
        }

        if (_isDamage)
        {
            ChangeLife(_playerCon, true);
            _DamageTime += Time.deltaTime;

            if (_DamageTime >= 0.5f)
            {
                _isDamage = false;
            }
        }
        else
        {
            ChangeLife(_playerCon, false);
        }
    }
}
