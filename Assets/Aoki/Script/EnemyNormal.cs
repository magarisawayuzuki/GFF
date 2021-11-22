using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormal : UnityEngine.MonoBehaviour
{
    private Rigidbody2D rb;
    SpriteRenderer EnemySprite;　//  エネミー
    public GameObject player;　//追跡ターゲット
   
    [SerializeField]
    public EnemyDate mono; // spriteデータ
    　
    [SerializeField]
    private float AnimeSpeed;　//spriteアニメのスピード
    [SerializeField]
    private float TrackingSpeed;　//追跡のスピード
    [SerializeField]
    private float ReturnSpeed; //元の位置のスピード
    [SerializeField]
    private float jumpForce;　//ジャンプパワー
    [SerializeField]
    private float WeaponForce;
    [SerializeField]
    private int ReScale;　//反転するときに使う大きさ
    [SerializeField]
    public  float[] Spritetime;   // spriteループ用の変数
    public int anime;　//switch変数

    private bool Retrcking;　
    private bool Jump;
    
    private Vector2 pos;　//自身の位置
    private Vector3 DefaultPos;　//最初の位置
    private Vector2 MapPos;　//二次元配列の位置

    private float num = 1; //反転するときに使う数字（1で固定
 
    public bool OnAttack;　//攻撃するか判定 
    public bool Tracking　= true;
    public bool Return;　//元の位置に戻っているか判定

    int MaxLeng; //spriteのマックス
    
    int MoveX = 0; 
    int MoveY = 0;

    int EnemyPositionX = 0; //二次元配列の横
    int EnemyPositionY = 0; //二次元配列の縦


    Maping map;
    // Start is called before the first frame update
    void Start()
    {
        //------------二次元配列のスクリプト取得------------------
        GameObject en = GameObject.FindGameObjectWithTag("Map");
        map = en.GetComponent<Maping>();

        //-----------二次元配列からエネミーの位置取得、更新--------
        for (int i = 0; i < map.stageArray.GetLength(0); i++)
        {
            for (int j = 0; j < map.stageArray.GetLength(1); j++)
            {
                if (map.stageArray[i, j] == 8)
                {
                    EnemyPositionX = j;
                    EnemyPositionY = i;
                }
            }
        }

        rb = GetComponent<Rigidbody2D>();
　　　  EnemySprite = GetComponent<SpriteRenderer>();

        // PLAYERオブジェクトを取得
        player = GameObject.Find("Player");
        MapPos = transform.position;
        DefaultPos = transform.position;
        MaxLeng = mono.move.GetLength(0);
    }

    // Update is called once per frame
    void Update()
    {    
        Vector2 scale = transform.localScale;
        pos = transform.position;
        
        if(Input.GetKeyDown(KeyCode.A))
        {
            rb.AddForce(transform.right * WeaponForce * -num, ForceMode2D.Impulse);
        }

        //----------------2次元配列管理--------------------
        if (num == 1) //右向き
        {
            //１つ右が壁だった場合ジャンプ
            if (map.stageArray[EnemyPositionY, EnemyPositionX + 1] == 3 && map.stageArray[EnemyPositionY, EnemyPositionX + 2] == 2)
            {
                Jump = true;
            }
            //１つ右が行けないエリアだった場合反転
            if (map.stageArray[EnemyPositionY, EnemyPositionX + 1] == 4 && map.stageArray[EnemyPositionY - 1, EnemyPositionX + 1] == 4)
            {
                Tracking = false;
                Return = true;
                Retrcking = true;
            }

        }

        if (num == -1)　//左向き
        {
            //１つ左が壁だった場合ジャンプ
            if (map.stageArray[EnemyPositionY, EnemyPositionX - 1] == 3 && map.stageArray[EnemyPositionY, EnemyPositionX - 2] == 2)
            {
                Jump = true;
            }
            //１つ左が行けないエリアだった場合反転
            if (map.stageArray[EnemyPositionY, EnemyPositionX - 1] == 4 && map.stageArray[EnemyPositionY - 1, EnemyPositionX - 1] == 4)
            {
                Tracking = false;
                Return = true;
                Retrcking = false;
            }
        }

        EnemyPos();

        //----------------ジャンプ処理----------------------
        if (Jump == true)
        {
            transform.Translate(transform.up * Time.deltaTime * jumpForce);
        }

        //----------------元の位置に戻っている---------------
        if(Return == true)
        {
          transform.Translate(transform.right * Time.deltaTime * ReturnSpeed * num);
        }

        //---------------元の位置についたとき最初のspriteに戻る---------------------
        if (Tracking == false)
        {                    
            if(pos.x <= DefaultPos.x && Retrcking == true)　//左に向いている
            {               
                Return = false;
                anime = 3;           
            }
            if (pos.x >= DefaultPos.x && Retrcking == false)　//右に向いている
            {
                Return = false;
                anime = 3;            
            }
        }


        switch (anime)
        {
            case 1:　//リスポーンアニメーション処理
                EnemySprite.sprite = mono.Resporn[(int)Spritetime[0]];
                Spritetime[0] += Time.deltaTime * AnimeSpeed;
                Tracking = true;
                Invoke("PlayerTracking", 1f);
                break;

            case 2:  //追跡アニメーション処理            
                EnemySprite.sprite = mono.move[(int)Spritetime[1]];
                Spritetime[1] += Time.deltaTime * AnimeSpeed;

                if(Spritetime[1] >= MaxLeng)
                {
                    Spritetime[1] = 0;
                }

                EnemyTracking(); //追跡処理
                
                //transform.Translate(transform.up * Time.deltaTime * jumpForce);

                if (Retrcking == true)
                {
                    num = -1;
                    scale.x = ReScale; //反転処理 　左
                }
                else if (Retrcking == false)
                {
                    num = 1;
                    scale.x = -ReScale; //反転処理 右
                }
                transform.localScale = scale;
                return;

            case 3:　//最初のアニメーションに戻る
                EnemySprite.sprite = mono.Return[(int)Spritetime[2]];
                Spritetime[2] += Time.deltaTime * AnimeSpeed;
                break;

        }

    }

    //----------------二次元配列の位置更新-----------------------------
    private void EnemyPos()
    {
        if (pos.x >= MapPos.x + 1)
        {
            MoveX = 1;
            MapPos.x = pos.x;
            if (MoveX == 1)
            {
                EnemyPositionX += 1;
                MoveX = 0;
            }
        }

        if (pos.x <= MapPos.x - 1)
        {
            MoveX = -1;
            MapPos.x = pos.x;
            if (MoveX == -1)
            {
                EnemyPositionX -= 1;
                MoveX = 0;
            }
        }

        if (pos.y >= MapPos.y + 1.2)
        {
            MoveY = 1;
            MapPos.y = pos.y;
            if (MoveY == 1)
            {
                EnemyPositionY += 1;
                MoveY = 0;
                Jump = false;
            }
        }
        if (pos.y <= MapPos.y - 1.1)
        {
            MoveY = -1;
            MapPos.y = pos.y;
            if (MoveY == -1)
            {
                EnemyPositionY -= 1;
                MoveY = 0;
            }
        }
       
    }

    private void OnBecameVisible()　//カメラ内処理
    {
        for (int i = 0; i < Spritetime.GetLength(0); i++)
        {
            Spritetime[i] = 0;
        }

        anime = 1;
                            
    }

    private void OnBecameInvisible() //カメラ外処理
    {
        for (int i = 0; i < Spritetime.GetLength(0); i++)
        {
            Spritetime[i] = 0;
        }

        anime = 3;
    }

    private void PlayerTracking()
    {
        anime = 2;
    }

    //-------------------------追跡処理---------------------------
    private void EnemyTracking()
    {
        if (Tracking == true)
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

            //transform.Translate(vx / MoveSpeed, vy / 5000, 0);
            transform.Translate(vx / TrackingSpeed, 0, 0);
        }
    }

}
