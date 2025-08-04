using System.CodeDom.Compiler;
using UnityEngine;

public class DroneObjectManager : MonoBehaviour
{
    private static DroneObjectManager instance;

    public GameObject BulletEnemyAPrefab;
    public GameObject BulletEnemyBPrefab;
    public GameObject DroneShooterPrefab;
    public GameObject DroneChargerPrefab;
    public GameObject DroneShooterBPrefeb;
    public GameObject DroneShooterCPrefeb;
    public GameObject DroneShooterDPrefeb;

    public GameObject[] BulletEnemyA;
    public GameObject[] BulletEnemyB;
    public GameObject[] DroneShooter;
    public GameObject[] DroneCharger;
    public GameObject[] DroneShooterB;
    public GameObject[] DroneShooterC;
    public GameObject[] DroneShooterD;

    GameObject[] TargetPool;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        BulletEnemyA = new GameObject[100];
        BulletEnemyB = new GameObject[100];
        DroneShooter = new GameObject[20];
        DroneCharger = new GameObject[20];
        DroneShooterB = new GameObject[20];
        DroneShooterC = new GameObject[20];
        DroneShooterD = new GameObject[20];

        Generate();
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

        for (int index = 0; index < DroneShooterB.Length; index++)
        {
            DroneShooterB[index] = Instantiate(DroneShooterBPrefeb);
            DroneShooterB[index].SetActive(false);
        }

        for (int index = 0; index < DroneShooterC.Length; index++)
        {
            DroneShooterC[index] = Instantiate(DroneShooterCPrefeb);
            DroneShooterC[index].SetActive(false);
        }

        for (int index = 0; index < DroneShooterD.Length; index++)
        {
            DroneShooterD[index] = Instantiate(DroneShooterDPrefeb);
            DroneShooterD[index].SetActive(false);
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
                
            case "DroneShooter":
                TargetPool = DroneShooter;
                break;
            case "DroneCharger":
                TargetPool = DroneCharger;
                break;

            case "DroneShooterB":
                TargetPool = DroneShooterB;
                break;
            case "DroneShooterC":
                TargetPool = DroneShooterC;
                break;
            case "DroneShooterD":
                TargetPool = DroneShooterD;
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
