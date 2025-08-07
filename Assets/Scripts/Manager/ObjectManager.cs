using System;
using System.CodeDom.Compiler;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;

    public GameObject BulletPlayerPrefab;
    public GameObject BulletEnemyPrefab;
    public GameObject DroneShooterPrefab;
    public GameObject DroneChargerPrefab;

    GameObject[] BulletPlayer;
    GameObject[] BulletEnemy;
    GameObject[] DroneShooter;
    GameObject[] DroneCharger;

    GameObject[] TargetPool;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else Destroy(gameObject);

        BulletPlayer = new GameObject[100];
        BulletEnemy = new GameObject[100];
        DroneShooter = new GameObject[20];
        DroneCharger = new GameObject[20];

        Generate();
    }

    private void OnDestroy()
    {
        if(instance == this)
            instance = null;
    }

    void Generate()
    {
        for (int index = 0; index < BulletPlayer.Length; index++)
        {
            BulletPlayer[index] = Instantiate(BulletPlayerPrefab);
            BulletPlayer[index].SetActive(false);
        }
        for (int index = 0; index < BulletEnemy.Length; index++)
        {
            BulletEnemy[index] = Instantiate(BulletEnemyPrefab);
            BulletEnemy[index].SetActive(false);
        }
        for (int index = 0; index < DroneShooter.Length; index++)
        {
            DroneShooter[index] = Instantiate(DroneShooterPrefab);
            DroneShooter[index].SetActive(false);
        }
        for (int index = 0; index < DroneCharger.Length; index++)
        {
            DroneCharger[index] = Instantiate(DroneChargerPrefab);
            DroneCharger[index].SetActive(false);
        }
    }

    public GameObject PullObject(string type)
    {
        switch (type)
        {
            case "BulletPlayer":
                TargetPool = BulletPlayer;
                break;
            case "BulletEnemy":
                TargetPool = BulletEnemy;
                break;
            case "DroneShooter":
                TargetPool = DroneShooter;
                break;
            case "DroneCharger":
                TargetPool = DroneCharger;
                break;
            default:
                break;
        }

        for (int index = 0; index < TargetPool.Length; index++)
        {
            if (!TargetPool[index].activeSelf)
            {
                TargetPool[index].SetActive(true);
                return TargetPool[index];
            }
        }

        Debug.Log(type + " Ǯ�� �����ִ� ������Ʈ�� �����ϴ�!");
        return null;
    }
}