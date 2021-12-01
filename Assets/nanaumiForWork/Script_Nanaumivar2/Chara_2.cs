using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chara_2 : MonoBehaviour
{
    private CharaParameter _chara;
    private int beforeLife;
    private int afterLife;

    [SerializeField] private Scrollbar scroll;

    protected virtual void Awake()
    {
        _chara = GameObject.FindGameObjectWithTag(this.gameObject.tag).GetComponent<CharaParameter>();
        beforeLife = _chara.life;
    }

    protected virtual void ChangeLife()
    {
        afterLife = _chara.life;
        scroll.value = afterLife;// 後でlerp処理記述
        beforeLife = afterLife;
    }
}
