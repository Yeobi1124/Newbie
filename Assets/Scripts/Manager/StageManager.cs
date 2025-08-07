using System;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;
    
    [SerializeField]
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
        
    }
}

[Serializable]
public struct SpawnData
{
    public GameObject prefab;
    public GameObject spawnPoint;
    public float spawnTime;
}

[Serializable]
public struct Wave
{
    [SerializeField]
    public List<SpawnData> spawns;
}