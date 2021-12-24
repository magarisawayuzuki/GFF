using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public enum BGM
    {
        Title,
        InGameNormal,// 通常時
        InGamePeerless,// 無双
        GameClear,
        GameOver
    }

    public enum PlayerSE
    {
        Idle,
        Walk,
        Jump,
        SordAttack,
        HummerAttack,
        Damage
    }

    public enum BossSE
    {
        Idle,
        Call,// 召喚
        Magic,
        WeakAttack,
        StrongAttack,
        Warp,
        Death
    }

    public enum UISE
    {
        Select,
        Decide,
        Back,
        ToPause
    }

    public BGM bgm = default;
    public PlayerSE playerSE = default;
    private AudioSource[] ad = { default, default, default, default };
    [SerializeField, Tooltip("BGM 0タイトル 1通常時 2無双時 3ゲームクリア 4ゲームオーバー")]
    private AudioClip[] bgmclip = default;
    [SerializeField, Tooltip("BGM 0タイトル 1通常時 2無双時 3ゲームクリア 4ゲームオーバー")]
    private AudioClip[] playerclip = default;
    [SerializeField, Tooltip("BGM 0タイトル 1通常時 2無双時 3ゲームクリア 4ゲームオーバー")]
    private AudioClip[] bossclip = default;
    [SerializeField, Tooltip("BGM 0タイトル 1通常時 2無双時 3ゲームクリア 4ゲームオーバー")]
    private AudioClip[] uiclip = default;

    private void Awake()
    {
        bgm = new BGM();
        for (int i = 0; i < ad.Length; i++)
        {
            ad[i] = GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        AudioChanged();
    }

    private void AudioChanged()
    {
        switch (bgm)
        {
            case BGM.Title:
                if (!ad[1].isPlaying)
                {
                    ad[1].PlayOneShot(bgmclip[0]);
                }
                break;
            case BGM.InGameNormal:
                if (!ad[1].isPlaying)
                {
                    ad[1].PlayOneShot(bgmclip[1]);
                }
                break;
            case BGM.InGamePeerless:
                if (!ad[1].isPlaying)
                {
                    ad[1].PlayOneShot(bgmclip[2]);
                }
                break;
            case BGM.GameClear:
                if (!ad[1].isPlaying)
                {
                    ad[1].PlayOneShot(bgmclip[3]);
                }
                break;
            case BGM.GameOver:
                if (!ad[1].isPlaying)
                {
                    ad[1].PlayOneShot(bgmclip[4]);
                }
                break;
        }

        switch (playerSE)
        {
            case PlayerSE.Idle:
                if (!ad[1].isPlaying)
                {
                    ad[1].PlayOneShot(bgmclip[0]);
                }
                break;
            case PlayerSE.Walk:
                if (!ad[1].isPlaying)
                {
                    ad[1].PlayOneShot(bgmclip[1]);
                }
                break;
            case PlayerSE.Jump:
                if (!ad[1].isPlaying)
                {
                    ad[1].PlayOneShot(bgmclip[2]);
                }
                break;
            case PlayerSE.SordAttack:
                if (!ad[1].isPlaying)
                {
                    ad[1].PlayOneShot(bgmclip[3]);
                }
                break;
            case PlayerSE.HummerAttack:
                if (!ad[1].isPlaying)
                {
                    ad[1].PlayOneShot(bgmclip[4]);
                }
                break;
            case PlayerSE.Damage:
                if (!ad[1].isPlaying)
                {
                    ad[1].PlayOneShot(bgmclip[4]);
                }
                break;
        }
    }
}
