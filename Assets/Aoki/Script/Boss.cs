using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : EnemyController
{           
    public SpriteRenderer BossSprite;

    [SerializeField]
    private BossData mono;             //spriteデータ      
    [SerializeField]
    private float AnimeSpeed;          //spriteアニメのスピード  
    [SerializeField]
    private float TrackingSpeed;       //追跡のスピード   
    [SerializeField]
    private GameObject[] AttackEffect; //魔法攻撃のprefab
    [SerializeField]
    private GameObject[] SummonSpell;  //召喚魔法のprefab   
    [SerializeField]
    private int[] AttackCount; 　　　　//攻撃回数
    [SerializeField]
    private Vector2[] SpellPos;       //魔法攻撃の位置

    #region bool
    private bool _IsReset;        //spriteのアニメーションをリセットする判定 
    private bool _IsHit;          //弱攻撃当たり判定
    private bool _IsStrongHit;    //強攻撃当たり判定
    private bool _IsAttackWait;   //強攻撃を一旦止める判定
    private bool _IsWarp;         //ワープ判定
    private bool _IsSwitch;       //行動を１度切り替える判定
    private bool _IsSwitch2;      //..
    private bool _IsSwitch3;      //..
    private bool _IsSummon;       //敵を召喚したか判定
    private bool _IsSecondAttack; //次の攻撃に移るか判定
    private bool _IsThirdAttack;  //次の攻撃に移るか判定
    private bool _IsRetrcking;    //反転の判定
    private bool _IsDefaultWarp;  //元の位置に戻るワープ判定
    private bool _EndMagic;

    [System.NonSerialized]
    public bool _IsSpell;　　　　 //魔法を撃ったか判定
    [System.NonSerialized]
    public bool _IsSummonSpell;   //召喚したか判定
    [System.NonSerialized]
    public bool _IsNext;          //次の魔法攻撃か判定
    [System.NonSerialized]
    public bool _IsCount;         //魔法攻撃のカウントをするか判定
    [System.NonSerialized]
    public bool _IsHitSpell;      //魔法当たり判定
    #endregion
   
    #region int
    private int DefaultPosXInt;   //二次元配列で使うためのint型位置
    private int WarpPosX;　　　　 //..
      
    private int AttackPattern = 1;   //行動パターン
             
    private int SpellCount;       //魔法攻撃回数
    private int SummonCount;      //召喚回数
    private GameObject[] Smush;   //召喚きのこ
    private GameObject[] Stroll;  //召喚トロール

    #endregion

    [SerializeField]
    private Vector2 WarpPos;     //ワープ位置
    CharacterController chara;
    private float damage;

    [SerializeField]
    private int HpState;　//ｈｐ行動パターン
    [SerializeField]
    private EnemyAiState aiState = EnemyAiState.Resporn;
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
        TakeHit,
    }

    private AudioManager audios = default;

    // Start is called before the first frame update
    void Start()
    {
        audios = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        //------------二次元配列のスクリプト取得------------------
        GameObject en = GameObject.FindGameObjectWithTag("Map");
        map = en.GetComponent<Maping>();

        //------------プレイヤーのスクリプト取得------------------
        player = GameObject.FindGameObjectWithTag("Player");
        chara = player.GetComponent<CharacterController>();

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
        　
        //-----------最初の位置取得、int型に変更--------------
        DefaultPos = transform.position;
        DefaultPosXInt = Mathf.FloorToInt(DefaultPos.x);


        #region spriteの最大数

        MaxLeng[0] = mono.Resporn.GetLength(0);
        MaxLeng[1] = mono.Idel.GetLength(0);
        MaxLeng[2] = mono.move.GetLength(0);
        MaxLeng[3] = mono.Attack.GetLength(0);
        MaxLeng[4] = mono.Warp.GetLength(0);
        MaxLeng[5] = mono.WarpAttack.GetLength(0);
        MaxLeng[6] = mono.Spell.GetLength(0);
        MaxLeng[7] = mono.StrongAttack.GetLength(0);
        MaxLeng[8] = mono.Return.GetLength(0);
        MaxLeng[9] = mono.TakeHit.GetLength(0);
        #endregion;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        MapMove();

        EnemyPos();

        ResetTime();

        AnimeMotion();

        EnemyTracking();

        Hit();

        HPMove();

        print(_life);
    }

    public override void CharaLifeCalculation(float damage, int knockBack, int weapon)
    {
        //倍率を代入
        damageScaleSword = data.swordScale;
        damageScaleHammer = data.hammerScale;

        if(_NowAttack == false)
        {
            _IsTakeHit = true;
            aiState = EnemyAiState.TakeHit;
        }
        base.CharaLifeCalculation(damage, knockBack, weapon);
    }

    private void OnBecameVisible()　//カメラ内処理
    {        
        for (int i = 0; i < Spritetime.GetLength(0); i++)
        {
            Spritetime[i] = 0;
        }
        AttackPattern = 1;
        aiState = EnemyAiState.Resporn;

    }

    private void OnBecameInvisible() //カメラ外処理
    {       
        AttackPattern = 0;
        aiState = EnemyAiState.Back;
    }

    //--------------------------HPによって行動変化---------------------
    //HP100%の時.リスポーン→（プレイヤー追跡→弱攻撃→繰り返し）
    //HP75%の時. 柔敵召喚　→（召喚した敵が死んだら）追跡弱 ×5　→　繰り返し
    //HP50%の時. 複数魔法　→　追跡強　→　繰り返し
    //HP25%の時. 硬敵召喚　→（召喚した敵が死んだら）ワープ攻撃　×3　→　複数魔法（撃っている時が隙）　→　繰り返し
    private void HPMove()
    {            
        if(_life <= 180 && _life >= 121)
        {
            HpState = 2;
        }
        else if (_life <= 120 && _life >= 61)
        {
            HpState = 3;
        }
        else if (_life <= 60)
        {
            HpState = 4;
        }
       
        switch (HpState)
        {
            case 1:

                break;
            #region HP75%以下
            case 2:
                print("Hp75%以下");
                AttackPattern = 2;　//攻撃パターンを切り替え
                _IsDefaultWarp = true;　//元の位置に戻るbool
               
                if (!_IsSwitch) //一度だけ切り替え
                {
                    aiState = EnemyAiState.Warp;　//ワープのアニメーション処理へ
                    _IsSwitch = true;
                }
                break;
            #endregion

            #region HP50%以下
            case 3:
                print("Hp50%以下");
                AttackPattern = 3; //攻撃パターンを切り替え

               
                if (!_IsSwitch2)　//一度だけ切り替え
                {
                    _IsDefaultWarp = true; //元の位置に戻るbool
                    aiState = EnemyAiState.Warp;　//ワープのアニメーション処理へ
                    _IsSwitch2 = true;
                }
                break;
            #endregion

            #region HP25%以下
            case 4:
                print("Hp25%以下");
                AttackPattern = 4;　//攻撃パターンを切り替え              

                if (!_IsSwitch3)　//一度だけ切り替え
                {
                    _IsDefaultWarp = true;　//元の位置に戻るbool
                    aiState = EnemyAiState.Warp;　//ワープのアニメーション処理へ
                    _IsSwitch3 = true;
                }               
                break;
#endregion
        }
    }

    //-------------------------------switch文-------------------------------
    private void AnimeMotion()
    {       
        switch (aiState)
        {           
            
            case EnemyAiState.Resporn:
                #region リスポーン
                //レイヤーを攻撃をくらわないように変更
                gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

                _IsReset = false;
                _NowAttack = false;
                _IsTracking = false;

                BossSprite.sprite = mono.Resporn[(int)Spritetime[0]];　
                Spritetime[0] += Time.deltaTime * AnimeSpeed;

                if (Spritetime[0] >= MaxLeng[0])　
                {
                    Spritetime[1] = 0;
                    _IsWarp = false;
                    aiState = EnemyAiState.IDLE; 
                                                             
                }
                break;
            #endregion;
                        
            case EnemyAiState.IDLE:
                #region　待機
                // Audio再生
                audios.bossSE = (AudioManager.BossSE)0;
                audios.AudioChanger("Boss");

                _NowAttack = false;
                _IsReset = false;

                //ワープ中は攻撃をくらわないように
                if (_IsWarp == true)
                {
                    gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                }
                else
                {
                    gameObject.layer = LayerMask.NameToLayer("Enemy"); //待機状態中のレイヤーを元のレイヤーに
                }

                BossSprite.sprite = mono.Idel[(int)Spritetime[1]];　
                Spritetime[1] += Time.deltaTime * AnimeSpeed;

                //-------------------アタックパターンが2の時、召喚したエネミー取得-----------------------
                if (AttackPattern == 2)　
                {
                    SummonCount = GameObject.FindGameObjectsWithTag("SoftS").Length;
                    Smush = GameObject.FindGameObjectsWithTag("SoftS");
                }
                //-----------------------攻撃パターンが4の時、召喚したエネミー取得---------------------
                if (AttackPattern == 4)　
                {
                    SummonCount = GameObject.FindGameObjectsWithTag("HardS").Length;
                    Stroll = GameObject.FindGameObjectsWithTag("HardS");
                }

                if (Spritetime[1] >= MaxLeng[1])　
                {
                    Spritetime[1] = 0;
                    AttackCount[1] = 0; 
                    if (AttackPattern == 1)　
                    {
                        _IsAttack = true;
                        aiState = EnemyAiState.Tracking;　
                    }

                    //攻撃パターンが2かつ召喚した敵が死んだ時
                    if (AttackPattern == 2 && _IsSummon == false)　
                    {
                        _IsSecondAttack = true;
                        _IsAttack = true;
                        aiState = EnemyAiState.Tracking;　
                    }

                    if (AttackPattern == 3 && SpellCount >= 4)
                    {
                        _IsAttackWait = false;
                        _IsDefaultWarp = false;
                        _IsAttack = true;
                        aiState = EnemyAiState.Tracking;
                    }

                    //攻撃パターンが4かつ召喚中ではなく三回目の攻撃ではない時
                    if (AttackPattern == 4 && _IsSummon == false && _IsThirdAttack == false)　
                    {
                        _IsDefaultWarp = false;　
                        _IsReset = true;　
                        aiState = EnemyAiState.Warp;　
                    }
                }
              
                if (_IsSummonSpell == true && _IsSummon == true)　
                {                   
                    if (SummonCount == 0)　
                    {
                        _IsSummon = false;
                        Warp();　//元の位置に戻るワープ
                        aiState = EnemyAiState.Resporn;　//ステージに戻ってくる
                    }
                }

                //-------------------------------次の魔法を撃つ時-----------------------------
                if (_IsNext == true)　
                {
                    if (_IsCount == true)　
                    {
                        SpellCount += 1;　
                        _IsCount = false;                   
                        aiState = EnemyAiState.StrongSpell;　
                    }
                }

                //-----------------------------魔法を撃った回数が4回以上の時------------------------------
                if (SpellCount >= 4 && _EndMagic == true && AttackPattern == 3)　
                {
                    _IsReset = true;
                    _EndMagic = false;
                    Warp(); //元の位置に戻るワープ
                    aiState = EnemyAiState.Resporn;　//ステージに戻ってくる
                }

                //-----------------------------強攻撃をした回数が4回の時----------------------------
                if (AttackCount[1] >= 4 && AttackPattern == 3)　
                {
                    SpellCount = 0;　
                    _IsCount = true;　
                    _IsSummonSpell = false;　
                    aiState = EnemyAiState.StrongSpell; //遠距離魔法状態に遷移
                }

                //--------------------------ワープ攻撃回数が3回の時----------------------------
                if (AttackCount[2] >= 3 && AttackPattern == 4 && _IsThirdAttack == false)　
                {                 
                    SpellCount = 0;　
                    _IsCount = true;　
                    _IsSummonSpell = false;　
                    _IsDefaultWarp = true;　
                    _IsThirdAttack = true;　
                    aiState = EnemyAiState.Warp;　
                }                           
                return;
            #endregion;
                       
            case EnemyAiState.Tracking:
                #region　追跡
                _IsLook = true; 

                _IsTracking = true;

                _NowAttack = false;
                BossSprite.sprite = mono.move[(int)Spritetime[2]];　
                Spritetime[2] += Time.deltaTime * AnimeSpeed;

                if (Spritetime[2] >= MaxLeng[2])
                { 
                    Spritetime[2] = 0;
                }                

                if (_NowAttack == false)　
                {
                    if (_IsRetrcking == true)　//右を向いてる時
                    {                      
                        BossSprite.flipX = false; //反転処理 　
                    }
                    else  //左を向いてる時
                    {                       
                        BossSprite.flipX = true; //反転処理                       
                    }
                }
              
                return;
            #endregion;
                        
            case EnemyAiState.ATTACK:
                #region 弱攻撃
                // Audio再生
                audios.bossSE = (AudioManager.BossSE)3;
                audios.AudioChanger("Boss");

                _NowAttack = true;
              
                _IsLook = false;                
                _IsTracking = false;

                BossSprite.sprite = mono.Attack[(int)Spritetime[7]]; 
                Spritetime[7] += Time.deltaTime * AnimeSpeed;

                //spriteが攻撃中の時
                if (Spritetime[7] >= MaxLeng[3] - 4 && Spritetime[7] <= MaxLeng[3] - 3) 
                {
                    _IsHit = true;　//攻撃が当たったかHit()で判定
                }

                if (Spritetime[7] >= MaxLeng[3]) 
                {
                    Spritetime[7] = 0;
                    _IsAttack = false;　
                    AttackCount[0] += 1;　//弱攻撃回数
                    aiState = EnemyAiState.IDLE;　
                }
                break;
            #endregion
                
            case EnemyAiState.Summon:
                #region 召喚
                // Audio再生
                audios.bossSE = (AudioManager.BossSE)1;
                audios.AudioChanger("Boss");

                _NowAttack = true;
                _IsDefaultWarp = false;　
                _IsReset = false;
                _IsLook = true;

                BossSprite.sprite = mono.Spell[(int)Spritetime[6]];
                Spritetime[6] += Time.deltaTime * AnimeSpeed;

                //魔法回数が0回の時             
                if (SpellCount < 1 && AttackPattern == 2)
                {
                    _IsSummonSpell = false;
                    Instantiate(SummonSpell[0], new Vector2(SummonSpell[0].transform.position.x, SummonSpell[0].transform.position.y), Quaternion.identity); // 固定座標に魔法  
                    _IsSummon = true;
                    SpellCount++;
                }

                if (SpellCount < 1 && AttackPattern == 4)
                {
                    _IsSummonSpell = false;
                    Instantiate(SummonSpell[1], new Vector2(SummonSpell[1].transform.position.x, SummonSpell[1].transform.position.y), Quaternion.identity); // 固定座標に魔法 
                    _IsSummon = true;
                    SpellCount++;
                }
               
                if (Spritetime[6] >= MaxLeng[6])
                {
                    _IsSummonSpell = true;
                   
                    _IsReset = true;

                    aiState = EnemyAiState.IDLE;
                }
                break;
            #endregion
           
            case EnemyAiState.Back:
                #region 最初のアニメーションに戻る

                _IsLook = false;
                _IsTracking = false;
                BossSprite.sprite = mono.Return[(int)Spritetime[3]];
                Spritetime[3] += Time.deltaTime * AnimeSpeed;

                if (Spritetime[3] >= MaxLeng[8])
                {
                    if (_isDeath == true)
                    {
                        Instantiate(kakera, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                        gameObject.SetActive(false);
                        // Audio再生
                        audios.bossSE = (AudioManager.BossSE)6;
                        audios.AudioChanger("Boss");
                    }
                    else
                    {
                        Spritetime[3] = MaxLeng[8] - 1;
                    }
                }
                break;
            #endregion
            
            case EnemyAiState.Warp:
                #region ワープ
                // Audio再生
                audios.bossSE = (AudioManager.BossSE)5;
                audios.AudioChanger("Boss");

                _NowAttack = true;
                _IsTracking = false;

                _IsReset = false;
                gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

                if (_IsRetrcking == true && _IsDefaultWarp == false)　//右を向いている
                {
                    //ワープ先が壁の場合ワープ先を変更
                    if (map.stageArray[EnemyPositionY, WarpPosX] == 4)
                    {
                        WarpPosX = map.PlayerPositionX + 6;
                    }
                    else
                    {
                        WarpPosX = map.PlayerPositionX - 6;
                    }
                }
                if (_IsRetrcking == false)　//左を向いている
                {
                    //ワープ先が壁の場合ワープ先を変更
                    if (map.stageArray[EnemyPositionY, WarpPosX] == 4)
                    {
                        WarpPosX = map.PlayerPositionX - 6;
                    }
                    else
                    {
                        WarpPosX = map.PlayerPositionX + 6;
                    }
                }


                BossSprite.sprite = mono.Warp[(int)Spritetime[4]];
                Spritetime[4] += Time.deltaTime * AnimeSpeed;

                if (Spritetime[4] >= MaxLeng[4])
                {
                    if (_IsDefaultWarp == true)
                    {
                        Spritetime[5] = 0;
                        //所定の位置にワープ
                        this.transform.position = new Vector2(WarpPos.x, WarpPos.y);
                        aiState = EnemyAiState.WarpAttack;
                    }

                    else
                    {
                        Spritetime[5] = 0;
                        //ワープ攻撃のカウント
                        AttackCount[2] += 1;

                        aiState = EnemyAiState.WarpAttack;
                    }
                }
                break;
            #endregion
           
            case EnemyAiState.WarpAttack:
                #region ワープ攻撃
                Spritetime[4] = 0;
                
                _IsTracking = false;
                //レイヤーを元に戻す
                gameObject.layer = LayerMask.NameToLayer("Enemy");

                //ワープ攻撃の場合
                if (_IsDefaultWarp == false)
                {
                    this.transform.position = new Vector2(WarpPosX, transform.position.y);
                }

                if (_IsRetrcking == true)
                {                  
                    BossSprite.flipX = false; //反転処理 　左                  
                }
                else 
                {                  
                    BossSprite.flipX = true; //反転処理 右                   
                }

                BossSprite.sprite = mono.WarpAttack[(int)Spritetime[5]];
                Spritetime[5] += Time.deltaTime * AnimeSpeed;

                //ワープ攻撃する前に判定
                if (_IsDefaultWarp == true && Spritetime[5] >= MaxLeng[5] - 10)
                {
                    //召喚処理に遷移
                    if (AttackPattern == 2 || AttackPattern == 4 && _IsThirdAttack == false)
                    {
                        _IsReset = true;
                        _IsNext = false;
                        _IsSpell = false;
                        SpellCount = 0;
                        aiState = EnemyAiState.Summon;
                    }
                    //魔法攻撃に遷移
                    if (AttackPattern == 3 || AttackPattern == 4 && _IsThirdAttack == true)
                    {
                        _IsReset = true;
                        _IsSpell = false;
                        _IsSummonSpell = false;
                        SpellCount = 0;
                        aiState = EnemyAiState.StrongSpell;
                    }
                }

                //ワープ攻撃している際中のアニメーション
                if (Spritetime[5] >= MaxLeng[5] - 6 && Spritetime[5] <= MaxLeng[5] -3 && _IsDefaultWarp == false)
                {
                    // Audio再生
                    audios.bossSE = (AudioManager.BossSE)6;
                    audios.AudioChanger("Boss");
                    _NowAttack = true;
                    _IsStrongHit = true;
                }

                if (Spritetime[5] >= MaxLeng[5])
                {
                    _IsReset = true;
                    aiState = EnemyAiState.IDLE;
                }             
                break;
            #endregion
           
            case EnemyAiState.StrongSpell:
                #region 魔法攻撃

                _NowAttack = true;
                _IsAttackWait = true;
                _IsLook = true;
                _IsTracking = false;
                _IsReset = false;
              
                AttackCount[1] = 0;
                AttackCount[2] = 0;
               
                BossSprite.sprite = mono.Spell[(int)Spritetime[6]];
                Spritetime[6] += Time.deltaTime * AnimeSpeed;

                if (SpellCount < 1)
                {
                    _IsSpell = false;
                    Instantiate(AttackEffect[1], new Vector2(SpellPos[0].x,SpellPos[0].y), Quaternion.identity); // 固定座標に魔法
                    SpellCount++;
                }

                if (_IsNext == true && SpellCount >=2 && SpellCount<= 3)
                {
                    _IsSpell = false;
                    Instantiate(AttackEffect[SpellCount], new Vector2(SpellPos[SpellCount].x, SpellPos[SpellCount].y), Quaternion.identity); // 固定座標に魔法                  
                    _IsNext = false;
                }

                //魔法攻撃が回数以上になった時
                if(SpellCount >= 4)
                {
                    _IsReset = true;

                    _IsNext = false;

                    _EndMagic = true;　//魔法を終了

                    //アタックパターンが４の時に次の攻撃に
                    if (_IsThirdAttack == true)
                    {                                                                      
                        _IsThirdAttack = false;
                        _IsDefaultWarp = true;
                        aiState = EnemyAiState.Warp;
                    }
                    else
                    {
                        aiState = EnemyAiState.IDLE;
                    }
                }

                if (Spritetime[6] >= MaxLeng[6])
                {
                    // Audio再生
                    audios.bossSE = (AudioManager.BossSE)2;
                    audios.AudioChanger("Boss");

                    _IsSpell = true;

                    _IsReset = true;

                    aiState = EnemyAiState.IDLE;
                }
                break;
            #endregion
              
            case EnemyAiState.StorongAttack:
                #region 強攻撃
                // Audio再生
                audios.bossSE = (AudioManager.BossSE)4;
                audios.AudioChanger("Boss");

                _NowAttack = true;
                _IsLook = false;
                _IsTracking = false;
                _IsReset = false;
                
                BossSprite.sprite = mono.StrongAttack[(int)Spritetime[8]];
                Spritetime[8] += Time.deltaTime * AnimeSpeed;

                //強攻撃している際中のアニメーション
                if (Spritetime[8] >= MaxLeng[7] - 6 && Spritetime[8] <= MaxLeng[7] - 3)
                {
                    _IsStrongHit = true;
                }
                else
                {
                    _IsStrongHit = false;
                }

                if (Spritetime[8] >= MaxLeng[7])                  
                {
                    _IsReset = true;
                    //強攻撃のカウント
                    AttackCount[1] += 1;
                    _IsStrongHit = false;
                    _IsAttack = false;
                    aiState = EnemyAiState.IDLE;
                }

                if (AttackCount[1] >= 4)
                {
                    SpellCount = 0;
                    _IsDefaultWarp = true;
                    aiState = EnemyAiState.Warp;
                }
                break;
            #endregion
            case EnemyAiState.TakeHit:
                #region 攻撃をくらった

                //攻撃をくらっている際は追跡しない
                _IsTracking = false;

                BossSprite.sprite = mono.TakeHit[(int)Spritetime[9]];
                Spritetime[9] += Time.deltaTime * 3;

                if (Spritetime[9] >= MaxLeng[9] && _isDeath == false)
                {
                    print("Takehit");
                    Spritetime[9] = 0;
                    _IsTakeHit = false;
                    aiState = EnemyAiState.IDLE;
                }
                break;
                #endregion
        }
    }

    //-------------------------------ワープ処理-----------------------------
    private void Warp()
    {
        _IsReset = false;
        _IsWarp = true;
        //レイヤーを攻撃をくらわないように変更
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        //ワープ先を最初の位置に変更
        WarpPosX = DefaultPosXInt;
        this.transform.position = new Vector2(WarpPosX, DefaultPos.y);

        BossSprite.sprite = mono.Warp[(int)Spritetime[4]];
        Spritetime[4] += Time.deltaTime * AnimeSpeed;

        if (Spritetime[4] > MaxLeng[4])
        {           
            Spritetime[4] = MaxLeng[4] - 1;
        }
    }

    //-------------------------------ヒット判定-----------------------------
    private void Hit()
    {        
        //--------------------弱.強攻撃をしている最中のアニメーション-------------
        if (_IsHit == true || _IsStrongHit == true)
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

            if (GetAttackRange >= -AttackRange && GetAttackRange <= 0 && _IsStrongHit == false )
            {
                damage = 8;
                chara.CharaLifeCalculation(damage, 0, 0);
                print("hit");
            }

            if (GetAttackRange >= -(AttackRange + 1)&& GetAttackRange <= 0 && _IsStrongHit == true && _IsHit == false)
            {
                damage = 10;
                chara.CharaLifeCalculation(damage, 0, 0);
                print("hitStrong");
                _IsStrongHit = false;
            }

        }

        if(_IsHitSpell == true)
        {
            damage = 12;
            chara.CharaLifeCalculation(damage, 0, 0);
            print("HitSpell");
            _IsHitSpell = false;
        }
    }

    //--------------------------spriteのリセット処理------------------------
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

    //------------------------二次元配列の位置更新--------------------------
    private void EnemyPos()
    {
        pos = transform.position;
        EnemyPositionX = Mathf.FloorToInt(pos.x);
        EnemyPositionY = Mathf.FloorToInt(pos.y);

    }

    //-----------------------2次元配列管理・攻撃判定------------------------
    private void MapMove()
    {        
        if (_IsRetrcking == false && EnemyPositionX + AttackRange >= map.PlayerPositionX && EnemyPositionX + AttackRange <= map.PlayerPositionX) //右向き
        {            
            //3つ右がプレイヤーだった場合攻撃
            if (AttackPattern == 1 &&  _IsAttack == true||
                AttackPattern == 2  && _IsSecondAttack == true && _IsAttack == true)
            {                   
                aiState = EnemyAiState.ATTACK;
            }
            else if(AttackPattern == 1 &&_IsAttack == false || AttackPattern == 2 && _IsAttack == false)
            {
                aiState = EnemyAiState.IDLE;
            }
            
            //3つ右がプレイヤーだった場合攻撃
            if (AttackPattern == 3 && AttackCount[1] <= 3 && _IsAttackWait == false && _IsDefaultWarp == false &&_IsAttack == true)
            {
                aiState = EnemyAiState.StorongAttack;
            }
            else if (AttackPattern == 3 && _IsAttack == false)
            {
                aiState = EnemyAiState.IDLE;
            }
        }

        if (_IsRetrcking == true && EnemyPositionX - AttackRange <= map.PlayerPositionX && EnemyPositionX - AttackRange >= map.PlayerPositionX)　//左向き
        {            
            //3つ左がプレイヤーだった場合攻撃
            if (AttackPattern == 1 && _IsAttack == true ||
                AttackPattern == 2 && _IsSecondAttack == true && _IsAttack == true)
            {
                aiState = EnemyAiState.ATTACK;
            }

            else if (AttackPattern == 1 && _IsAttack == false || AttackPattern == 2 && _IsAttack == false)
            {
                aiState = EnemyAiState.IDLE;
            }
            //3つ右がプレイヤーだった場合攻撃
            if (AttackPattern == 3 && AttackCount[1] <= 3 && _IsAttackWait == false && _IsDefaultWarp == false && _IsAttack == true)
            {
                aiState = EnemyAiState.StorongAttack;
            }
            else if (AttackPattern == 3 && _IsAttack == false)
            {
                aiState = EnemyAiState.IDLE;
            }

        }
    }
   
    //-------------------------------追跡処理--------------------------------
    private void EnemyTracking()
    {
        if (_IsLook == true)
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

    
 
}
