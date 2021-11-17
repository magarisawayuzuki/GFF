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
    AnimationSpriteData animData;




    [SerializeField]
    protected PlayerInput input;
    protected Rigidbody rb = null;
    protected SpriteRenderer spriteRenderer = null;
    protected CharacterStatus charaStatus;


    /// Vector3
    private Vector3 CharacterMove = new Vector3();

    /// int
    // 記憶の個数
    protected int _weaponMemoryCount = 0;
    protected int _memoryCount = 0;
    private int _anim = 0;
    private int[] _maxAnimationCount = { 4, 8, 5, 13, 0 };


    /// float
    // 攻撃力
    protected float _attackPower = 0;
    // Jumpの高さ
    private float _y = 5;
    private float time;
    // Rayの長さ
    private float[] _animationTime = { 0, 0, 0, 0, 0 };


    /// bool
    protected bool _isGround = false;
    protected bool _hasMemory = false;


    /// <summary>
    /// const
    /// </summary>
    protected const int _ZERO = 0;
    private const float _GROUNDDISTANCE = 3f;

    //==========================================================


    /// <summary>
    /// 処理を呼び出す
    /// </summary>
    private void Update()
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
        rb.velocity = CharacterMove;

        /*
        if (input._isJump)
        {
            time += Time.deltaTime;
            Debug.Log(time);
        }

        
        if (time > animationCurve.length)
        {
            rb.useGravity = true;
            time = 0;
        }
        */
    }



    /// <summary>
    /// CharaStatus(Charaの状態)
    /// </summary>
    public enum CharacterStatus
    {
        Idle,
        Move,
        Jump,
        Down,
        swordAttack,
        hammerAttack,
        Death,
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
        // 攻撃状態じゃなければ
        if (!input._isAttack)
        {
            CharacterMove.x = input._x * 10;
        }
        else
        {
            CharacterMove.x = _ZERO;
        }
    }

    /// <summary>
    /// 基本的な攻撃挙動のみ記述しておき、子クラスで書き換える
    /// </summary>
    public virtual void Attack()
    {

    }

    private void Jump()
    {
        /*
        // Jump処理、攻撃処理の時は重力を外す
        if (input._isAttack || input._isJump)
        {
            rb.useGravity = false;
        }
        */

        // 地面にいたらJumpする地面にいなかったらしない
        if (input._isJump && _isGround && !input._isAttack)
        {
            CharacterMove.y = _y;
            //CharacterMove.y = animationCurve.Evaluate(Time.time) * _y;
        }
        else if (!input._isAttack)
        {
            CharacterMove.y = rb.velocity.y;
        }
        // 攻撃中は上下移動しない
        else
        {
            CharacterMove.y = _ZERO;
        }

        Debug.DrawRay(transform.position, Vector3.down * _GROUNDDISTANCE, Color.red);
        // LayerMaskがGroundだったら着地
        if (rb.velocity.y <= 0 && Physics.Raycast(transform.position, Vector3.down, _GROUNDDISTANCE, LayerMask.GetMask("Ground")))
        {
            _isGround = true;
            input._isJump = false;
            _animationTime[2] = _ZERO;
        }
        else
        {
            _isGround = false;
        }
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