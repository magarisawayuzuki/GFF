using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 近距離攻撃のAI
/// </summary>
public class EnemyPlant : EnemyController
{

    //AI動作を記述
    public override PlayerInput InputMethod()
    {

        return null;
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
