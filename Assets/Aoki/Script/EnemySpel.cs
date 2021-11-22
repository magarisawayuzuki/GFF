using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpel : MonoBehaviour
{
    SpriteRenderer EnemySprite;
    public GameObject player;　//追跡ターゲット
    public GameObject tama;

    int anime;
    [SerializeField]
    EnemyDate mono;

    [SerializeField]
    float ResAnimeSpeed;
    [SerializeField]
    float AttackAnimeSpeed;    
    [SerializeField]
    private float targetTime = 1.0f;
    [SerializeField]
    private float currentTime = 0;
    [SerializeField]
    float[] time;    

    [SerializeField]
    private int ReScale;　//反転するときに使う大きさ
    [SerializeField]
    private int HP;  //エネミーの体力

    private bool OnTracking;
    private bool Retrcking;　//追跡するとき反転するか判定
    private bool Shot;

    private Vector2 pos;　//自身の位置
    private Vector2 DefaultPos;  
    private float num = 1; //反転するときに使う数字（1で固定

    int MovLeng;
    int AttackLeng;

    int MoveX = 0;
    int MoveY = 0;

    int playerPositionX = 0;
    int playerPositionY = 0;

    // Start is called before the first frame update
    void Start()
    {
        EnemySprite = GetComponent<SpriteRenderer>();
        AttackLeng = mono.Attack.GetLength(0);
    }

    // Update is called once per frame
    void Update()
    {
       
        Vector2 scale = transform.localScale;
        pos = transform.position;

        switch (anime)
        {
            case 1:
                EnemySprite.sprite = mono.Resporn[(int)time[0]];
                time[0] += Time.deltaTime * ResAnimeSpeed;
                Invoke("Tracking", 1f);
                break;

            case 2:

                shot();
                EnemyTracking();
                if (Retrcking == true)
                {
                    num = -1;
                    scale.x = -ReScale; //反転処理 
                }
                else if (Retrcking == false)
                {
                    num = 1;
                    scale.x = ReScale; //反転処理
                }
                transform.localScale = scale;
              
                EnemySprite.sprite = mono.Attack[(int)time[1]];
                time[1] += Time.deltaTime * AttackAnimeSpeed;

                if (time[1] >= AttackLeng)
                {
                    time[1] = 0;
                }
                return;
        }

        if(HP <= 0)
        {
            Invoke("Resporn", 20f);
        }
      
    }

    private void OnBecameVisible()
    {
        time[0] = 0;
        time[1] = 0;

        anime = 1;
    }

    private void OnBecameInvisible()
    {
        time[0] = 0;
        time[1] = 0;

        anime = 3;
    }

    private void Tracking()
    {
        anime = 2;
    }

    private void shot()
    {       
            currentTime += Time.deltaTime;       
        
        if (targetTime < currentTime)
        {            
                    currentTime = 0;
                   
                    //敵の座標を変数posに保存
                    var pos = this.gameObject.transform.position;
                    //弾のプレハブを作成
                    var t = Instantiate(tama, new Vector3(2, 3, 0), Quaternion.identity) as GameObject;
                    //弾のプレハブの位置を敵の位置にする
                    t.transform.position = new Vector3(pos.x , pos.y, pos.z);
                    //敵からプレイヤーに向かうベクトルをつくる
                    //プレイヤーの位置から敵の位置（弾の位置）を引く
                    Vector2 vec = player.transform.position - pos;
                    //弾のRigidBody2Dコンポネントのvelocityに先程求めたベクトルを入れて力を加える
                    t.GetComponent<Rigidbody2D>().velocity = vec;                       
                                
        }       

    }


    private void EnemyTracking()
    {

        Vector3 pv = player.transform.position;
        Vector3 ev = transform.position;

        float TrackingposX = pv.x - ev.x;
        float TrackingposY = pv.y - ev.y;

        float vx = 0f;
        float vy = 0f;

        float sp = 10f;

        // 減算した結果がマイナスであればXは減算処理
        if (TrackingposX < 0 || TrackingposX == 0)
        {
            Retrcking = true;
            vx = -sp;
        }
        else
        {
            Retrcking = false;
            vx = sp;
        }

        // 減算した結果がマイナスであればYは減算処理
        if (TrackingposY < 0)
        {
            vy = -sp;
        }
        else
        {
            vy = sp;
        }

        //transform.Translate(vx / 1000, vy / 5000, 0);
    }
}

