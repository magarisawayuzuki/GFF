using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI_2 : Chara_2
{
    [SerializeField] private CharaParameter _enemyPara;

    private void Awake()
    {
        HPScroll = this.GetComponent<Scrollbar>();
    }

    private void Start()
    {
        //SetChara(_enemyPara);
    }

    private void Update()
    {
        ChangeLife(_enemyPara);
    }
}
