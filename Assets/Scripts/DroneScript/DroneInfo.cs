using System;
using UnityEditor.Rendering;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Scripting.APIUpdating;

public class DroneInfo : Attack, IHittable
{
    public float OriginalHealth;
    public float Health;
    public float OriginalSpeed = 1;
    public float DroneSpeed;
    public int ShootNum = 0;
    public bool Shootable = false;
    public bool isFriendly = false;
    public bool isDestroyed = false;
    public float chargeDamage = 20;
    void Start()
    {
        ResetDrone();
        DroneSpeed = OriginalSpeed;
    }

    void Update()
    {
        if (transform.position.x < -16 || transform.position.x > 16 || transform.position.y < -10 || transform.position.y > 10)
        {
            Destroyed();
        }

        if (Health <= 0 && !isDestroyed)
        {
            Destroyed();
        }
    }

    public void Hit(float damage, bool parryable = true)
    {
        Health -= damage;
    }

    public bool IsValidTarget(bool isFriendlyToAttacker)
    {
        return isFriendly != isFriendlyToAttacker;
    }


    public void ResetDrone()
    {
        Shootable = true;
        Health = OriginalHealth;
        ShootNum = 0;
        isDestroyed = false;
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("Trigger called?");
        if (col.TryGetComponent(out IHittable hittable))
        {
            //Debug.Log("At List HITTABLE");
            if (hittable.IsValidTarget(isFriendlyToPlayer))
            {
                Destroyed();
                hittable.Hit(chargeDamage);
                DroneSpeed = 0;
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
        Shootable = false;
        Debug.Log($"NONE Shootable: {gameObject.GetComponent<DroneInfo>().Shootable}");
        gameObject.GetComponent<DroneAnimation>().OnDead();
        //GameObject shard = DroneObjectManager.Instance.PullObject("EnergyShard");
    }

}

