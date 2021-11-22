using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵機のインターフェース
/// </summary>
public class EnemyController : CharacterController
{
    protected float damageScaleSword;
    protected float damageScaleHammer;
    /// <summary>
    /// 子クラスのinputを用いてinputを返す。ここでは共通処理のみ記述可
    /// </summary>
    /// <returns></returns>
    public override PlayerInput InputMethod()
    {

        return input;
    }

    public override void CharaLifeCalculation(float damage, int knockBack, int weapon)
    {
        //攻撃された武器により倍率変更
        switch (weapon)
        {
            //剣
            case 0:
                damage *= damageScaleSword;
                break;

            //槌
            case 1:
                damage *= damageScaleHammer;
                break;
        }


        base.CharaLifeCalculation(damage, knockBack,weapon);
    }

    //記憶の欠片を落とす処理を追記
    public override void Death()
    {
        base.Death();


    }
}