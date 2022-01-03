using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// 0:タイトル
    /// 1:通常時環境音
    /// 2:ゲームクリア
    /// 3:ゲームオーバー
    /// </summary>
    public enum BGM
    {
        Title,
        InGameNormal,
        GameClear,
        GameOver
    }

    /// <summary>
    /// 0:待機
    /// 1:歩き
    /// 2:着地
    /// 3:剣　弱攻撃
    /// 4:剣　強攻撃
    /// 5:槌　弱攻撃
    /// 6:槌　強攻撃
    /// 7:被弾
    /// 8:死亡
    /// </summary>
    public enum PlayerSE
    {
        Idle,
        Walk,
        Landing,
        SordWeakAttack,
        SordStrongAttack,
        HummerWeakAttack,
        HummerStrongAttack,
        Damage,
        Death
    }

    /// <summary>
    /// 0:待機
    /// 1:召喚
    /// 2:魔法
    /// 3:弱攻撃
    /// 4:強攻撃
    /// 5:ワープ
    /// 6:死亡
    /// </summary>
    public enum BossSE
    {
        Idle,
        Call,
        Magic,
        WeakAttack,
        StrongAttack,
        Warp,
        Death
    }

    /// <summary>
    /// 0:選択
    /// 1:決定
    /// 2:戻る
    /// 3:ポーズへ
    /// 4:シーン遷移
    /// </summary>
    public enum UISE
    {
        Select,
        Decide,
        Back,
        ToPause,
        Transition
    }

    public BGM bgm = default;
    public PlayerSE playerSE = default;
    public BossSE bossSE = default;
    public UISE uiSE = default;
    private AudioSource[] ad = { default, default, default, default };
    [SerializeField, Tooltip("BGM 0タイトル 1通常時 2ゲームクリア 3ゲームオーバー")]
    private AudioClip[] bgmclip = default;
    [SerializeField, Tooltip("PlayerSE 0待機 1歩く 2ジャンプ 3剣弱攻撃 4剣強攻撃 5槌弱攻撃 6槌強攻撃 7ダメージ")]
    private AudioClip[] playerclip = default;
    [SerializeField, Tooltip("BossSE 0待機 1召喚 2魔法 3弱攻撃 4強攻撃 5ワープ 6死亡")]
    private AudioClip[] bossclip = default;
    [SerializeField, Tooltip("UISE 0選択 1決定 2戻る 3ポーズへ 4シーン遷移")]
    private AudioClip[] uiclip = default;
    [SerializeField]
    private AudioMixerGroup[] mixer = default;

    private void Awake()
    {
        bgm = new BGM();
        playerSE = new PlayerSE();
        bossSE = new BossSE();
        uiSE = new UISE();

        ad[0] = gameObject.AddComponent<AudioSource>();
        ad[0].loop = true;
        ad[0].playOnAwake = false;
        ad[0].outputAudioMixerGroup = mixer[0];

        for (int i = 1; i < ad.Length; i++)
        {
            ad[i] = gameObject.AddComponent<AudioSource>();
            ad[i].loop = false;
            ad[i].playOnAwake = false;
            ad[i].outputAudioMixerGroup = mixer[1];
        }
    }

    private void Update()
    {
        Debug.Log("None : "+InGameToPauseUI_2._isPause);
        Debug.Log("Static : "+InGameToPauseUI_2._isStaticPause);
    }

    public void AudioChanger(string nowState)
    {
        switch (nowState)
        {
            case "BGM":
                AudioChangedBGM();
                break;
            case "Player":
                if (!ad[1].isPlaying)
                {
                    AudioChangedPlayerSE();
                }
                break;
            case "Boss":
                if (!ad[2].isPlaying)
                {
                    AudioChangedBossSE();
                }
                break;
            case "UI":
                AudioChangedUISE();
                break;
            default:
                break;
        }
    }

    private void AudioChangedBGM()
    {
        switch (bgm)
        {
            case BGM.Title:
                if (!ad[0].isPlaying || ad[0].clip != bgmclip[0])
                {
                    ad[0].clip = bgmclip[0];
                    ad[0].Play();
                }
                break;
            case BGM.InGameNormal:
                if (!ad[0].isPlaying || ad[0].clip != bgmclip[1])
                {
                    ad[0].clip = bgmclip[1];
                    ad[0].Play();
                }
                break;
            case BGM.GameClear:
                if (!ad[0].isPlaying || ad[0].clip != bgmclip[2])
                {
                    ad[0].clip = bgmclip[2];
                    ad[0].Play();
                }
                break;
            case BGM.GameOver:
                if (!ad[0].isPlaying || ad[0].clip != bgmclip[3])
                {
                    ad[0].clip = bgmclip[3];
                    ad[0].Play();
                }
                break;
        }
    }

    private void AudioChangedPlayerSE()
    {

        switch (playerSE)
        {
            case PlayerSE.Idle:
                ad[1].PlayOneShot(playerclip[0]);
                break;
            case PlayerSE.Walk:
                ad[1].PlayOneShot(playerclip[1]);
                break;
            case PlayerSE.Landing:
                ad[1].PlayOneShot(playerclip[2]);
                break;
            case PlayerSE.SordWeakAttack:
                ad[1].PlayOneShot(playerclip[3]);
                break;
            case PlayerSE.SordStrongAttack:
                ad[1].PlayOneShot(playerclip[4]);
                break;
            case PlayerSE.HummerWeakAttack:
                ad[1].PlayOneShot(playerclip[5]);
                break;
            case PlayerSE.HummerStrongAttack:
                ad[1].PlayOneShot(playerclip[6]);
                break;
            case PlayerSE.Damage:
                ad[1].PlayOneShot(playerclip[7]);
                break;
            case PlayerSE.Death:
                ad[1].PlayOneShot(playerclip[8]);
                break;
        }
    }

    private void AudioChangedBossSE()
    {
        switch (bossSE)
        {
            case BossSE.Idle:
                ad[2].PlayOneShot(bossclip[0]);
                break;
            case BossSE.Call:
                ad[2].PlayOneShot(bossclip[1]);
                break;
            case BossSE.Magic:
                ad[2].PlayOneShot(bossclip[2]);
                break;
            case BossSE.WeakAttack:
                ad[2].PlayOneShot(bossclip[3]);
                break;
            case BossSE.StrongAttack:
                ad[2].PlayOneShot(bossclip[4]);
                break;
            case BossSE.Warp:
                ad[2].PlayOneShot(bossclip[5]);
                break;
            case BossSE.Death:
                ad[2].PlayOneShot(bossclip[6]);
                break;
        }
    }

    private void AudioChangedUISE()
    {
        switch (uiSE)
        {
            case UISE.Select:
                ad[3].PlayOneShot(uiclip[0]);
                break;
            case UISE.Decide:
                ad[3].PlayOneShot(uiclip[1]);
                break;
            case UISE.Back:
                ad[3].PlayOneShot(uiclip[2]);
                break;
            case UISE.ToPause:
                ad[3].PlayOneShot(uiclip[3]);
                break;
            case UISE.Transition:
                ad[3].PlayOneShot(uiclip[4]);
                break;
        }
    }
}
