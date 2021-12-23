using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGame_2 : Chara_2
{
    [SerializeField, Range(0, 2)] private int _testaaa = default;
    [SerializeField] private CharaParameter _playerPara = default;
    [SerializeField, Range(0, 100)] private int _testbbb = default;

    [SerializeField] private RectMask2D _memoryGaugeBar = default;
    private float _beforeMemory = default;
    private float _afterMemory = default;
    private float _memoryChangeTime = default;
    private bool _isMemoryChange = default;

    [SerializeField] private RectMask2D _memoryGaugeAchievementBar = default;
    private float _beforeMemoryAchievement = default;
    private float _afterMemoryAchievement = default;
    private float _memoryAchievementChangeTime = default;
    private bool _isMemoryAchievementChange = default;

    [SerializeField, Tooltip("0sord/1hummer")] private GameObject[] _weaponImage = default;

    private bool _isDamage = default;
    private float _DamageTime = default;
    private int _beforeLifeChild = default;

    private PlayerController playerCon = default;

    private void Awake()
    {
        _isDamage = false;
    }

    private void Start()
    {
        playerCon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        SetChara(_playerPara);
        _beforeLifeChild = _playerPara.life;

        _beforeMemory = playerCon._MemoryGauge;// memoryの取得
    }

    private void Update()
    {
        PlayerHPUI();

        if (_beforeMemory != playerCon._MemoryGauge/*_testbbb*/ || _isMemoryChange)
        {
            PlayerMemoryUI();
            if (!_isMemoryChange)
            {
                _isMemoryChange = true;
                _memoryChangeTime = 0;
            }
        }

        if (_beforeMemoryAchievement != playerCon._MemoryGauge/*_testbbb*/ || _isMemoryChange)
        {
            PlayerMemoryAchievementUI();
            if (!_isMemoryChange)
            {
                _isMemoryAchievementChange = true;
                _memoryAchievementChangeTime = 0;
            }
        }

        WeaponChangeUI();
    }

    private void WeaponChangeUI()
    {
        switch (/*_testaaa*/playerCon._WeaponMemoryCount)
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
        _afterMemory = 100 - playerCon._MemoryGauge/*_testbbb*/;
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
        _afterMemoryAchievement = 100 - playerCon._MemoryGauge/*_testbbb*/;
        if (_memoryChangeTime <= 1f)
        {
            _memoryAchievementChangeTime += Time.deltaTime * 2;
            _memoryGaugeBar.padding = Vector4.Lerp(vectorHP * _beforeMemoryAchievement * 2.4f, vectorHP * _afterMemoryAchievement * 2.4f, _memoryAchievementChangeTime);
        }
        else
        {
            _beforeMemoryAchievement = _afterMemoryAchievement;
            _isMemoryAchievementChange = false;
        }
    }

    private void PlayerHPUI()
    {
        if (_beforeLifeChild != _playerPara.life)
        {
            _isDamage = true;
            _DamageTime = 0;
            _beforeLifeChild = _playerPara.life;
        }

        if (_isDamage)
        {
            ChangeLife(_playerPara, true);
            _DamageTime += Time.deltaTime;

            if (_DamageTime >= 1)
            {
                _isDamage = false;
            }
        }
        else
        {
            ChangeLife(_playerPara, false);
        }
    }
}
