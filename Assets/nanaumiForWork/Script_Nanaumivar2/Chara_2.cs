using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chara_2 : MonoBehaviour
{
    private CharaParameter _chara;
    private int maxLife;
    private int beforeLife;
    private int afterLife;

    protected Scrollbar HPScroll;

    [SerializeField] private int life;

    private int lifeChangeMagnitude = 5;

    protected void SetChara(CharaParameter charaPara)
    {
        //_chara = GameObject.FindGameObjectWithTag(charaTag).GetComponent<CharaParameter>();
        beforeLife = /*charaPara.*/life;
        maxLife = /*charaPara.*/life;
    }

    protected virtual void ChangeLife(CharaParameter charaPara)
    {
        afterLife = /*_chara.*/life;
        HPScroll.size = Mathf.Lerp(beforeLife, afterLife, Time.deltaTime * lifeChangeMagnitude) / maxLife;
        beforeLife = afterLife;
    }
}
