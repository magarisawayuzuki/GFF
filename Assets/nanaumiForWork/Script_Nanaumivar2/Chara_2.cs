using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chara_2 : MonoBehaviour
{
    private float maxLife = default;
    private float beforeLife = default;
    private float afterLife = default;

    [SerializeField] protected RectMask2D[] HPScroll = default;

    //[SerializeField, Range(0, 100)] protected int life = default;

    private float lifeChangeMagnitude = 2f;
    private float lerpTime = default;

    protected Vector4 vectorHP = new Vector4(0, 0, 1, 0);

    protected void SetChara(CharacterController charaPara)
    {
        beforeLife = 0;
        maxLife = charaPara.GetLife;
    }

    protected virtual void ChangeLife(CharacterController charaPara, bool isDamage)
    {
        afterLife = maxLife - charaPara.GetLife;
        HPScroll[0].padding = vectorHP * afterLife * 2.68f;
        if (!isDamage)
        {
            if (lerpTime <= 1f)
            {
                lerpTime += Time.deltaTime * lifeChangeMagnitude * 2;
                HPScroll[1].padding = Vector4.Lerp(vectorHP * beforeLife * 2.68f, vectorHP * afterLife * 2.68f, lerpTime);
            }
            else
            {
                beforeLife = afterLife;
            }
        }
        else if(isDamage)
        {
            lerpTime = 0;
        }
    }
}
