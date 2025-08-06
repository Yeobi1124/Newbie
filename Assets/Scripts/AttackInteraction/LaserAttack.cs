using System;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : Attack
{
    [SerializeField] private float damageInterval = 0.2f;

    [SerializeField]
    private List<Tuple<IHittable, float>> targets = new List<Tuple<IHittable, float>>();

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IHittable hittable) == false) return;
        if (hittable.IsValidTarget(isFriendlyToPlayer) == false) return;
        
        targets.Add(new Tuple<IHittable, float>(hittable, Time.time));
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i].Item2 > Time.time)
            {
                targets[i].Item1.Hit(damage);
                targets[i] = new Tuple<IHittable, float>(targets[i].Item1, targets[i].Item2 + damageInterval);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IHittable hittable) == false) return;
        if (hittable.IsValidTarget(isFriendlyToPlayer) == false) return;

        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i].Item1 == hittable)
            {
                targets.RemoveAt(i);
                break;
            }
        }
    }
}