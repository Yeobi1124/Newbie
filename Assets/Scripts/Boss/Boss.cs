using UnityEngine;
using Unity.Behavior;
using System.Collections.Generic;

public class Boss : Attack,IHittable
{
    public float originalHealth;
    public float curHealth;
    public float originalSpeed = 1;
    public float speed;
    public bool isDestroyed = false;
    private bool isFriendly = false;
    BTInitializer btInitializer;

    [SerializeField] private BehaviorGraphAgent behaviorTree;
    public void BTInit(GameObject player, IMissileMover mover, 
        List<GameObject> backgrounds, GameObject laser,  List<GameObject> wayPoints,Transform laserTransform)
    {
        curHealth = 1000;
        originalHealth = 1000;
        originalSpeed = 1;
        speed = 1;
        behaviorTree = GetComponent<BehaviorGraphAgent>();
        btInitializer = new BTInitializer(behaviorTree);
        btInitializer.Init(player, mover,backgrounds, laser, originalHealth, wayPoints, laserTransform);
    }
    

    void Update()
    {
        btInitializer.HPUpdate(curHealth);
    }

    public void Hit(float damage, bool parryable = true)
    {
        curHealth -= damage;
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
                hittable.Hit(damage);
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
