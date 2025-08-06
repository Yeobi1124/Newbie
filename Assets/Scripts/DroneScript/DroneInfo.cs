using System;
using UnityEditor.Rendering;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Scripting.APIUpdating;

public class DroneInfo : MonoBehaviour, IHittable
{
    public float OriginalHealth;
    public float Health;
    public float DroneSpeed = 1;
    public int ShootNum = 0;
    public bool Shootable = true;
    public bool isFriendly = false; 
    private bool isDestroyed = false;

    void Start()
    {
        ResetDrone();
    }

    void Update()
    {
        if (transform.position.x < -16 || transform.position.x > 16 || transform.position.y < -10 || transform.position.y > 10)
        {
            gameObject.SetActive(false);
        }

        if (Health <= 0 && !isDestroyed)
        {
            isDestroyed = true;
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

    private void Destroyed()
    {
        gameObject.SetActive(false);
    }

    public void ResetDrone()
    {
        Shootable = true;
        Health = OriginalHealth;
        ShootNum = 0;
        isDestroyed = false;
    }

    
}

