using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵機のインターフェース
/// </summary>
public class EnemyController : CharacterController
{
    [Header("普通の敵のダメージ倍率")]
    [SerializeField] private float[] nomalScale = { 0, 0 };

    [Header("柔らかい敵のダメージ倍率")]
    [SerializeField] private float[] plantScale = { 0, 0 };

    [Header("硬い敵のダメージ倍率")]
    [SerializeField] private float[] rockScale = {0, 0};

    /// <summary>
    /// 子クラスのinputを用いてinputを返す。ここでは共通処理のみ記述可
    /// </summary>
    /// <returns></returns>
    public override PlayerInput InputMethod()
    {

        return input;
    }

    public override void CharaLifeCalculation(int damage, int knockBack, int weapon)
    {
        int damageScaleSword = 1;
        int damageScaleHammer = 1;
        //子クラスにEnemyRockがあれば実行
        if (this.GetType().IsSubclassOf(typeof(EnemyRock)))
        {
            damageScaleSword = 0;
            damageScaleHammer = 0;
        }
        //子クラスにEnemyPlantがあれば実行
        else if (this.GetType().IsSubclassOf(typeof(EnemyPlant)))
        {

        }
        //子クラスにEnemyNormalがあれば実行
        else if(this.GetType().IsSubclassOf(typeof(EnemyNormal)))
        {

        }

        switch (weapon)
        {
            //剣
            case 0:
                damage *= 0;
                break;

            //槌
            case 1:
                damage *= 0;
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