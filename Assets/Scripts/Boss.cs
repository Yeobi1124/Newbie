using UnityEngine;
using Unity.Behavior;
using System.Collections.Generic;

public class Boss : Attack,IHittable
{
    public float originalHealth;
    public float health;
    public float originalSpeed = 1;
    public float speed;
    public bool isDestroyed = false;
    private bool isFriendly = false;
    private int collideDamage = 25;
    BTInitializer btInitializer;

    [SerializeField] private BehaviorGraphAgent behaviorTree;
    [SerializeField] private GameObject player;
    [SerializeField] private IMissileMover mover;
    [SerializeField] private Transform missileAbove;
    [SerializeField] private Transform missileBelow;
    [SerializeField] private List<GameObject> backgrounds;
    [SerializeField] private GameObject laser;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 1000;
        originalHealth = 1000;
        originalSpeed = 1;
        speed = 1;
        btInitializer = new BTInitializer(behaviorTree);
        btInitializer.Init(player,mover,missileAbove,missileBelow,backgrounds,laser);
    }

    public void Hit(float damage, bool parryable = true)
    {
        health -= damage;
    }

    public bool IsValidTarget(bool isFriendlyToAttacker)
    {
        return isFriendly != isFriendlyToAttacker;
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("Trigger called?");
        if (col.TryGetComponent(out IHittable hittable))
        {
            //Debug.Log("At List HITTABLE");
            if (hittable.IsValidTarget(isFriendlyToPlayer))
            {
                hittable.Hit(collideDamage);
            }
        }
        if (col.gameObject.CompareTag("Border"))
        {
            gameObject.GetComponent<DroneAnimation>().OnDead();
        }
    }

    public void Destroyed()
    {
        isDestroyed = true;
        //gameObject.GetComponent<BossAnimation>().OnDead();
    }
}
