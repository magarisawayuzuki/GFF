using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{
    public float speed = 100.0F;    // 移動早さ
   
    
    // Use this for initialization
    void Start()
    {
       
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    //PlayerPosition();

    //    // 矢印キーの入力情報を取得
    //    var h = Input.GetAxis("Horizontal");
    //    var v = Input.GetAxis("Vertical");

    //    // 移動する向きを作成する
    //    Vector2 direction = new Vector2(h, v).normalized;

    //    // 移動する向きとスピードを代入 
    //    GetComponent<Rigidbody2D>().velocity = direction * speed;
    //}

}

