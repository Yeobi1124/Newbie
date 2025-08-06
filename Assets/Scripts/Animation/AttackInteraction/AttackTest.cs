using UnityEngine;

public class AttackTest : MonoBehaviour, IHittable
{
    public bool isFriendlyToPlayer;
    public float health = 20f;
    public float maxHealth = 20f;
    
    public void Hit(float damage)
    {
        health -= damage;
    }

    public bool IsValidTarget(bool isFriendlyToPlayer) => isFriendlyToPlayer != this.isFriendlyToPlayer;
}
