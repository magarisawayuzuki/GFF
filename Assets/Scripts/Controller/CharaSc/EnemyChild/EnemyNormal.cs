using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 遠距離攻撃のAI
/// </summary>
public class EnemyNormal : EnemyController
{
    [SerializeField]
    EnemyParameter enemyData;
    //AI動作を記述
    public override PlayerInput InputMethod()
    {

        return null;
    }

    public override void CharaLifeCalculation(float damage, int knockBack, int weapon)
    {
        //倍率を代入
        damageScaleSword = enemyData.swordScale;
        damageScaleHammer = enemyData.hammerScale;

        base.CharaLifeCalculation(damage, knockBack, weapon);
    }

    //===========動作の上書き・追記が必要なら変更する===================

    public override void Move()
    {
        base.Move();
    }

    public override void Attack()
    {
        base.Attack();
    }
}
