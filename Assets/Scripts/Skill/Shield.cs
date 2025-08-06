using System;
using UnityEngine;

public class Shield : MonoBehaviour, IHittable
{
    [SerializeField] private Parry parry;
    [SerializeField] private bool isFriendlyToPlayer = true;
    
    public void Hit(float damage, bool parryable = true)
    {
        if (parryable == false) return;
        parry.ParrySuccess(damage);
    }

    public bool IsValidTarget(bool isFriendlyToPlayer) => isFriendlyToPlayer != this.isFriendlyToPlayer;
}
