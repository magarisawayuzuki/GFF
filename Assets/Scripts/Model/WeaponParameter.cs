using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Create WeaponParameter")]
public class WeaponParameter : ScriptableObject
{
    public int power;
    public int weight;
    public int attackRange;
    public int attackSpeed;
}
