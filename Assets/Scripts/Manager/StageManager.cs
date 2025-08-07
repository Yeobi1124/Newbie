using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public int currentWave = 0;
    public float waveTime = 0;
    [SerializeField, Tooltip("Please using FIFO(Queue) method")]
    public List<Wave> waves = new List<Wave>();
    
    [SerializeField]
    public List<GameObject> remainEnemies = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private int CheckRemainEnemies()
    {
        List<GameObject> temp = new List<GameObject>();
        int cnt = 0;

        foreach (GameObject obj in remainEnemies)
        {
            if (obj.activeSelf == true)
            {
                temp.Add(obj);
                cnt++;
            }
        }

        remainEnemies = temp;
        return cnt;
    }

    private void Update()
    {
        if (waves.Count <= currentWave) return;
        
        // Check Current Wave Finish Condition
        if (CheckRemainEnemies() <= 0 && waves[currentWave].Count == 0)
        {
            currentWave++;
            waveTime = 0;
        }
        
        // Spawn Enemey
        waveTime += Time.deltaTime;
        
        Wave wave = waves[currentWave];
        Debug.Log(currentWave);
        while (IsSpawnAble(wave))
        {
            // Spawn Drone
            GameObject enemy = DroneObjectManager.Instance.PullObject(wave.spawns[0].enemyName);
            enemy.transform.position = wave.spawns[0].spawnPoint.transform.position;
            enemy.SetActive(true);
            remainEnemies.Add(enemy);
            wave.spawns.RemoveAt(0);
        }
    }
    private bool IsSpawnAble(Wave wave)
    {
        if (wave.Count <= 0)
            return false;
        if (wave.spawns.Count <= 0)
            return false;
        return wave.spawns[0].spawnTime <= waveTime;

    }

}


[Serializable]
public record SpawnData
{
    public string enemyName;
    public GameObject spawnPoint;
    public float spawnTime;
}

[Serializable]
public record Wave
{
    [SerializeField]
    public List<SpawnData> spawns;
    
    public int Count => spawns.Count;
}