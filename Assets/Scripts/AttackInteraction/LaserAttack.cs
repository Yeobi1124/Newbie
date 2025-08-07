using System;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : Attack
{
    [SerializeField] private float damageInterval = 0.2f;

    [SerializeField]
    private List<TempTuple> targets = new List<TempTuple>();

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IHittable hittable) == false) return;
        if (hittable.IsValidTarget(isFriendlyToPlayer) == false) return;
        
        targets.Add(new TempTuple(other.gameObject, Time.time));
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i].Item2 <= Time.time)
            {
                // targets[i].Item1.Hit(damage);
                targets[i].Item1.GetComponent<IHittable>().Hit(damage);
                targets[i] = new TempTuple(targets[i].Item1, targets[i].Item2 + damageInterval);
                
                // AudioManager.Instance.PlaySE(SEType.La);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IHittable hittable) == false) return;
        if (hittable.IsValidTarget(isFriendlyToPlayer) == false) return;

        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i].Item1.GetComponent<IHittable>() == hittable)
            {
                targets.RemoveAt(i);
                break;
            }
        }
    }
}

[Serializable]
public class TempTuple
{
    // public IHittable Item1; // For Debug...
    public GameObject Item1;
    public float Item2;

    // public TempTuple(IHittable item1, float item2){
    //     Item1 = item1;
    //     Item2 = item2;
    // }

    public TempTuple(GameObject item1, float item2){
        Item1 = item1;
        Item2 = item2;
    }
}