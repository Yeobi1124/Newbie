using UnityEngine;
using System.Collections.Generic;

public class BossSpawner : MonoBehaviour
{
    [SerializeField]private GameObject bossPrefab;
    [SerializeField]private GameObject player;
    [SerializeField]private IMissileMover mover;
    [SerializeField]private List<GameObject> backgrounds;
    [SerializeField]private GameObject laser;
    [SerializeField]private Transform laserTransform;
    [SerializeField]private List<GameObject> wayPoints;



    public GameObject SpawnBoss(Vector2 spawnPosition)
    {
        GameObject boss = Instantiate(bossPrefab);
        boss.transform.position = spawnPosition;
        Boss bossStarter = boss.GetComponent<Boss>();
        bossStarter.BTInit(player, mover, backgrounds, laser, wayPoints, laserTransform);
        return boss;
    }
}
