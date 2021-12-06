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

    /// flooat
    // ジャンプしている時間
    protected float _jumpTimer = 0;
    // 落下している時間
    private float _fallTimer = 1;
    // 攻撃力
    protected float _attackPower = 0;
    // 落下速度
    private float _fallSpeed = 9;
    // ジャンプの高さ
    [SerializeField]
    private float _defaultJumpHeight = 4;
    // Rayの長さ
    private float[] _animationTime = { 0, 0, 0, 0, 0 };

    
    /// bool
    // 着地判定
    protected bool _isGround = false;
    // 記憶を持っているか
    protected bool _hasMemory = false;

    
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

    //マージしないといけないのでここに仮で記述してます。
    private float acceleration = default;

    /// <summary>
    /// ジャンプ関連の定義や状態の判定
    /// </summary>
    private void Jump()
    {
        bool _nowJump = false;
        //ジャンプ処理から返される加速値
        float jumpAccelerationValue = default;
        //落下処理から返される加速値
        float fallAccelerationValue = default;


        //攻撃中は停止
        if (input._isAttack)
        {
            CharacterMove.y = _ZERO;
            return;
        }

        // LayerMaskがGroundだったら着地
        if (Physics.Raycast(transform.position, Vector3.down, _ONE, LayerMask.GetMask("Ground")))
        {
            _isGround = true;
        }
        else
        {
            _isGround = false;
        }

        //ジャンプ中ならジャンプ処理を呼び、以下の判定は行わない
        if (_nowJump)
        {
            //加速値を計算する
            AccelerationValueCalculation(jumpAccelerationValue);
            return;
        }

        //着地状態
        if (_isGround)
        {
            //動かさない
            CharacterMove.y = _ZERO;

            //ジャンプ可能なタイミングでジャンプが押された
            if (!input._isAttack && input._isJump)
            {
                //ジャンプ開始
                _nowJump = true;
            }
        }
        //非着地状態
        else
        {
            //落下
            CharaFallProcess(fallAccelerationValue);

        }
    }

    /// <summary>
    /// ジャンプ処理の加速値を計算し加速度に加算する
    /// </summary>
    /// <param name="returnValue"></param>
    /// <returns></returns>
    private void AccelerationValueCalculation(float jumpAccelerationValue)
    {
        

        //加速度にジャンプの加速値を加算する
        acceleration += jumpAccelerationValue;
    }

    /// <summary>
    /// キャラの落下処理(擬似重力処理)の加速値を計算し加速度から減算する
    /// </summary>
    private void CharaFallProcess(float fallAccelerationValue)
    {



        //加速度にジャンプの加速値を加算する
        acceleration += fallAccelerationValue;
    }


    /// <summary>
    /// 着地判定、velocityのy軸に値を入れる
    /// </summary>
    //private void Jump()
    //{
    //    //print(_fallTimer);
    //    print(jumpCurve.Evaluate(_jumpTimer));


    //    // LayerMaskがGroundだったら着地
    //    if (Physics.Raycast(transform.position, Vector3.down, _ONE, LayerMask.GetMask("Ground")))
    //    {
    //        _isGround = true;
    //    }
    //    else
    //    {
    //        _isGround = false;
    //    }

    //    // ジャンプしてる時間を加算
    //    if (input._isJump && _jumpTimer < 0.5f)
    //    {
    //        _jumpTimer += Time.deltaTime;
    //    }
    //    //**0.5f以上ジャンプしてたら終了？
    //    else
    //    {
    //        _jumpTimer = _ZERO;
    //        input._isJump = false;
    //    }

    //    // 落下してる時間を加算
    //    if (!_isGround && !input._isJump)
    //    {
    //        _fallTimer += Time.deltaTime;
    //    }
    //    else
    //    {
    //        _fallTimer = _ONE;
    //    }


    //    // 地面にいたらJumpする地面にいなかったらしない
    //    if (input._isJump && _isGround && !input._isAttack)
    //    {
    //        charaStatus = CharacterStatus.Jump;
    //        CharacterMove.y = jumpCurve.Evaluate(_jumpTimer) * _defaultJumpHeight;
    //        Debug.Log("aaafaa");
    //    }
    //    //**自由落下の状態
    //    else if(!input._isJump && !_isGround)
    //    {
    //        charaStatus = CharacterStatus.Fall;
    //        CharacterMove.y = -_fallSpeed * _fallTimer;
    //    }
    //    //地面にいてジャンプが押されていないもしくは攻撃中
    //    else if(!input._isJump && _isGround || input._isAttack)
    //    {
    //        CharacterMove.y = _ZERO;
    //    }
    //}


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