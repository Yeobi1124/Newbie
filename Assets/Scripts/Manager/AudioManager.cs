using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public static BGMType PlayingBGM = BGMType.None;
    
    public enum BGMType
    {
        None = 0,
        Title = 1,
        InGame = 2,
        Boss = 3,
        Lose = 4,
        Win = 5
    }

    public enum SEType
    {

    }

    private AudioSource Player;

    [Header("AudioClips")]
    public AudioClip BGMTitle;
    public AudioClip BGMInGame;
    public AudioClip BGMBoss;
    public AudioClip BGMLose;
    public AudioClip BGMWin;
    public AudioClip[] SE;
    public AudioClip PlayerShoot;
    public AudioClip EnergyShard;
    public AudioClip ParryUse;
    public AudioClip ParrySuccess;


    private void Awake()
    {
        if(null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        Player = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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
            case BGMType.Boss:
                Player.clip = BGMBoss;
                break;
            case BGMType.Lose:
                Player.clip = BGMLose;
                break;
            case BGMType.Win:
                Player.clip = BGMWin;
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


}
