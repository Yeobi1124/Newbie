using System.CodeDom.Compiler;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    GameObject BulletPlayerPrefab;
    GameObject[] BulletPlayer;

    GameObject[] TargetPool;

    private void Awake()
    {
        BulletPlayer = new GameObject[100];

        Generate();
    }

    void Generate()
    {
        for (int index = 0; index < BulletPlayer.Length; index++)
        {
            BulletPlayer[index] = Instantiate(BulletPlayerPrefab);
            BulletPlayer[index].SetActive(false);
        }
    }

    public GameObject PullObject(string type)
    {
        switch (type)
        {
            case "BulletPlayer":
                TargetPool = BulletPlayer;
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

        return null;
    }
}
