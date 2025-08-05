using System.CodeDom.Compiler;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;
    
    // private라 Inspector에서 접근이 불가능해서 public으로 둠...
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
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        BulletPlayer = new GameObject[100];
        BulletEnemy = new GameObject[100];
        DroneShooter = new GameObject[20];
        DroneCharger = new GameObject[20];

        Generate();
    }

    void Generate()
    {
        for (int index = 0; index < BulletEnemy.Length; index++)
        {
            BulletEnemy[index] = Instantiate(BulletPlayerPrefab);
            BulletEnemy[index].SetActive(false);
        }
        for (int index = 0; index < BulletPlayer.Length; index++)
        {
            BulletPlayer[index] = Instantiate(BulletEnemyPrefab);
            BulletPlayer[index].SetActive(false);
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

        Debug.Log(type + " 풀에 남아있는 오브젝트가 없습니다!");
        return null;
    }
}
