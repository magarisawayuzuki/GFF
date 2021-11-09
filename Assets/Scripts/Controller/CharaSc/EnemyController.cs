using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵機のインターフェース
/// </summary>
public class EnemyController : CharacterController
{

    /// <summary>
    /// 子クラスのinputを用いてinputを返す。ここでは共通処理のみ記述可
    /// </summary>
    /// <returns></returns>
    public override PlayerInput InputMethod()
    {

        return input;
    }

    //記憶の欠片を落とす処理を追記
    public override void Death()
    {
        base.Death();


    }
}