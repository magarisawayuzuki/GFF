using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラに共通する処理を記述
/// </summary>
public class CharacterController : MonoBehaviour
{
    [SerializeField]
    CharaParameter charaData;
    [SerializeField]
    Weapons[] weapon;

    

    [SerializeField]
    protected PlayerInput input;


    //==========================================================


    /// <summary>
    /// 処理を呼び出す
    /// </summary>
    private void Update()
    {
        input = InputMethod();
    }





    /// <summary>
    /// 子クラスからの入力を受ける
    /// </summary>
    /// <returns></returns>
    public virtual PlayerInput InputMethod()
    {

        return null;
    }


    /// <summary>
    /// プレイヤーの挙動で記述しておき、特殊な敵は子クラスで書き換える
    /// </summary>
    public virtual void Move()
    {

    }

    /// <summary>
    /// 基本的な攻撃挙動のみ記述しておき、子クラスで書き換える
    /// </summary>
    public virtual void Attack()
    {

    }

    private void Jump()
    {

    }

    public virtual void Death()
    {

    }



    /// <summary>
    /// 自機のライフ計算を行う
    /// </summary>
    /// <param name="power"></param>
    public void CharaLifeCalculation(int power)
    {

    }

    /// <summary>
    /// 自機の状態からViewクラスを管理
    /// </summary>
    private void CharaViewController()
    {

    }
}


//=================================================================================


/// <summary>
/// inputを可視化するための変数定義クラス
/// </summary>
[System.Serializable]
public class PlayerInput
{

}

[System.Serializable]
public class Weapons
{
    public string name;
    public WeaponParameter param;
}