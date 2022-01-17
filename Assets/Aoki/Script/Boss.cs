using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : EnemyController
{           
    public SpriteRenderer BossSprite;

    [SerializeField]
    public BossData mono; // spriteデータ   
    [SerializeField]
    private float HP;
    [SerializeField]
    private float AnimeSpeed; //spriteアニメのスピード  
    [SerializeField]
    private float TrackingSpeed; //追跡のスピード   
    [SerializeField]
    private GameObject[] AttackEffect;
    [SerializeField]
    private GameObject[] SummonSpell;    
    [SerializeField]
    private int[] AttackCount;
    [SerializeField]
    private Vector2[] SpellPos;
    
    private bool _IsReset;   
    private bool _IsHit;
    private bool _IsStrongHit;
    private bool _IsAttackWait;
    private bool _IsWarp;
    private bool _IsSwitch;
    private bool _IsSwitch2;
    private bool _IsSwitch3;

    private bool _IsSummon;
    private bool _IsSecondAttack;
    private bool _IsThirdAttack;
    private float _SummonTime;
  
    private int DefaultPosXInt;
    private int WarpPosX;
      
    private int AttackPattern = 1;   
    private float DefaultSpeed;

    [System.NonSerialized]
    public bool _IsRetrcking;
    [System.NonSerialized]
    public bool _IsSpell;
    [System.NonSerialized]
    public bool _IsSummonSpell;   
    [System.NonSerialized]
    public bool _IsNext;
    [System.NonSerialized]
    public bool _IsDefaultWarp;
    [System.NonSerialized]
    public bool _IsCount;
    [System.NonSerialized]
    public bool _IsHitSpell;

    private int SpellCount;
    private int SummonCount;
    private GameObject[] Smush;
    private GameObject[] Stroll;
   
    CharacterController chara;
    private float damage;

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
                   
        DefaultPos = transform.position;
        DefaultPosXInt = Mathf.FloorToInt(DefaultPos.x);
        DefaultSpeed = TrackingSpeed;
        box = GetComponent<BoxCollider>();
        boxcenter = box.center;


        MaxLeng[0] = mono.Resporn.GetLength(0);
        MaxLeng[1] = mono.Idel.GetLength(0);
        MaxLeng[2] = mono.move.GetLength(0);
        MaxLeng[3] = mono.Attack.GetLength(0);
        MaxLeng[4] = mono.Warp.GetLength(0);
        MaxLeng[5] = mono.WarpAttack.GetLength(0);
        MaxLeng[6] = mono.Spell.GetLength(0);
        MaxLeng[7] = mono.StrongAttack.GetLength(0);
        MaxLeng[8] = mono.Return.GetLength(0);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!InGameToPauseUI_2._isStaticPause)
        {
            base.Update();

            MapMove();

            EnemyPos();

            ResetTime();

            AnimeMotion();

            EnemyTracking();

            Hit();

            HPMove();
        }
    }

    public override void CharaLifeCalculation(float damage, int knockBack, int weapon)
    {
        //倍率を代入
        damageScaleSword = data.swordScale;
        damageScaleHammer = data.hammerScale;

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
    private void HPMove()
    {    //HP100%の時.リスポーン→（プレイヤー追跡→弱攻撃→繰り返し）
         //HP75%の時. 柔敵召喚　→（召喚した敵が死んだら）追跡弱 ×5　→　繰り返し
         //HP50%の時. 複数魔法(撃っている時が隙　→　追跡強　→　繰り返し
         //HP25%の時. 硬敵召喚　→（召喚した敵が死んだら）ワープ攻撃　×3　→　複数魔法（撃っている時が隙）　→　繰り返し    
        switch (HpState)
        {
            case 1:

                break;

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

            case 3:
                print("Hp50%以下");
                AttackPattern = 3; //攻撃パターンを切り替え

                _IsAttack = false;
                if (!_IsSwitch2)　//一度だけ切り替え
                {
                    _IsDefaultWarp = true; //元の位置に戻るbool
                    aiState = EnemyAiState.Warp;　//ワープのアニメーション処理へ
                    _IsSwitch2 = true;
                }
                break;

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
        }
    }
    //--------------------------switch文-------------------------------
    private void AnimeMotion()
    {       
        switch (aiState)
        {            
            //--------------------------------------リスポーンアニメーション処理--------------------------------
            case EnemyAiState.Resporn:
                gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

                BossSprite.sprite = mono.Resporn[(int)Spritetime[0]];　
                Spritetime[0] += Time.deltaTime * AnimeSpeed;

                if (Spritetime[0] >= MaxLeng[0])　
                {
                    Spritetime[1] = 0;
                    _IsWarp = false;
                    aiState = EnemyAiState.IDLE; 
                                                             
                }
                break;
            //--------------------------------------待機アニメーション処理-------------------------------------- 
            case EnemyAiState.IDLE:
                // Audio再生
                audios.bossSE = (AudioManager.BossSE)0;
                audios.AudioChanger("Boss");

                _IsReset = false;

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
               
                if (AttackPattern == 2)　//アタックパターンが2の時、召喚したエネミー取得
                {
                    SummonCount = GameObject.FindGameObjectsWithTag("SoftS").Length;
                    Smush = GameObject.FindGameObjectsWithTag("SoftS");
                }

                if (AttackPattern == 4)　//攻撃パターンが4の時、召喚したエネミー取得
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
                        aiState = EnemyAiState.Tracking;　
                    }

                    //攻撃パターンが2かつ召喚中の時
                    if (AttackPattern == 2 && _IsSummon == false)　
                    {
                        _IsSecondAttack = true;　
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
                    Warp();　//元の位置に戻るワープ
                    if (SummonCount == 0)　
                    {
                        _IsSummon = false;　                        
                        aiState = EnemyAiState.Resporn;　//ステージに戻ってくる
                    }
                }

                //次の魔法を撃つ時
                if (_IsNext == true)　
                {
                    if (_IsCount == true)　
                    {
                        SpellCount += 1;　
                        _IsCount = false;                   
                        aiState = EnemyAiState.StrongSpell;　
                    }
                }

                //魔法回数が4回の時
                if (SpellCount >= 4 && AttackPattern == 3)　
                {
                    _IsAttackWait = false;　
                    aiState = EnemyAiState.Tracking;　
                }

                //強攻撃回数が4回の時
                if (AttackCount[1] >= 4 && AttackPattern == 3)　
                {
                    SpellCount = 0;　
                    _IsCount = true;　
                    _IsSummonSpell = false;　
                    aiState = EnemyAiState.StrongSpell; //遠距離魔法状態に遷移
                }

                //ワープ攻撃回数が3回の時
                if (AttackCount[2] >= 3 && AttackPattern == 4 && _IsThirdAttack == false)　
                {                 
                    SpellCount = 0;　
                    _IsCount = true;　
                    _IsSummonSpell = false;　
                    _IsDefaultWarp = true;　
                    _IsThirdAttack = true;　
                    aiState = EnemyAiState.Warp;　
                }
                
                //召喚エネミー管理
                if (_IsSummon == true)　
                {
                    _SummonTime += Time.deltaTime;　
                    if(_SummonTime >= 2 && AttackPattern == 2)　
                    {
                        Destroy(Smush[SummonCount - 1]);                       
                        SummonCount -= 1;　
                        _SummonTime = 0;　
                    }
                    if (_SummonTime >= 2 && AttackPattern == 4)　
                    {
                        Destroy(Stroll[SummonCount - 1]);   
                        SummonCount -= 1; 
                        _SummonTime = 0; 
                    }
                }

                return;

            //--------------------------------------追跡アニメーション処理-----------------------------------------
            case EnemyAiState.Tracking:
                _IsLook = true; 

                _IsTracking = true;　

                BossSprite.sprite = mono.move[(int)Spritetime[2]];　
                Spritetime[2] += Time.deltaTime * AnimeSpeed;

                if (Spritetime[2] >= MaxLeng[2])
                { 
                    Spritetime[2] = 0;
                }                

                if (_IsAttack == false)　
                {
                    if (_IsRetrcking == true)　//右を向いてる時
                    {                      
                        BossSprite.flipX = false; //反転処理 　
                    }
                    else if (_IsRetrcking == false) //左を向いてる時
                    {                       
                        BossSprite.flipX = true; //反転処理                       
                    }
                }
              
                return;
            //--------------------------------------弱攻撃-----------------------------------------
            case EnemyAiState.ATTACK:
                // Audio再生
                audios.bossSE = (AudioManager.BossSE)3;
                audios.AudioChanger("Boss");

                _IsAttack = true;
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

            //------------------------------------召喚処理----------------------------------
            case EnemyAiState.Summon:
                // Audio再生
                audios.bossSE = (AudioManager.BossSE)1;
                audios.AudioChanger("Boss");

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

            //-------------------------------------最初のアニメーションに戻る---------------------------------------
            case EnemyAiState.Back:
                // Audio再生
                audios.bossSE = (AudioManager.BossSE)6;
                audios.AudioChanger("Boss");

                _IsLook = false;
                _IsTracking = false;
                BossSprite.sprite = mono.Return[(int)Spritetime[3]];
                Spritetime[3] += Time.deltaTime * AnimeSpeed;
                if (Spritetime[3] >= MaxLeng[8])
                {
                    Spritetime[3] = MaxLeng[8] - 1;

                   if(charaData.life <= 0)
                   {
                        Destroy(gameObject);
                   }
                }
                break;

            //-------------------------------------ワープアニメーション処理-----------------------------------------
            case EnemyAiState.Warp:
                // Audio再生
                audios.bossSE = (AudioManager.BossSE)5;
                audios.AudioChanger("Boss");

                _IsTracking = false;

                _IsReset = false;
                gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                if (_IsRetrcking == true)　//右を向いている
                {
                    if (map.stageArray[EnemyPositionY, WarpPosX] == 4)
                    {
                        WarpPosX = map.PlayerPositionX - 6;
                    }
                    else
                    {
                        WarpPosX = map.PlayerPositionX + 6;
                    }
                }
                if (_IsRetrcking == false)　//左を向いている
                {                  
                    if (map.stageArray[EnemyPositionY, WarpPosX] == 4)
                    {
                        WarpPosX = map.PlayerPositionX + 6;
                    }
                    else
                    {
                        WarpPosX = map.PlayerPositionX - 6;
                    }
                }


                BossSprite.sprite = mono.Warp[(int)Spritetime[4]];
                Spritetime[4] += Time.deltaTime * AnimeSpeed;


                if (_IsDefaultWarp == true && Spritetime[4] >= MaxLeng[4])
                {
                    WarpPosX = DefaultPosXInt;
                    Spritetime[5] = 0;
                    this.transform.position = new Vector2(WarpPosX, transform.position.y);
                    aiState = EnemyAiState.WarpAttack;
                }

                if (_IsDefaultWarp == false && Spritetime[4] >= MaxLeng[4])
                {
                    Spritetime[5] = 0;
                    AttackCount[2] += 1;
                    aiState = EnemyAiState.WarpAttack;
                }
                break;

            //----------------------------------------ワープして攻撃------------------------------------------------
            case EnemyAiState.WarpAttack:
                Spritetime[4] = 0;
                _IsTracking = false;
                gameObject.layer = LayerMask.NameToLayer("Enemy");
                this.transform.position = new Vector2(WarpPosX, transform.position.y);

                if (_IsRetrcking == true)
                {                  
                    BossSprite.flipX = false; //反転処理 　左                  
                }
                else if (_IsRetrcking == false)
                {                  
                    BossSprite.flipX = true; //反転処理 右                   
                }

                BossSprite.sprite = mono.WarpAttack[(int)Spritetime[5]];
                Spritetime[5] += Time.deltaTime * AnimeSpeed;

                if (_IsDefaultWarp == true && Spritetime[5] >= MaxLeng[5] - 10 && AttackPattern == 2 ||
                    _IsDefaultWarp == true && Spritetime[5] >= MaxLeng[5] - 10 && AttackPattern == 4 && _IsThirdAttack == false)
                {
                    _IsReset = true;
                    _IsNext = false;
                    _IsSpell = false;
                    SpellCount = 0;
                    aiState = EnemyAiState.Summon;
                }

                if (_IsDefaultWarp == true && Spritetime[5] >= MaxLeng[5] - 10 && AttackPattern == 3 ||
                    _IsDefaultWarp == true && Spritetime[5] >= MaxLeng[5] - 10 && AttackPattern == 4 && _IsThirdAttack == true)
                {
                    _IsReset = true;
                    _IsSpell = false;
                    _IsSummonSpell = false;
                    SpellCount = 0;
                    aiState = EnemyAiState.StrongSpell;
                }

                if (Spritetime[5] >= MaxLeng[5] - 6 && Spritetime[5] <= MaxLeng[5] - 1 && _IsDefaultWarp == false)
                {
                    // Audio再生
                    audios.bossSE = (AudioManager.BossSE)6;
                    audios.AudioChanger("Boss");

                    _IsStrongHit = true;
                }

                if (Spritetime[5] >= MaxLeng[5])
                {
                    _IsReset = true;
                    aiState = EnemyAiState.IDLE;
                }             
                break;

            //-------------------------------------------魔法遠距離攻撃----------------------------------------------   
            case EnemyAiState.StrongSpell:
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
                    Instantiate(AttackEffect[1], new Vector2(-7, 8), Quaternion.identity); // 固定座標に魔法
                    SpellCount++;
                }

                if (_IsNext == true && SpellCount >=2 && SpellCount<= 3)
                {
                    _IsSpell = false;
                    Instantiate(AttackEffect[SpellCount], new Vector2(SpellPos[SpellCount].x, SpellPos[SpellCount].y), Quaternion.identity); // 固定座標に魔法                  
                    _IsNext = false;
                }
                if(SpellCount >= 4)
                {
                    _IsReset = true;

                    _IsNext = false;

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

            //-------------------------------------------強攻撃----------------------------------------------          
            case EnemyAiState.StorongAttack:
                // Audio再生
                audios.bossSE = (AudioManager.BossSE)4;
                audios.AudioChanger("Boss");

                _IsLook = false;
                _IsTracking = false;
                _IsReset = false;
                
                BossSprite.sprite = mono.StrongAttack[(int)Spritetime[8]];
                Spritetime[8] += Time.deltaTime * AnimeSpeed;

                if (Spritetime[8] >= MaxLeng[7] - 5 && Spritetime[8] <= MaxLeng[7] - 3)
                {
                    _IsStrongHit = true;
                }

                if (Spritetime[8] >= MaxLeng[7])                  
                {
                    _IsReset = true;
                    AttackCount[1] += 1;
                    aiState = EnemyAiState.IDLE;
                }

                if (AttackCount[1] >= 4)
                {
                    SpellCount = 0;
                    _IsDefaultWarp = true;
                    aiState = EnemyAiState.Warp;
                }
                break;

        }
    }

    private void Warp()
    {
        _IsReset = false;
        _IsWarp = true;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

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
                damage = 12;
                chara.CharaLifeCalculation(damage, 0, 0);
                print("hitW");
            }
        }

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

            if (GetAttackRange >= -AttackRange && GetAttackRange <= 0 && _IsStrongHit == false)
            {
                damage = 8;
                chara.CharaLifeCalculation(damage, 0, 0);
                print("hit");
            }

            if (GetAttackRange >= -(AttackRange + 3)&& GetAttackRange <= 0 && _IsStrongHit == true)
            {
                damage = 12;
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
        //-------------------2次元配列管理　・　攻撃判定-----------------------------
    private void MapMove()
    {        
        if (_IsRetrcking == false) //右向き
        {            
            //3つ右がプレイヤーだった場合攻撃
            if (AttackPattern == 1 && EnemyPositionX + AttackRange >= map.PlayerPositionX && EnemyPositionX + AttackRange <= map.PlayerPositionX ||
                AttackPattern == 2 && EnemyPositionX + AttackRange >= map.PlayerPositionX && EnemyPositionX + AttackRange <= map.PlayerPositionX && _IsSecondAttack == true)
            {                   
                aiState = EnemyAiState.ATTACK;
            }           
            
            //3つ右がプレイヤーだった場合攻撃
            if (AttackPattern == 3 && AttackCount[1] <= 3 &&  _IsAttackWait == false &&
                EnemyPositionX + AttackRange >= map.PlayerPositionX && EnemyPositionX + AttackRange <= map.PlayerPositionX)
            {
                aiState = EnemyAiState.StorongAttack;
            }            
        }

        if (_IsRetrcking == true)　//左向き
        {            
            //3つ左がプレイヤーだった場合攻撃
            if (AttackPattern == 1 && EnemyPositionX - AttackRange <= map.PlayerPositionX && EnemyPositionX - AttackRange >= map.PlayerPositionX ||
                AttackPattern == 2 && EnemyPositionX - AttackRange <= map.PlayerPositionX && EnemyPositionX - AttackRange >= map.PlayerPositionX && _IsSecondAttack == true)
            {
                aiState = EnemyAiState.ATTACK;
            }            
          
            //3右がプレイヤーだった場合攻撃
            if (AttackPattern == 3 && AttackCount[1] <= 3 && _IsAttackWait == false &&
                EnemyPositionX - AttackRange >= map.PlayerPositionX && EnemyPositionX - AttackRange <= map.PlayerPositionX)
            {
                aiState = EnemyAiState.StorongAttack;
            }
            

        }
    }
   
        //-------------------------追跡処理--------------------------------
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
