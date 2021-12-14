using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    enum copy_status
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

    /// <summary>
    /// アニメーションを切り替える判定を行う
    /// </summary>
    /// <param name="charaStats"></param>
    public void AnimationChenge(CharacterController.CharacterStatus charaStats)
    {
        switch ((int)charaStats)
        {
            case (int)copy_status.Idle:


                return;
            case (int)copy_status.Move:


                return;
            case (int)copy_status.Jump:


                return;
            case (int)copy_status.Fall:


                return;
            case (int)copy_status.swordAttack:


                return;
            case (int)copy_status.hammerAttack:


                return;
            case (int)copy_status.Death:


                return;
            case (int)copy_status.Damage:


                return;
        }
    }
}
