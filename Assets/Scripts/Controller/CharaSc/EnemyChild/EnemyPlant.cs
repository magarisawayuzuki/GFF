using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 近距離攻撃のAI
/// </summary>
public class EnemyPlant : EnemyController
{    
    PlayerController playerController;
    private float damage = 5;
    private float JumpTime;    
    protected override void Awake()
    {
        base.Awake();
        //------------二次元配列のスクリプト取得------------------
        GameObject en = GameObject.FindGameObjectWithTag("Map");
        map = en.GetComponent<Maping>();

        //SpriteRendererコンポーネントを取得
        EnemySprite = GetComponent<SpriteRenderer>();

        // PLAYERオブジェクトを取得
        player = GameObject.FindGameObjectWithTag("Player");
        //最初の座標
        DefaultPos = transform.position;

        DefaultPosIntX = Mathf.FloorToInt(DefaultPos.x);
        DefaultPosIntY = Mathf.FloorToInt(DefaultPos.y);
        //spriteの最大数を取得maxLeng(0リスポーン　1移動　２待機　3攻撃　4攻撃を当てられた　5死亡
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

    private void Start()
    {
        // PLAYERオブジェクトを取得
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }
    //AI動作を記述
    public override PlayerInput InputMethod()
    {
        _ = base.InputMethod();
        if (_IsJump == true)
        {           
            input._isJump = true;
            JumpTime += Time.deltaTime;
            if(JumpTime >= 0.6)
            {
                JumpTime = 0;
                input._isJump = false;
                _IsJump = false;
            }
        }

        return input;
    }

    protected override void Update()
    {
        base.Update();

        MapMove();　//二次元配列・座標管理

        EnemyPos();　//座標更新・整数に変更

        Bool();

        AnimeMotion(); //switch文

        EnemyTracking(); //追跡処理
    }

    public override void CharaLifeCalculation(float damage, int knockBack, int weapon)
    {
        //倍率を代入
        damageScaleSword = data.swordScale;
        damageScaleHammer = data.hammerScale;

        if (_IsAttack == false)
        {
            _IsTakeHit = true;
            anime = 6;
        }
        base.CharaLifeCalculation(damage, knockBack, weapon);
    }


    //===========動作の上書き・追記が必要なら変更する===================

    public override void Move()
    {
        base.Move();
    }

    public override void Attack()
    {
        base.Attack();
    }

    //------------------座標更新・整数に変更---------------------------
    private void EnemyPos()
    {
        //常に座標を取得
        pos = transform.position;

        //座標を整数に変換　XY
        EnemyPositionX = Mathf.FloorToInt(pos.x);
        EnemyPositionY = Mathf.FloorToInt(pos.y);

        if (EnemyPositionX >= DefaultPosIntX + 1)
        {
            //ジャンプ時の配列更新
            if (map.stageArray[EnemyPositionY, EnemyPositionX + 1] == 3)
            {
                map.stageArray[EnemyPositionY, EnemyPositionX] = 0;
            }
            else if (map.stageArray[EnemyPositionY, EnemyPositionX + 2] == 3)
            {
                map.stageArray[EnemyPositionY, EnemyPositionX - 1] = 0;
            }
            //ジャンプ以外の配列更新
            else
            {
                map.stageArray[EnemyPositionY, EnemyPositionX] = 6;
                map.stageArray[EnemyPositionY, EnemyPositionX - 1] = 0;

            }
            DefaultPosIntX = EnemyPositionX;
        }

        if (EnemyPositionX <= DefaultPosIntX - 1)
        {
            //ジャンプ時の配列更新
            if (map.stageArray[EnemyPositionY, EnemyPositionX - 1] == 3)
            {
                map.stageArray[EnemyPositionY, EnemyPositionX] = 0;
            }
            else if (map.stageArray[EnemyPositionY, EnemyPositionX - 2] == 3)
            {
                map.stageArray[EnemyPositionY, EnemyPositionX + 1] = 0;
            }
            //ジャンプ以外の配列更新
            else
            {
                map.stageArray[EnemyPositionY, EnemyPositionX] = 6;
                map.stageArray[EnemyPositionY, EnemyPositionX + 1] = 0;

            }
            DefaultPosIntX = EnemyPositionX;
        }        
    }

    private void OnBecameVisible()　//カメラ内処理
    {
        for (int i = 0; i < Spritetime.GetLength(0); i++)
        {
            Spritetime[i] = 0;
        }
        if (_isDeath == false)
        {
            anime = 1;
        }

    }

    private void OnBecameInvisible() //カメラ外処理
    {
        for (int i = 0; i < Spritetime.GetLength(0); i++)
        {
            Spritetime[i] = 0;
        }

        _InEnemy = false;
        _IsTrackingWait = false;
        if (_isDeath == false)
        {
            anime = 3;
        }
    }

    //--------------------------switch文-------------------------------
    private void AnimeMotion()
    {       
        // MaxLeng[](0リスポーン 1移動　２待機 3攻撃 4攻撃を当てられた 5死亡
        // Spritetime[](0リスポーン　1移動　2最初のspritに（死亡）　3待機　4攻撃　5攻撃を当てられた

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
                _IsTakeHit = false;
                _IsTracking = true;
                Spritetime[4] = 0;

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
                    EnemySprite.flipX = true; //反転処理 　左
                }
                else if (_Retrcking == false)
                {
                    num = 1;
                    EnemySprite.flipX = false; //反転処理 右
                }

                if (_IsTakeHit == true)
                {
                    anime = 6;
                }
                return;

            case 3:　//最初のアニメーションに戻る
                _IsTracking = false;
                EnemySprite.sprite = Anime.Death[(int)Spritetime[2]];
                Spritetime[2] += Time.deltaTime * data.AnimeSpeed[0];
                if (_isDeath == true)
                {
                    gekitui.gameObject.SetActive(true);
                    gekitui.transform.parent = null;
                }

                if (Spritetime[2] >= MaxLeng[5])
                {
                    if (_isDeath == true)
                    {
                        Instantiate(kakera, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                        map.stageArray[EnemyPositionY, EnemyPositionX] = 0;
                        gameObject.SetActive(false);
                    }
                    else
                    {
                        Spritetime[2] = MaxLeng[5] - 1;
                        Spritetime[3] = 0;
                    }
                }
                break;

            case 4://---------------------------待機-----------------------------
                _IsTracking = false;

                EnemySprite.sprite = Anime.Idel[(int)Spritetime[3]];
                Spritetime[3] += Time.deltaTime * data.AnimeSpeed[2];

                //待機状態中にplayerが範囲に入った場合追跡再開
                if (_isDeath == false && _IsWait == true)
                {
                    if (EnemyPositionX >= map.PlayerPositionX && EnemyPositionX + AttackRange + 3 <= map.PlayerPositionX && EnemySprite.flipX == false)
                    {
                        _IsTracking = true;
                        _IsLook = true;
                        _IsWait = false;
                        anime = 2;
                    }
                    if (EnemyPositionX > map.PlayerPositionX && EnemyPositionX - (AttackRange + 3) <= map.PlayerPositionX && EnemySprite.flipX == true)
                    {
                        _IsTracking = true;
                        _IsLook = true;
                        _IsWait = false;
                        anime = 2;
                    }
                }

                if(_IsTakeHit == true)
                {
                    anime = 6;
                }
                if (Spritetime[3] >= MaxLeng[2])
                {
                    Spritetime[3] = 0;

                    //カメラ内にいるかつ待機中ではない時
                    if (_InEnemy == true && _IsTrackingWait == false)
                    {
                        _IsAttack = true;
                    }

                }

                if (_Retrcking == true)
                {
                    num = -1;
                    EnemySprite.flipX = true; //反転処理 　左
                }
                else if (_Retrcking == false)
                {
                    num = 1;
                    EnemySprite.flipX = false; //反転処理 右
                }              
                return;

            case 5: // ---------------------------攻撃--------------------------------- 
                _IsTracking = false;
                EnemySprite.sprite = Anime.Attack[(int)Spritetime[4]];
                Spritetime[4] += Time.deltaTime * data.AnimeSpeed[3];
                _NowAttack = true;
                //攻撃しているspriteの時
                if (Spritetime[4] >= MaxLeng[3] - 1)
                {
                
                    //向いている方向によって敵とプレイヤーの距離計算
                    if (_Retrcking == false)
                    {
                        GetAttackRange = EnemyPositionX - map.PlayerPositionX;
                    }

                    if (_Retrcking == true)
                    {
                        GetAttackRange = map.PlayerPositionX - EnemyPositionX;
                    }
                    //計算した距離が-2から0だった時攻撃を当てた
                    if (GetAttackRange >= -AttackRange && GetAttackRange <= 0)
                    {
                        playerController.CharaLifeCalculation(damage, 0, 0);
                        print("hit");
                    }
                }
                
                if (Spritetime[4] >= MaxLeng[3])
                {
                    Spritetime[4] = 0;
                    _IsAttack = false;
                    _NowAttack = false;
                }
                break;

            case 6: //----------------------------攻撃をくらった-------------------------------- 
                _IsTracking = false;
                EnemySprite.sprite = Anime.TakeHit[(int)Spritetime[5]];
                Spritetime[5] += Time.deltaTime * data.AnimeSpeed[4];

                if (Spritetime[5] >= MaxLeng[4] && _isDeath == false)
                {
                    print("Takehit");
                    Spritetime[5] = 0;
                    _IsTakeHit = false;
                    anime = 2;
                }
                break;
        }
    }


    //-----------------二次元配列によって行動変化----------------------
    private void MapMove()
    {
        if (num == 1) //右向き
        {
            //2つ右がプレイヤーだった場合攻撃
            if (EnemyPositionX + AttackRange >= map.PlayerPositionX && EnemyPositionX + AttackRange <= map.PlayerPositionX && _IsLook == true 
                && _isDeath == false)
            {
                if (_IsAttack == true && _IsTakeHit == false)
                {
                    anime = 5;
                }
                else if(_IsAttack == false && _IsTakeHit == false)　//攻撃待機
                {
                    anime = 4;
                }
            }
            //攻撃範囲外の場合追跡
            else if (_InEnemy == true && _IsTrackingWait == false && _IsLook == true && _isDeath == false && _IsTakeHit == false && _NowAttack == false)
            {
                _IsTracking = true;
                anime = 2;
            }

        }
        if (num == -1) //左向き
        {
            //2つ左がプレイヤーだった場合攻撃
            if (EnemyPositionX - AttackRange >= map.PlayerPositionX && EnemyPositionX - AttackRange <= map.PlayerPositionX && _IsLook == true
                && _isDeath == false)
            {
                if (_IsAttack == true && _IsTakeHit == false)
                {
                    anime = 5;
                }
                else if (_IsAttack == false && _IsTakeHit == false)　//攻撃待機
                {                
                    anime = 4;
                }
            }
            //攻撃範囲外の場合追跡
            else if (_InEnemy == true && _IsTrackingWait == false && _IsLook == true && _isDeath == false && _IsTakeHit == false && _NowAttack == false)
            {
                _IsTracking = true;
                anime = 2;
            }
        }

    }

    //--------------------------bool処理-------------------------------
    private void Bool()
    {       
        //----------------元の座標に戻っている---------------
        if (_IsReturn == true)
        {
            //追跡をoff
            _IsLook = false;
            _IsTracking = false;
            transform.Translate(transform.right * Time.deltaTime * data.ReturnSpeed * num);

            if (EnemyPositionX + AttackRange >= map.PlayerPositionX && EnemyPositionX + AttackRange <= map.PlayerPositionX && _Retrcking == true && _isDeath == false ||
                EnemyPositionX >= map.PlayerPositionX && EnemyPositionX -(AttackRange + 3)<= map.PlayerPositionX && _Retrcking == false && _isDeath == false)
            {
                _IsTracking = true;
                _IsLook = true;
                _IsReturn = false;
                anime = 2;
            }
        }

        //---------------元の座標についたとき最初のspriteに戻る---------------------
        if (_IsTracking == false && _IsReturn == true && _isDeath == false)
        {
            if (pos.x <= DefaultPos.x && _Retrcking == true && _isDeath == false)　//左に向いている
            {
                _IsReturn = false;  //元に戻った
                _Retrcking = false; //反転
                _IsWait = true;     //待機状態に
                Spritetime[2] = 0;
                anime = 4;
            }
            if (pos.x >= DefaultPos.x && _Retrcking == false && _isDeath == false)　//右に向いている
            {
                _IsReturn = false; //元に戻った
                _Retrcking = true; //反転
                _IsWait = true;    //待機状態に
                Spritetime[2] = 0;
                anime = 4;
            }
        }       
    }

    //--------------------------追跡処理-------------------------------
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
}
