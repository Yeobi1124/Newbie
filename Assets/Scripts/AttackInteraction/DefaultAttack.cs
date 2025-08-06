
using UnityEngine;

public class DefaultAttack : Attack
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IHittable hittable) == false) return;
        if (hittable.IsValidTarget(isFriendlyToPlayer) == false) return;
        
        hittable.Hit(damage);
        gameObject.SetActive(false);
    }
}