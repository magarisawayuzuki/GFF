using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chara_2 : MonoBehaviour
{
    private CharaParameter _chara = default;
    private int maxLife = default;
    private float maxLifeMag = default;
    private int beforeLife = default;
    private int afterLife = default;

    [SerializeField] protected RectMask2D[] HPScroll = default;

    [SerializeField, Range(0, 100)] protected int life = default;

    private float lifeChangeMagnitude = 0.01f;
    private float lerpTime = default;

    private Vector4 vectorHP = new Vector4(0, 0, 1, 0);

    protected void SetChara(CharaParameter charaPara)
    {
        //_chara = GameObject.FindGameObjectWithTag(charaTag).GetComponent<CharaParameter>();
        beforeLife = 0;
        //maxLife = _chara.life;
        maxLifeMag = 2.68f;
    }

    protected virtual void ChangeLife(CharaParameter charaPara, bool isDamage)
    {
        afterLife = /*maxLife - _chara.*/100 - life;
        HPScroll[0].padding = vectorHP * afterLife * 2.68f;
        if (!isDamage && lerpTime >= 0f)
        {
            lerpTime += Time.deltaTime * lifeChangeMagnitude / (beforeLife - afterLife);
            Debug.Log(vectorHP * beforeLife * 2.68f);
            HPScroll[1].padding = Vector4.Lerp(vectorHP * afterLife * 2.68f, vectorHP * beforeLife * 2.68f, lerpTime);
        }
        else 
        {
            lerpTime = 0;
            beforeLife = afterLife;
        }
    }
}
