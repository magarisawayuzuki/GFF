using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGame_2 : Chara_2
{
    [SerializeField] private float _aaa;
    [SerializeField] private CharaParameter playerPara;
    [SerializeField] private float _bbb;

    [SerializeField] private Scrollbar _memoryGaugeBar;

    [SerializeField] private GameObject[] weaponImage;

    private void Awake()
    {
        HPScroll = this.GetComponentInChildren<Scrollbar>();
    }

    private void Start()
    {
        SetChara(playerPara);
    }

    private void FixedUpdate()
    {
        ChangeLife(playerPara);
    }
}
