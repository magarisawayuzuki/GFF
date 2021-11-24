using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラに共通する処理を記述
/// </summary>
public class CharacterController : MonoBehaviour
{
    [SerializeField]
    protected CharaParameter charaData;
    [SerializeField]
    Weapons[] weapon;
    [SerializeField]
    AnimationSpriteData animData;
    [SerializeField]
    AnimationCurve jumpCurve;




    [SerializeField]
    protected PlayerInput input;
    protected SpriteRenderer spriteRenderer = null;
    protected CharacterStatus charaStatus;


    /// Vector3
    protected Vector3 CharacterMove = new Vector3();

    /// int
    private int _anim = 0;
    private int[] _maxAnimationCount = { 4, 8, 5, 13, 0 };


    /// float
    protected float _jumpTimer = 0;
    // 攻撃力
    protected float _attackPower = 0;
    // Jumpの高さ
    private float _y = 4;
    // Rayの長さ
    private float[] _animationTime = { 0, 0, 0, 0, 0 };


    /// bool
    protected bool _isGround = false;
    protected bool _hasMemory = false;


    /// <summary>
    /// const
    /// </summary>
    protected const int _ZERO = 0;
    protected const int _ONE = 1;

    //==========================================================

    /// <summary>
    /// CharaStatus(Charaの状態)
    /// </summary>
    public enum CharacterStatus
    {
        Idle,
        Move,
        Jump,
        Fall,
        swordAttack,
        hammerAttack,
        Death,
        Damage,
    }

    //==========================================================


    /// <summary>
    /// 処理を呼び出す
    /// </summary>
    protected virtual void Update()
    {
        input = InputMethod();

        // 移動
        Move();

        // CharacterAnimation
        //CharaViewControll();

        // ジャンプ処理
        Jump();

        // _isAttackがtrueの時攻撃
        if (input._isAttack)
        {
            Attack();
        }

        // velocityへ入れる
        transform.position += CharacterMove;
    }


    //=================================================================================


    /// <summary>
    /// 子クラスからの入力を受ける
    /// </summary>
    /// <returns></returns>
    public virtual PlayerInput InputMethod()
    {
        return null;
    }

    //=================================================================================


    /// <summary>
    /// プレイヤーの挙動で記述しておき、特殊な敵は子クラスで書き換える
    /// </summary>
    public virtual void Move()
    {
        // 攻撃状態じゃなければ
        if (!input._isAttack)
        {
            charaStatus = CharacterStatus.Move;
            CharacterMove.x = input._x * 10 * Time.deltaTime;
        }
        else
        {
            charaStatus = CharacterStatus.Idle;
            CharacterMove.x = _ZERO;
        }
    }


    //==================================================================================


    /// <summary>
    /// 基本的な攻撃挙動のみ記述しておき、子クラスで書き換える
    /// </summary>
    public virtual void Attack()
    {

    }


    //===================================================================================


    /// <summary>
    /// 着地判定、velocityのy軸に値を入れる
    /// </summary>
    private void Jump()
    {
        // LayerMaskがGroundだったら着地
        if (Physics.Raycast(transform.position, Vector3.down, _ONE, LayerMask.GetMask("Ground")))
        {
            _isGround = true;
        }
        else
        {
            _isGround = false;
        }

        Debug.Log(_jumpTimer);
        Debug.Log(CharacterMove.y);

        if (input._isJump && _jumpTimer < jumpCurve.length)
        {
            _jumpTimer += Time.deltaTime;
        }
        else
        {
            input._isJump = false;
            _jumpTimer = _ZERO;
        }

        // 地面にいたらJumpする地面にいなかったらしない
        if (input._isJump && _isGround && !input._isAttack)
        {
            charaStatus = CharacterStatus.Jump;
            //transform.Translate(Vector3.up * jumpCurve.Evaluate(_jumpTimer) * _y);
            CharacterMove.y = jumpCurve.Evaluate(_jumpTimer) * Time.deltaTime;
        }
        else if(!input._isJump && !_isGround)
        {
            charaStatus = CharacterStatus.Fall;
            CharacterMove.y = -_y *  Time.deltaTime;
        }
        else if(!input._isJump && _isGround || input._isAttack)
        {
            CharacterMove.y = _ZERO;
        }
    }


    //=====================================================================


    public virtual void Death()
    {
        charaStatus = CharacterStatus.Death;
    }


    //=====================================================================


    /// <summary>
    /// 自機のライフ計算を行う
    /// </summary>
    /// <param name="power"></param>
    public virtual void CharaLifeCalculation(float damage, int knockBack, int weapon)
    {
        charaStatus = CharacterStatus.Damage;

        charaData.life -= (int)Mathf.Floor(damage);

        if (charaData.life <= 0)
        {
            Death();
        }
    }


    //=====================================================================


    /// <summary>
    /// 自機の状態からViewクラスを管理
    /// </summary>
    private void CharaViewController()
    {
        // _xが0より小さければ左を向く
        if (input._x < _ZERO)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}


//=================================================================================


/// <summary>
/// inputを可視化するための変数定義クラス
/// </summary>
[System.Serializable]
public class PlayerInput
{
    public float _x = 0;
    public bool _isAttack = false;
    public bool _isJump = false;
}

[System.Serializable]
public class Weapons
{
    public string name;
    public WeaponParameter param;
}