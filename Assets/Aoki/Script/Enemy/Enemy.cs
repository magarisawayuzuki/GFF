using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : UnityEngine.MonoBehaviour
{
    SpriteRenderer EnemySprite;　//  エネミー
    public GameObject player; //追跡ターゲット
    //public GameObject[] Mob;
    
    [SerializeField]
    public EnemyAnime Anime; // spriteデータ
    [SerializeField]
    public EnemyData data; //変数データ   
    [SerializeField]
    private float AttackRange;
    [SerializeField]
    private float WaitRange;
    [SerializeField]
    private int[] MaxLeng; //spriteのマックス
    [SerializeField]
    public float[] Spritetime;   // spriteループ用の変数

    public int anime;　//switch変数

    [HideInInspector]
    public bool _Retrcking;
    [HideInInspector]
    public bool _IsJump;
    [HideInInspector]
    public bool _IsAttack = true;　//攻撃するか判定 
  
    public bool _IsTracking;
    [HideInInspector]
    public bool _IsReturn; //元の位置に戻っているか判定
    [HideInInspector]
    public float num = 1; //反転するときに使う数字（1で固定
    
    private Vector2 pos;　//自身の位置
    private Vector3 DefaultPos; //最初の位置
      
    private float GetAttackRange;

    public bool _InEnemy;
    public bool _IsTrackingWait;

    int EnemyPositionX = 0; //二次元配列の横
    int EnemyPositionY = 0; //二次元配列の縦
  
    Maping map;

    public Vector2[] DefaultMobPos;
    public int[] MobPositionX;
    public int[] MobPositionY;
    // Start is called before the first frame update
    void Start()
    {
        //------------二次元配列のスクリプト取得------------------
        GameObject en = GameObject.FindGameObjectWithTag("Map");
        map = en.GetComponent<Maping>();

        //SpriteRendererコンポーネントを取得
        EnemySprite = GetComponent<SpriteRenderer>();

        // PLAYERオブジェクトを取得
        player = GameObject.FindGameObjectWithTag("Player");
        //最初の座標
        DefaultPos = transform.position; 

        //spriteの最大数を取得
        MaxLeng[0] = Anime.Resporn.GetLength(0);
        MaxLeng[1] = Anime.move.GetLength(0);
        MaxLeng[2] = Anime.Idel.GetLength(0);
        MaxLeng[3] = Anime.Attack.GetLength(0);
        MaxLeng[4] = Anime.TakeHit.GetLength(0);
        MaxLeng[5] = Anime.Death.GetLength(0);

        //座標を整数に変換　XY
        EnemyPositionX = Mathf.FloorToInt(DefaultPos.x); 
        EnemyPositionY = Mathf.FloorToInt(DefaultPos.y); 

        //spriteアニメーションの時間をリセット
        for (int i = 0; i < Spritetime.GetLength(0); i++)
        {
            Spritetime[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {             
        MapMove();

        EnemyPos();

        Bool();

        AnimeMotion();

        EnemyTracking(); //追跡処理
    }

    //------------------二次元配列の座標更新---------------------------
    private void EnemyPos()
    {
        //常に座標を取得
        pos = transform.position;

        //座標を整数に変換　XY
        EnemyPositionX = Mathf.FloorToInt(pos.x); 
        EnemyPositionY = Mathf.FloorToInt(pos.y); 

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

        _InEnemy = false;
        anime = 3;
    }
   
    //--------------------------switch文-------------------------------
    private void AnimeMotion()
    {
        Vector2 scale = transform.localScale;

        switch (anime)
        {
            case 1:　//----------------------リスポーンアニメーション処理--------------------------------
                EnemySprite.sprite = Anime.Resporn[(int)Spritetime[0]];
                Spritetime[0] += Time.deltaTime * data.AnimeSpeed[0];
                
                if (Spritetime[0] >= MaxLeng[0])
                {
                    _IsTracking = true;
                    _InEnemy = true;
                }
                
                break;

            case 2:  //-------------------------追跡アニメーション処理---------------------------------- 
                _IsTracking = true;
                EnemySprite.sprite = Anime.move[(int)Spritetime[1]];
                Spritetime[1] += Time.deltaTime * data.AnimeSpeed[1];

                if (Spritetime[1] >= MaxLeng[1])
                {
                    Spritetime[1] = 0;
                }

                EnemyTracking(); //追跡処理
                
                if (_Retrcking == true)
                {
                    num = -1;
                    EnemySprite.flipX = false; //反転処理 　左
                }
                else if (_Retrcking == false)
                {
                    num = 1;
                    EnemySprite.flipX = true; //反転処理 右
                }
                transform.localScale = scale;
                return;

            case 3:　//最初のアニメーションに戻る
                EnemySprite.sprite = Anime.Death[(int)Spritetime[2]];
                Spritetime[2] += Time.deltaTime * data.AnimeSpeed[0];
                break;

            case 4://---------------------------待機-----------------------------
                _IsTracking = false;
              
                EnemySprite.sprite = Anime.Idel[(int)Spritetime[3]];
                Spritetime[3] += Time.deltaTime * data.AnimeSpeed[2];
               
                if (Spritetime[3] >= MaxLeng[2])
                {
                    Spritetime[3] = 0;
                    if (_InEnemy == true && _IsTrackingWait == false)
                    {
                        _IsAttack = true;
                    }
                }

                if (_Retrcking == true)
                {
                    num = -1;
                    EnemySprite.flipX = false; //反転処理 　左
                }
                else if (_Retrcking == false)
                {
                    num = 1;
                    EnemySprite.flipX = true; //反転処理 右
                }
                transform.localScale = scale;
                return;

            case 5: // ---------------------------攻撃--------------------------------- 
                _IsTracking = false;
                EnemySprite.sprite = Anime.Attack[(int)Spritetime[4]];
                Spritetime[4] += Time.deltaTime * data.AnimeSpeed[3];
               
                if(Spritetime[4] >= MaxLeng[3] - 1)
                {
                    if(_Retrcking == false)
                    {
                        GetAttackRange = EnemyPositionX - map.PlayerPositionX;
                    }

                    if (_Retrcking == true)
                    {
                        GetAttackRange = map.PlayerPositionX - EnemyPositionX;
                    }

                    if (GetAttackRange >= -AttackRange && GetAttackRange <= 0)
                    {
                        print("hit");
                    }
                }


                if (Spritetime[4] >= MaxLeng[3])
                {
                    Spritetime[4] = 0;
                    _IsAttack = false;                 
                }                
                break;

            case 6: //----------------------------ヒット--------------------------------                              
                EnemySprite.sprite = Anime.TakeHit[(int)Spritetime[5]];
                Spritetime[5] += Time.deltaTime * data.AnimeSpeed[4];

                 if (Spritetime[5] >= MaxLeng[4])
                 {
                     print("hit");
                     Spritetime[5] = 0;
                     
                     anime = 2;                        
                 }                
                break;
        }
    }

  
    //-----------------二次元配列によって行動変化----------------------
    private void MapMove()
    {

        GameObject[] EnemyM = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < EnemyM.Length; i++)
        {
            DefaultMobPos[i] = EnemyM[i].transform.position;
            MobPositionX[i] = Mathf.FloorToInt(DefaultMobPos[i].x);
            MobPositionY[i] = Mathf.FloorToInt(DefaultMobPos[i].y);
           
            if (num == 1) //右向き
            {
                //2つ右がプレイヤーだった場合攻撃
                if (EnemyPositionX + AttackRange >= map.PlayerPositionX && EnemyPositionX + AttackRange <= map.PlayerPositionX)
                {
                    if (_IsAttack == true)
                    {
                        anime = 5;
                    }
                    else
                    {
                        anime = 4;
                    }
                }
                else if (_InEnemy == true && _IsTrackingWait == false)
                {
                    anime = 2;
                }
            
                if (EnemyPositionX + WaitRange >= MobPositionX[1] && EnemyPositionX + WaitRange <= MobPositionX[1])
                {
                    print("右待機");
                    _IsTrackingWait = true;
                    anime = 4;
                }
                else if (_IsTrackingWait == true)
                {
                    _IsTrackingWait = false;
                    anime = 2;
                }
            }
            if (num == -1) //左向き
            {

                //2つ左がプレイヤーだった場合攻撃
                if (EnemyPositionX - AttackRange >= map.PlayerPositionX && EnemyPositionX - AttackRange <= map.PlayerPositionX)
                {
                    if (_IsAttack == true)
                    {
                        anime = 5;
                    }
                    else
                    {
                        anime = 4;
                    }
                }
                else if (_InEnemy == true && _IsTrackingWait == false)
                {
                    anime = 2;
                }

                if (EnemyPositionX - WaitRange >= MobPositionX[0] && EnemyPositionX - WaitRange <= MobPositionX[0])
                {
                    print("左待機");

                    _IsTrackingWait = true;
                    anime = 4;
                }
                else if (_IsTrackingWait == true)
                {
                    _IsTrackingWait = false;
                    anime = 2;
                }
            }
        }
    }

    //--------------------------bool処理-------------------------------
    private void Bool()
    {
        //----------------ジャンプ処理----------------------
        //if (IsJump == true)
        //{
        //    transform.Translate(transform.up * Time.deltaTime * data.jumpForce);
        //}

        //----------------元の座標に戻っている---------------
        if (_IsReturn == true)
        {
            transform.Translate(transform.right * Time.deltaTime * data.ReturnSpeed * num);
        }

        //---------------元の座標についたとき最初のspriteに戻る---------------------
        if (_IsTracking == false && _IsReturn == true)
        {
            if (pos.x <= DefaultPos.x && _Retrcking == true)　//左に向いている
            {
                _IsReturn = false;
                Spritetime[2] = 0;
                anime = 3;
            }
            if (pos.x >= DefaultPos.x && _Retrcking == false)　//右に向いている
            {
                _IsReturn = false;
                Spritetime[2] = 0;
                anime = 3;
            }
        }
    }

    //--------------------------追跡処理-------------------------------
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
            _Retrcking = true;
            vx = -sp;
        }
        else
        {
            _Retrcking = false;
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
        if (_IsTracking == true)
        {            
            transform.Translate(vx / data.TrackingSpeed, 0, 0);
        }
        
    }

}
