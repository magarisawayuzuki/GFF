using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Create AnimationSpriteData")]
public class EnemyAnime : ScriptableObject
{
    public Sprite[] Idel;
    public Sprite[] Resporn;
    public Sprite[] Return;
    public Sprite[] move;   
    public Sprite[] Attack;
    public Sprite[] Death;
    public Sprite[] TakeHit;


}

