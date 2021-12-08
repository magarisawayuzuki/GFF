﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラに共通する処理を記述
/// </summary>
public class CharacterController : MonoBehaviour
{
    [SerializeField]
    protected CharaParameter charaData = default;
    /*
    [SerializeField]
    Weapons[] weapon = null;
    
    [SerializeField]
    AnimationSpriteData animData = null;
    */
    [SerializeField]
    AnimationCurve jumpCurve = default;

    [SerializeField]
    protected PlayerInput input = default;
    protected SpriteRenderer spriteRenderer = default;
    // Charaのstatsを入れる
    protected CharacterStatus charaStatus = default;


    /// Vector3
    // キャラの移動
    protected Vector3 CharacterMove = new Vector3();


    /// int
    /*
    private int _anim = 0;
    private int[] _maxAnimationCount = { 4, 8, 5, 13, 0 };
    */
    protected int _weapon = 0;
    protected int _knockBack = 0;
    protected float _damage = 0;


    /// flooat
    //
    protected float _speedDownTimer = 0;
    // ジャンプしている時間
    protected float _jumpTimer = 0;
    // 落下している時間
    private float _fallTimer = 1;
    // 攻撃力
    protected float _attackPower = 0;
    // 落下速度
    private float _fallSpeed = 9;
    // ジャンプの高さ
    private float _y = 4;
    // Rayの長さ
    private float[] _animationTime = { 0, 0, 0, 0, 0 };


    /// bool
    // 着地判定
    protected bool _isGround = false;
    // 記憶を持っているか
    protected bool _hasMemory = false;
    protected bool _isSpeedDown = false;
    protected bool _isInvincible = false;

    /// const
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
        transform.position += CharacterMove * Time.deltaTime;
    }


    //=================================================================================


    /// <summary>
    /// 子クラスからの入力を受ける
    /// </summary>
    /// <returns></returns>
    public virtual PlayerInput InputMethod()
    {
        if (input._x != 0 && !input._isJump && input._isAttack)
        {
            charaStatus = CharacterStatus.Move;
        }
        else if(!input._isJump && !input._isAttack)
        {
            charaStatus = CharacterStatus.Idle;
        }
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
            CharacterMove.x = input._x * 10;
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

        // ジャンプしてる時間を加算
        if (input._isJump && _jumpTimer < 0.5f)
        {
            _jumpTimer += Time.deltaTime;
        }
        else
        {
            _jumpTimer = _ZERO;
            input._isJump = false;
        }

        // 落下してる時間を加算
        if (!_isGround)
        {
            _fallTimer += Time.deltaTime;
        }
        else
        {
            _fallTimer = _ONE;
        }


        // 地面にいたらJumpする地面にいなかったらしない
        if (input._isJump && _isGround && !input._isAttack)
        {
            charaStatus = CharacterStatus.Jump;
            CharacterMove.y = jumpCurve.Evaluate(_jumpTimer) * _y;
            Debug.Log("aaafaa");
        }
        else if(!input._isJump && !_isGround)
        {
            charaStatus = CharacterStatus.Fall;
            CharacterMove.y = -_fallSpeed * _fallTimer;
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
    public float _wasx = 0;
    public bool _isAttack = false;
    public bool _isJump = false;
}

[System.Serializable]
public class Weapons
{
    public string name;
    public WeaponParameter param;
}