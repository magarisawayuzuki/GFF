using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{    
    public float TrackingSpeed;　//追跡のスピード

    public float ReturnSpeed; //元の位置のスピード
   
    public float jumpForce;　//ジャンプパワー
    
    public float WeaponForce;

    public float[] AnimeSpeed;　//spriteアニメのスピード 
          
}
