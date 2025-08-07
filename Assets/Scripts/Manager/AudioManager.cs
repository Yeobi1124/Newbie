using BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public static BGMType PlayingBGM = BGMType.None;
    
    public enum BGMType
    {
        None,
        Title,
        InGame,
        Elite,
        Boss,
        Lose
    }

    public enum SEType
    {
        PlayerShoot,
        PlayerHit,
        PlayerMissile,
        PlayerMissileHit,
        PlayerLaser,
        PlayerDie,
        PlayerEnergyShard,
        PlayerParry,
        PlayerParrySuccess,
        PlayerHeal,

        DroneShootA,
        DroneShootB,
        DroneShootC,
        DroneHit,
        DroneMissileHit,
        DroneCharge,
        DroneChargeShoot,
        DroneDashReady,
        DroneDie,

        BossShootA,
        BossShootB,
        BossShootC,
        BossHit,
        BossMissileHit,
        BossDashReady,
        BossLaser,
        BossMissile,
        BossMissileBoost,
        BossDie,

        UIButton,
        UIClearStamp
    }

    private AudioSource Player;

    [Header("BGM")]
    public AudioClip BGMTitle;
    public AudioClip BGMInGame;
    public AudioClip BGMElite;
    public AudioClip BGMBoss;
    public AudioClip BGMLose;

    [Header("Player SE")]
    public AudioClip PlayerShoot;
    public AudioClip PlayerHit;
    public AudioClip PlayerMissile;
    public AudioClip PlayerMissileHit;
    public AudioClip PlayerLaser;
    public AudioClip PlayerDie;
    public AudioClip PlayerEnergyShard;
    public AudioClip PlayerParry;
    public AudioClip PlayerParrySuccess;
    public AudioClip PlayerHeal;

    [Header("Drone SE")]
    public AudioClip DroneShootA;
    public AudioClip DroneShootB;
    public AudioClip DroneShootC;
    public AudioClip DroneHit;
    public AudioClip DroneMissileHit;
    public AudioClip DroneCharge;
    public AudioClip DroneChargeShoot;
    public AudioClip DroneDashReady;
    public AudioClip DroneDie;

    [Header("Boss SE")]
    public AudioClip BossShootA;
    public AudioClip BossShootB;
    public AudioClip BossShootC;
    public AudioClip BossHit;
    public AudioClip BossMissileHit;
    public AudioClip BossDashReady;
    public AudioClip BossLaser;
    public AudioClip BossMissile;
    public AudioClip BossMissileBoost;
    public AudioClip BossDie;

    [Header("UI SE")]
    public AudioClip UIButton;
    public AudioClip UIClearStamp;


    private void Awake()
    {
        if(null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        Player = GetComponent<AudioSource>();
        Player.loop = true;
    }

    void Update()
    {
        Player.volume = UIManager.Instance.BGM_Volume.value;
    }

    public void PlayBGM(BGMType type)
    {
        if(PlayingBGM == type)
        {
            Debug.Log("Error: BGM " + type + "is Already Playing!!!");
        }
        PlayingBGM = type;
        switch (type)
        {
            case BGMType.Title:
                Player.clip = BGMTitle;
                break;
            case BGMType.InGame:
                Player.clip = BGMInGame;
                break;
            case BGMType.Elite:
                Player.clip = BGMElite;
                break;
            case BGMType.Boss:
                Player.clip = BGMBoss;
                break;
            case BGMType.Lose:
                Player.clip = BGMLose;
                break;
            default:
                break;
        }
        Player.Play();
    }

    public void StopBGM()
    {
        Player.Stop();
        PlayingBGM = BGMType.None;
    }

    public void PlaySE(SEType type)
    {
        float volume = UIManager.Instance.SE_Volume.value;
        switch (type)
        {
            case SEType.PlayerShoot:
                Player.PlayOneShot(PlayerShoot, volume);
                break;
            case SEType.PlayerHit:
                Player.PlayOneShot(PlayerHit, volume);
                break;
            case SEType.PlayerMissile:
                Player.PlayOneShot(PlayerMissile, volume);
                break;
            case SEType.PlayerMissileHit:
                Player.PlayOneShot(PlayerMissileHit, volume);
                break;
            case SEType.PlayerLaser:
                Player.PlayOneShot(PlayerLaser, volume);
                break;
            case SEType.PlayerDie:
                Player.PlayOneShot(PlayerDie, volume);
                break;
            case SEType.PlayerEnergyShard:
                Player.PlayOneShot(PlayerEnergyShard, volume);
                break;
            case SEType.PlayerParry:
                Player.PlayOneShot(PlayerParry, volume);
                break;
            case SEType.PlayerParrySuccess:
                Player.PlayOneShot(PlayerParrySuccess, volume);
                break;
            case SEType.PlayerHeal:
                Player.PlayOneShot(PlayerHeal, volume);
                break;

            case SEType.DroneShootA:
                Player.PlayOneShot(DroneShootA, volume);
                break;
            case SEType.DroneShootB:
                Player.PlayOneShot(DroneShootB, volume);
                break;
            case SEType.DroneShootC:
                Player.PlayOneShot(DroneShootC, volume);
                break;
            case SEType.DroneHit:
                Player.PlayOneShot(DroneHit, volume);
                break;
            case SEType.DroneMissileHit:
                Player.PlayOneShot(DroneMissileHit, volume);
                break;
            case SEType.DroneCharge:
                Player.PlayOneShot(DroneCharge, volume);
                break;
            case SEType.DroneChargeShoot:
                Player.PlayOneShot(DroneChargeShoot, volume);
                break;
            case SEType.DroneDashReady:
                Player.PlayOneShot(DroneDashReady, volume);
                break;
            case SEType.DroneDie:
                Player.PlayOneShot(DroneDie, volume);
                break;

            case SEType.BossShootA:
                Player.PlayOneShot(BossShootA, volume);
                break;
            case SEType.BossShootB:
                Player.PlayOneShot(BossShootB, volume);
                break;
            case SEType.BossShootC:
                Player.PlayOneShot(BossShootC, volume);
                break;
            case SEType.BossHit:
                Player.PlayOneShot(BossHit, volume);
                break;
            case SEType.BossMissileHit:
                Player.PlayOneShot(BossMissileHit, volume);
                break;
            case SEType.BossDashReady:
                Player.PlayOneShot(BossDashReady, volume);
                break;
            case SEType.BossLaser:
                Player.PlayOneShot(BossLaser, volume);
                break;
            case SEType.BossMissile:
                Player.PlayOneShot(BossMissile, volume);
                break;
            case SEType.BossMissileBoost:
                Player.PlayOneShot(BossMissileBoost, volume);
                break;
            case SEType.BossDie:
                Player.PlayOneShot(BossDie, volume);
                break;

            case SEType.UIButton:
                Player.PlayOneShot(UIButton, volume);
                break;
            case SEType.UIClearStamp:
                Player.PlayOneShot(UIClearStamp, volume);
                break;
            default:
                break;
        }
    }
}
