using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyboolData : ScriptableObject
{
    public bool Retrcking;
    public bool IsJump;
    public bool IsAttack;　//攻撃するか判定 
    public bool IsTracking = true;
    public bool IsReturn; //元の位置に戻っているか判定

    public float num = 1; //反転するときに使う数字（1で固定
}
