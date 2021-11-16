using UnityEngine;

public class CharaController : MonoBehaviour
{
    // ---- Playerの見た目変更 ---- 

    /*
    // ジャンプのスプライトの数がAnimationTimeより短ければtrue
    bool isJumpAnim = animData.jump.Length > (int)_animationTime[_anim];
    // 剣攻撃のスプライトの数がAnimationTimeより短ければtrue
    bool isSwordAttackAnim = animData.swordAttack.Length > (int)_animationTime[_anim];
    // 槌攻撃のスプライトの数がAnimationTimeより短ければtrue
    bool isHammerAttackAnim = animData.hammerAttack.Length > (int)_animationTime[_anim];

    

    if (!input._isAttack)
    {
        // Jumpをしていなければ移動の入力の絶対値を切り上げした数を_animに入れる
        if (input._isJump)
        {
            charaStatus = CharacterStatus.Jump;
            _anim = 2;
        }
        else if (!input._isJump && rb.velocity.y < _ZERO)
        {
            charaStatus = CharacterStatus.Down;
            _animationTime[2] = 4;
            _anim = 2;
        }
        else
        {
            if (input._x != 0)
            {
                charaStatus = CharacterStatus.Move;
                _anim = 1;
            }
            else
            {
                charaStatus = CharacterStatus.Idle;
                _anim = 0;
            }
        }
    }
    else
    {
        charaStatus = CharacterStatus.swordAttack;
        _anim = 3;
    }

    switch (charaStatus)
    {
        case CharacterStatus.Idle:
            spriteRenderer.sprite = animData.idle[(int)_animationTime[_anim]];
            _animationTime[_anim] += Time.deltaTime * 5;
            break;
        case CharacterStatus.Move:
            spriteRenderer.sprite = animData.move[(int)_animationTime[_anim]];
            _animationTime[_anim] += Time.deltaTime * 9;
            break;
        case CharacterStatus.Jump:
            if (isJumpAnim)
            {
                spriteRenderer.sprite = animData.jump[(int)_animationTime[_anim]];
                _animationTime[_anim] += Time.deltaTime * 9;
            }
            break;
        case CharacterStatus.swordAttack:
            if (isSwordAttackAnim)
            {
                spriteRenderer.sprite = animData.swordAttack[(int)_animationTime[_anim]];
                _animationTime[_anim] += Time.deltaTime * 9;
            }
            else
            {
                input._isAttack = false;
                rb.useGravity = true;
                _animationTime[_anim] = _ZERO;
            }
            break;
        case CharacterStatus.hammerAttack:
            if (isHammerAttackAnim)
            {
                spriteRenderer.sprite = animData.hammerAttack[(int)_animationTime[_anim]];
                _animationTime[_anim] += Time.deltaTime * 9;
            }
            else
            {
                input._isAttack = false;
                rb.useGravity = true;
                _animationTime[_anim] = _ZERO;
            }
            break;
    }

    if (_maxAnimationCount[_anim] < _animationTime[_anim] && _anim <= 1)
    {
        _animationTime[_anim] = _ZERO;
    }
    */
}