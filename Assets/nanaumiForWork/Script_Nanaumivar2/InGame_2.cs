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
    private int _beforeMemory = default;
    private int _afterMemory = default;
    private float _memoryChangeTime = default;
    private bool _isMemoryChange = default;

    [SerializeField, Tooltip("0sord/1hummer")] private GameObject[] _weaponImage = default;

    private bool _isDamage = default;
    private float _DamageTime = default;
    private int _beforeLifeChild = default;

    private void Awake()
    {
        //HPScroll = this.GetComponentInChildren<RectMask2D>();
        _isDamage = false;
    }

    private void Start()
    {
        SetChara(_playerPara);
        _beforeLifeChild = _playerPara.life;

        _beforeMemory = _testaaa;// memoryの取得
    }

    private void Update()
    {
        PlayerHPUI();

        if (_beforeMemory != /*memoryの取得*/_testbbb || _isMemoryChange)
        {
            PlayerMemoryUI();
            if (!_isMemoryChange)
            {
                _isMemoryChange = true;
                _memoryChangeTime = 0;
            }
        }

        WeaponChangeUI();
    }

    private void WeaponChangeUI()
    {
        switch (_testaaa/*武器の取得状況*/)
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
        _afterMemory = /*maxLife - _chara.*/100 - _testbbb;
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

    private void PlayerHPUI()
    {
        if (_beforeLifeChild != /*playerPara.*/life)
        {
            _isDamage = true;
            _DamageTime = 0;
            _beforeLifeChild = /*playerPara.*/life;
        }

        if (_isDamage)
        {
            ChangeLife(_playerPara, true);
            _DamageTime += Time.deltaTime;

            if (_DamageTime >= 2)
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
