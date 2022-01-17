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

    private float time = 0;

    private Animation anima;

    private List<AnimationClip> animas = new List<AnimationClip>();

    private PlayerController playerc;

    public bool start = false;

    private void Start()
    {

        anima = this.gameObject.GetComponent<Animation>();

        playerc = GetComponentInParent<PlayerController>();

        //animationを取得
        foreach(AnimationState anim in anima)
        {
            this.animas.Add(anim.clip);
        }

        
    }

    /// <summary>
    /// アニメーションを切り替える判定を行う
    /// </summary>
    /// <param name="charaStats"></param>
    public void AnimationChenge(CharacterController.CharacterStatus charaStats)
    {
        print((int)charaStats);
        switch ((int)charaStats)
        {
            case (int)copy_status.Idle:
                anima.Play(this.animas[0].name);

                return;
            case (int)copy_status.Move:
                anima.Play(this.animas[1].name);

                return;
            case (int)copy_status.Jump:
                anima.Play(this.animas[2].name);

                return;
            case (int)copy_status.Fall:

                //anima.Play(this.animas[0].name);

                return;
            case (int)copy_status.swordAttack:
                anima.Play(this.animas[4].name);

                return;
            case (int)copy_status.hammerAttack:
                anima.Play(this.animas[5].name);

                return;
            case (int)copy_status.Death:
                anima.Play(this.animas[7].name);

                return;
            case (int)copy_status.Damage:
                anima.Play(this.animas[6].name);

                return;
        }
    }

    public void EndAttack()
    {
        GameObject weapon = GameObject.FindWithTag("Weapon");
        playerc.EndAttack(weapon);
    }
}
