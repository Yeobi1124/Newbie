using System;
using System.CodeDom.Compiler;
using UnityEngine;

public class DroneObjectManager : MonoBehaviour
{
    private static DroneObjectManager instance;

    public GameObject BulletEnemyAPrefab;
    public GameObject BulletEnemyBPrefab;
    public GameObject BulletEnemyCPrefab;
    public GameObject DroneShooterAPrefab;
    public GameObject DroneShooterBPrefeb;
    public GameObject DroneChargerAPrefeb;
    public GameObject ElliteAPrefeb;
    public GameObject ShardPrefeb;

    public GameObject[] BulletEnemyA;
    public GameObject[] BulletEnemyB;
    public GameObject[] BulletEnemyC;
    public GameObject[] DroneShooterA;
    public GameObject[] DroneShooterB;
    public GameObject[] DroneChargerA;
    public GameObject[] ElliteA;
    
    public GameObject[] shards;

    GameObject[] TargetPool;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else Destroy(gameObject);

        BulletEnemyA = new GameObject[100];
        BulletEnemyB = new GameObject[100];
        BulletEnemyC = new GameObject[100];
        DroneShooterA = new GameObject[20];
        DroneShooterB = new GameObject[20];
        DroneChargerA = new GameObject[20];
        ElliteA = new GameObject[20];
       
        shards = new GameObject[20];

        

        Generate();
    }

    private void OnDestroy()
    {
        if(instance == this)
            instance = null;
    }

    void Generate()
    {
        for (int index = 0; index < BulletEnemyA.Length; index++)
        {
            BulletEnemyA[index] = Instantiate(BulletEnemyAPrefab);
            BulletEnemyA[index].SetActive(false);
        }
        for (int index = 0; index < BulletEnemyB.Length; index++)
        {
            BulletEnemyB[index] = Instantiate(BulletEnemyBPrefab);
            BulletEnemyB[index].SetActive(false);
        }

        for (int index = 0; index < BulletEnemyC.Length; index++)
        {
            BulletEnemyC[index] = Instantiate(BulletEnemyCPrefab);
            BulletEnemyC[index].SetActive(false);
        }
        for (int index = 0; index < DroneShooterA.Length; index++)
        {
            DroneShooterA[index] = Instantiate(DroneShooterAPrefab);
            DroneShooterA[index].SetActive(false);
        }

        for (int index = 0; index < DroneShooterB.Length; index++)
        {
            DroneShooterB[index] = Instantiate(DroneShooterBPrefeb);
            DroneShooterB[index].SetActive(false);
        }

        for (int index = 0; index < DroneChargerA.Length; index++)
        {
            DroneChargerA[index] = Instantiate(DroneChargerAPrefeb);
            DroneChargerA[index].SetActive(false);
        }
        for (int index = 0; index < ElliteA.Length; index++)
        {
            ElliteA[index] = Instantiate(ElliteAPrefeb);
            ElliteA[index].SetActive(false);
        }


        for (int index = 0; index < shards.Length; index++)
        {
            shards[index] = Instantiate(ShardPrefeb);
            shards[index].SetActive(false);
        }


    }

    public GameObject PullObject(string type)
    {
        switch (type)
        {
            case "BulletEnemy":
                TargetPool = BulletEnemyA;
                break;
            case "BulletEnemyBig":
                TargetPool = BulletEnemyB;
                break; 
            case "BulletChase":
                TargetPool = BulletEnemyC;
                break;
                
            case "DroneShooterA":
                TargetPool = DroneShooterA;
                break;
            case "DroneShooterB":
                TargetPool = DroneShooterB;
                break;

            case "DroneChargerA":
                TargetPool = DroneChargerA;
                break;
            case "Shard":
                TargetPool = shards;
                break;

            case "ElliteA":
                TargetPool = ElliteA;
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

    public static DroneObjectManager Instance
    {
        get { return instance; }
    }
}
