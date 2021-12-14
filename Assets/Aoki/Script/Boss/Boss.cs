using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{       
    public GameObject playerObj; //追跡ターゲット
    public SpriteRenderer BossSprite;

    [SerializeField]
    public BossData mono; // spriteデータ
    [SerializeField]
    private float HP;
    [SerializeField]
    private float AnimeSpeed; //spriteアニメのスピード  
    [SerializeField]
    private float TrackingSpeed;　//追跡のスピード    
    [SerializeField]
    private float WeaponForce;
    [SerializeField]
    private int ReScale;　//反転するときに使う大きさ
    [SerializeField]
    private int AttackRange;
    [SerializeField]
    private GameObject[] AttackEffect;
    [SerializeField]
    private GameObject[] SummonSpell;
    [SerializeField]
    private float[] AttackAnimeSpeed;　//spriteアニメのスピード
    [SerializeField]
    private float[] Spritetime;   // spriteループ用の変数
    [SerializeField]
    int[] MaxLeng; //spriteのマックス　0待機1追跡2ワープ
    [SerializeField]
    private int[] AttackCount;
    [SerializeField]
    private Vector2[] SpellPos;

    private bool _IsAttack;　//攻撃するか判定 
    private bool _IsReset;
    private bool _IsLook;
    private bool _IsHit;
    private bool _IsCount;
    public bool _IsSwitch;

    private Vector2 pos;　//自身の位置
    private Vector3 DefaultPos;　//最初の位置
    private Vector2 MapPos;　//二次元配列の位置
    private Vector2 scale;

    private int DefaultPosXInt;
    private int WarpPosX;
    private int anime;　//switch変数
    
    private int AttackPattern = 1;
    private float GetAttackRange; 
    private float num = 1; //反転するときに使う数字（1で固定
    private float DefaultSpeed;

    [HideInInspector]
    public bool _IsRetrcking;
    [HideInInspector]
    public bool _IsSpell;
    [HideInInspector]
    public bool _IsSummonSpell;
    [HideInInspector]
    public bool _IsTracking = true;
    [HideInInspector]
    public bool _IsNext;
    [HideInInspector]
    public bool _IsDefaultWarp;

    private int SpellCount;
    private int SummonCount = 3;
    int EnemyPositionX = 0; //二次元配列の敵の横
    int EnemyPositionY = 0; //二次元配列の敵の縦
       
    Maping map;
    Play play;

    public int HpState;
    public EnemyAiState aiState = EnemyAiState.Resporn;
    public enum EnemyAiState
    {
        Resporn,       //リスポーン
        IDLE,            //待機
        Tracking,            //移動
        ATTACK,        //停止して攻撃
        StorongAttack,
        Summon,
        Spell,
        StrongSpell,
        Warp,
        WarpAttack,
        Back,
    }


    // Start is called before the first frame update
    void Start()
    {      
        //------------二次元配列のスクリプト取得------------------
        GameObject en = GameObject.FindGameObjectWithTag("Map");
        map = en.GetComponent<Maping>();

        //------------二次元配列のプレイヤーのスクリプト取得------------------
        GameObject PlayerScript = GameObject.FindGameObjectWithTag("Player");
        play = PlayerScript.GetComponent<Play>();
        
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        BossSprite = boss.GetComponent<SpriteRenderer>();
  
        //-----------二次元配列からエネミーの位置取得--------
        for (int i = 0; i < map.stageArray.GetLength(0); i++)
        {
            for (int j = 0; j < map.stageArray.GetLength(1); j++)
            {
                if (map.stageArray[i, j] == 9)
                {
                    EnemyPositionX = j;
                    EnemyPositionY = i;
                }
            }
        }
        
        
        // PLAYERオブジェクトを取得
        playerObj = GameObject.FindGameObjectWithTag("Player");

        MapPos = transform.position;
        DefaultPos = transform.position;
        DefaultPosXInt = Mathf.FloorToInt(DefaultPos.x);
        DefaultSpeed = TrackingSpeed;

        MaxLeng[0] = mono.Resporn.GetLength(0);
        MaxLeng[1] = mono.Idel.GetLength(0);
        MaxLeng[2] = mono.move.GetLength(0);
        MaxLeng[3] = mono.Attack.GetLength(0);
        MaxLeng[4] = mono.Warp.GetLength(0);
        MaxLeng[5] = mono.WarpAttack.GetLength(0);
        MaxLeng[6] = mono.Spell.GetLength(0);
        MaxLeng[7] = mono.StrongAttack.GetLength(0);

    }

    // Update is called once per frame
    void Update()
    {

        MapMove();

        EnemyPos();

        ResetTime();

        AnimeMotion();

        EnemyTracking();

        Hit();

        HPMove();

    }
    //--------------------------HPによって行動変化---------------------
    private void HPMove()
    {
       
        switch (HpState)
        {
            case 1:

                break;

            case 2:
                print("Hp75%以下");
                _IsDefaultWarp = true;
                if (!_IsSwitch)
                {
                    aiState = EnemyAiState.Warp;
                    _IsSwitch = true;
                }
                break;

            case 3:
                print("Hp50%以下");
                _IsDefaultWarp = true;
                
                if (!_IsSwitch)
                {
                    aiState = EnemyAiState.Warp;
                    _IsSwitch = true;
                }
                break;

            case 4:
                print("Hp25%以下");
                aiState = EnemyAiState.Summon;
                break;
        }
    }
    //--------------------------switch文-------------------------------
    private void AnimeMotion()
    {
        scale = transform.localScale;
        switch (aiState)
        {            
            //--------------------------------------リスポーンアニメーション処理--------------------------------
            case EnemyAiState.Resporn:
                BossSprite.sprite = mono.Resporn[(int)Spritetime[0]];
                Spritetime[0] += Time.deltaTime * AnimeSpeed;

                if (Spritetime[0] >= MaxLeng[0])
                {                   
                    aiState = EnemyAiState.IDLE;
                                                             
                }
                break;
            //--------------------------------------待機アニメーション処理-------------------------------------- 
            case EnemyAiState.IDLE:
                _IsReset = false;
                BossSprite.sprite = mono.Idel[(int)Spritetime[1]];
                Spritetime[1] += Time.deltaTime * AnimeSpeed;

                if (Spritetime[1] >= MaxLeng[1])
                {
                    Spritetime[1] = 0;
                    AttackCount[1] = 0;
                    if (AttackPattern == 1 || AttackPattern == 2)
                    {
                        aiState = EnemyAiState.Tracking;
                    }
                }


                if (AttackCount[1] > 2)
                {
                    AttackPattern = 1;
                    AttackCount[0] = 0;
                }


                if (_IsNext == true)
                {
                    if (!_IsCount)
                    {
                        SpellCount += 1;
                        _IsCount = false;
                    }

                    aiState = EnemyAiState.StrongSpell;
                }

                if (_IsSummonSpell == true)
                {
                    Warp();
                    if (SummonCount == 0)
                    {
                        aiState = EnemyAiState.Resporn;
                    }
                }
                return;

            //--------------------------------------追跡アニメーション処理-----------------------------------------
            case EnemyAiState.Tracking:
                _IsLook = true;

                BossSprite.sprite = mono.move[(int)Spritetime[2]];
                Spritetime[2] += Time.deltaTime * AnimeSpeed;

                if (Spritetime[2] >= MaxLeng[2])
                {
                    Spritetime[2] = 0;
                }
                TrackingSpeed = DefaultSpeed;

                EnemyTracking();

                if (_IsAttack == false)
                {
                    if (_IsRetrcking == true)
                    {
                        num = -1;
                        scale.x = ReScale; //反転処理 　左
                    }
                    else if (_IsRetrcking == false)
                    {
                        num = 1;
                        scale.x = -ReScale; //反転処理 右
                    }
                }
                transform.localScale = scale;
                return;
            //--------------------------------------弱攻撃-----------------------------------------
            case EnemyAiState.ATTACK:
                _IsAttack = true;
                _IsLook = false;

                BossSprite.sprite = mono.Attack[(int)Spritetime[7]];
                Spritetime[7] += Time.deltaTime * AttackAnimeSpeed[2];

                if (Spritetime[7] >= MaxLeng[6] - 4 && Spritetime[7] <= MaxLeng[5] - 3)
                {
                    _IsHit = true;
                }

                if (Spritetime[7] >= MaxLeng[6])
                {
                    Spritetime[7] = 0;
                    _IsAttack = false;
                    AttackCount[0] += 1;
                    aiState = EnemyAiState.IDLE;
                }
                break;

            //------------------------------------召喚処理----------------------------------
            case EnemyAiState.Summon:
                AttackPattern = 2;
                _IsDefaultWarp = false;
                _IsReset = false;

                BossSprite.sprite = mono.Spell[(int)Spritetime[6]];
                Spritetime[6] += Time.deltaTime * AttackAnimeSpeed[1];
                
                GameObject SummonEn = GameObject.Find("MushroomS(Clone)");
               
                if (SpellCount < 1)
                {
                    _IsSummonSpell = false;
                    Instantiate(SummonSpell[0], new Vector2(SummonSpell[0].transform.position.x, SummonSpell[0].transform.position.y), Quaternion.identity); // 固定座標に魔法
                    SpellCount++;
                }

                if (SummonCount == 0)
                {
                    _IsSummonSpell = false;
                    Instantiate(SummonSpell[1], new Vector2(SummonSpell[1].transform.position.x, SummonSpell[1].transform.position.y), Quaternion.identity); // 固定座標に魔法
                    SummonCount = 3;
                }
                if (Spritetime[6] >= MaxLeng[6])
                {
                    _IsSummonSpell = true;

                    _IsReset = true;

                    aiState = EnemyAiState.IDLE;
                }
                break;

            //-------------------------------------最初のアニメーションに戻る---------------------------------------
            case EnemyAiState.Back:
                BossSprite.sprite = mono.Return[(int)Spritetime[3]];
                Spritetime[3] += Time.deltaTime * AnimeSpeed;
                break;

            //-------------------------------------ワープアニメーション処理-----------------------------------------
            case EnemyAiState.Warp:
                _IsTracking = false;

                if (_IsRetrcking == true)　//右を向いている
                {
                    WarpPosX = map.PlayerPositionX + 5;

                    if (map.stageArray[EnemyPositionY, WarpPosX] == 4)
                    {
                        WarpPosX = map.PlayerPositionX - 5;
                    }
                }
                if (_IsRetrcking == false)　//左を向いている
                {
                    WarpPosX = map.PlayerPositionX - 5;

                    if (map.stageArray[EnemyPositionY, WarpPosX] == 4)
                    {
                        WarpPosX = map.PlayerPositionX + 5;
                    }
                }


                BossSprite.sprite = mono.Warp[(int)Spritetime[4]];
                Spritetime[4] += Time.deltaTime * AnimeSpeed;


                if (_IsDefaultWarp == true && Spritetime[4] >= MaxLeng[4])
                {
                    WarpPosX = DefaultPosXInt;
                    this.transform.position = new Vector2(WarpPosX, transform.position.y);
                    aiState = EnemyAiState.WarpAttack;
                }

                if (_IsDefaultWarp == false && Spritetime[4] >= MaxLeng[4])
                {
                    Spritetime[5] = 0;
                    AttackCount[1] += 1;
                    aiState = EnemyAiState.WarpAttack;
                }
                break;

            //----------------------------------------ワープして攻撃------------------------------------------------
            case EnemyAiState.WarpAttack:
                Spritetime[4] = 0;
                _IsTracking = false;
                this.transform.position = new Vector2(WarpPosX, transform.position.y);

                if (_IsRetrcking == true)　//右を向いている
                {
                    num = -1;
                    scale.x = ReScale; //左に反転処理 　
                }
                if (_IsRetrcking == false) //左を向いている
                {
                    num = 1;
                    scale.x = -ReScale; //右に反転処理
                }

                BossSprite.sprite = mono.WarpAttack[(int)Spritetime[5]];
                Spritetime[5] += Time.deltaTime * AttackAnimeSpeed[0];

                if (_IsDefaultWarp == true && Spritetime[5] >= MaxLeng[5] - 10)
                {
                    _IsReset = true;
                    aiState = EnemyAiState.Summon;
                }


                if (Spritetime[5] >= MaxLeng[5])
                {
                    _IsReset = true;
                    aiState = EnemyAiState.IDLE;
                }

                transform.localScale = scale;
                break;

            //-------------------------------------------遠距離攻撃----------------------------------------------   
            case EnemyAiState.StrongSpell:
                _IsTracking = false;

                _IsReset = false;

                BossSprite.sprite = mono.Spell[(int)Spritetime[6]];
                Spritetime[6] += Time.deltaTime * AttackAnimeSpeed[1];

                if (SpellCount < 1)
                {
                    Instantiate(AttackEffect[1], new Vector2(-7, 8), Quaternion.identity); // 固定座標に魔法
                    SpellCount++;
                }

                if (_IsNext == true)
                {
                    _IsSpell = false;
                    Instantiate(AttackEffect[SpellCount], new Vector2(SpellPos[SpellCount].x, SpellPos[SpellCount].y), Quaternion.identity); // 固定座標に魔法
                    _IsNext = false;

                }
                if (Spritetime[6] >= MaxLeng[6])
                {
                    _IsSpell = true;

                    _IsReset = true;

                    aiState = EnemyAiState.IDLE;
                }
                break;

            //-------------------------------------------強攻撃----------------------------------------------          
            case EnemyAiState.StorongAttack:
                BossSprite.sprite = mono.StrongAttack[(int)Spritetime[8]];
                Spritetime[8] += Time.deltaTime * AttackAnimeSpeed[3];

                if (Spritetime[8] >= MaxLeng[7])
                {
                    aiState = EnemyAiState.IDLE;
                }
                break;

        }
    }

    private void Warp()
    {
        _IsReset = false;
        
        
            BossSprite.sprite = mono.Warp[(int)Spritetime[4]];
            Spritetime[4] += Time.deltaTime * AnimeSpeed;

            if (Spritetime[4] > MaxLeng[4])
            {
                Spritetime[4] = MaxLeng[4] - 1;
            }
    }
    //--------------------------ヒット判定-----------------------------
    private void Hit()
    {
        //--------------------ワープ攻撃をしている最中のアニメーション-------------
        if (Spritetime[5] >= MaxLeng[3] - 6 && Spritetime[5] <= MaxLeng[3] - 3)
        {
            if (_IsRetrcking == false)
            {
                //プレイヤーとボスとの距離を計算
                GetAttackRange = EnemyPositionX - map.PlayerPositionX;
            }

            if (_IsRetrcking == true)
            {
                //プレイヤーとボスとの距離を計算
                GetAttackRange = map.PlayerPositionX - EnemyPositionX;
            }

            if (GetAttackRange >= -AttackRange && GetAttackRange <= 0)
            {
                print("hitW");
            }
        }

        //--------------------弱.強攻撃をしている最中のアニメーション-------------
        if (_IsHit == true)
        {
            if (_IsRetrcking == false)
            {
                //プレイヤーとボスとの距離を計算
                GetAttackRange = EnemyPositionX - map.PlayerPositionX;
                _IsHit = false;
            }

            if (_IsRetrcking == true)
            {
                //プレイヤーとボスとの距離を計算
                GetAttackRange = map.PlayerPositionX - EnemyPositionX;
                _IsHit = false;
            }

            if (GetAttackRange >= -AttackRange && GetAttackRange <= 0)
            {
                print("hit");
            }
        }
    }

    private void ResetTime()
    {
        if (_IsReset == true)
        {
            for (int i = 0; i < Spritetime.GetLength(0); i++)
            {
                Spritetime[i] = 0;               
            }
        }
    }
    ////------------------二次元配列の位置更新-------------------------
    private void EnemyPos()
    {
        pos = transform.position;
        EnemyPositionX = Mathf.FloorToInt(pos.x);
        EnemyPositionY = Mathf.FloorToInt(pos.y);

    }
        //-------------------2次元配列管理-----------------------------
    private void MapMove()
    {        
        if (_IsRetrcking == false) //右向き
        {
            if (AttackPattern == 1)
            {
                //2つ右がプレイヤーだった場合攻撃
                if (EnemyPositionX + AttackRange >= map.PlayerPositionX && EnemyPositionX + AttackRange <= map.PlayerPositionX)
                {                   
                    aiState = EnemyAiState.ATTACK;
                }
            }
           

            //if (map.stageArray[EnemyPositionY, EnemyPositionX - 1] == 5)
            //{
            //    anime = 9;
            //}
        }

        if (_IsRetrcking == true)　//左向き
        {
            if (AttackPattern == 1)
            {
                //１つ左がプレイヤーだった場合攻撃
                if (EnemyPositionX - AttackRange <= map.PlayerPositionX && EnemyPositionX - AttackRange >= map.PlayerPositionX)
                {
                    aiState = EnemyAiState.ATTACK;
                }
            }
            
        }
    }
   
        //-------------------------追跡処理--------------------------------
    private void EnemyTracking()
    {
        if (_IsLook == true)
        {
            Vector3 pv = playerObj.transform.position;
            Vector3 ev = transform.position;

            float TrackingposX = pv.x - ev.x;
            float TrackingposY = pv.y - ev.y;

            float vx = 0f;
            float vy = 0f;

            float sp = 10f;

            // 減算した結果がマイナスであればXは減算処理
            if (TrackingposX < 0 || TrackingposX == 0)
            {
                _IsRetrcking = true;
                vx = -sp;
            }
            else
            {
                _IsRetrcking = false;
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
                transform.Translate(vx / TrackingSpeed, 0, 0);
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

        anime = 4;
    }

}
