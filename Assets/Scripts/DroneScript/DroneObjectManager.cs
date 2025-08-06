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
    public GameObject DroneShooterCPrefeb;
    public GameObject DroneShooterDPrefeb;
    public GameObject DroneShooterEPrefeb;
    public GameObject DroneShooterFPrefeb;
    public GameObject DroneShooterGPrefeb;
    public GameObject DroneChargerAPrefeb;
    public GameObject DroneChargerBPrefeb;

    public GameObject[] BulletEnemyA;
    public GameObject[] BulletEnemyB;
    public GameObject[] BulletEnemyC;
    public GameObject[] DroneShooterA;
    public GameObject[] DroneShooterB;
    public GameObject[] DroneShooterC;
    public GameObject[] DroneShooterD;
    public GameObject[] DroneShooterE;
    public GameObject[] DroneShooterF;
    public GameObject[] DroneShooterG;
    public GameObject[] DroneChargerA;
    public GameObject[] DroneChargerB;

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
        BulletEnemyC = new GameObject[100];
        DroneShooterA = new GameObject[20];
        DroneShooterB = new GameObject[20];
        DroneShooterC = new GameObject[20];
        DroneShooterD = new GameObject[20];
        DroneShooterE = new GameObject[20];
        DroneShooterF = new GameObject[20];
        DroneShooterG = new GameObject[20];
        DroneChargerA = new GameObject[20];
        DroneChargerB = new GameObject[20];

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

        for (int index = 0; index < DroneShooterE.Length; index++)
        {
            DroneShooterE[index] = Instantiate(DroneShooterEPrefeb);
            DroneShooterE[index].SetActive(false);
        }

        for (int index = 0; index < DroneShooterF.Length; index++)
        {
            DroneShooterF[index] = Instantiate(DroneShooterFPrefeb);
            DroneShooterF[index].SetActive(false);
        }

        for (int index = 0; index < DroneShooterG.Length; index++)
        {
            DroneShooterG[index] = Instantiate(DroneShooterGPrefeb);
            DroneShooterG[index].SetActive(false);
        }

        for (int index = 0; index < DroneChargerA.Length; index++)
        {
            DroneChargerA[index] = Instantiate(DroneChargerAPrefeb);
            DroneChargerA[index].SetActive(false);
        }

        for (int index = 0; index < DroneChargerB.Length; index++)
        {
            DroneChargerB[index] = Instantiate(DroneChargerBPrefeb);
            DroneChargerB[index].SetActive(false);
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
            case "DroneShooterC":
                TargetPool = DroneShooterC;
                break;
            case "DroneShooterD":
                TargetPool = DroneShooterD;
                break;
            case "DroneShooterE":
                TargetPool = DroneShooterE;
                break;
            case "DroneShooterF":
                TargetPool = DroneShooterF;
                break;
            case "DroneShooterG":
                TargetPool = DroneShooterG;
                break;

            case "DroneChargerA":
                TargetPool = DroneChargerA;
                break;
            case "DroneChargerB":
                TargetPool = DroneChargerB;
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
