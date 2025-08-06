using System;
using UnityEngine;

public class AutoFire : MonoBehaviour
{
    private TestAttack _attack;

    [SerializeField] private float interval = 1f;
    private float time;
    
    private void Awake()
    {
        _attack = GetComponent<TestAttack>();
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time >= interval)
        {
            time -= interval;
            _attack.Use();
        }
    }
}
