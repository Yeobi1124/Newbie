using System;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : Attack
{
    [SerializeField] private float damageInterval = 0.2f;

    private float time = 0f;

    [SerializeField]
    private List<Tuple<IHittable, float>> targets = new List<Tuple<IHittable, float>>();
    
    private void Update()
    {
        time += Time.deltaTime;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IHittable hittable) == false) return;
        if (hittable.IsValidTarget(isFriendlyToPlayer) == false) return;
        
        hittable.Hit(damage);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
    }
}