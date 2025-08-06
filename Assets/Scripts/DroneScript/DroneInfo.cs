using System;
using UnityEditor.Rendering;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Scripting.APIUpdating;

public class DroneInfo : MonoBehaviour
{
    public float OriginalHealth;
    public float Health;
    public float DroneSpeed = 1;
    public int ShootNum = 0;
    public bool Shootable = true;
    public Animator animator;
    public int check = 0;
    void Start()
    {
        Shootable = true;
        Health = OriginalHealth;
        ShootNum = 0;
    }

    void Onable()
    {
        Shootable = true;
        Health = OriginalHealth;
        ShootNum = 0;        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x < -16 || gameObject.transform.position.x > 16 || gameObject.transform.position.y < -10 || gameObject.transform.position.y > 10)
        {
            gameObject.SetActive(false);
        }

        if (Health <= 0)
        {
            check++;
            Destroyed();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Health -= 1;
    }

    
    //파괴시 가동
    private void Destroyed()
    {
        //animator.Play("DroneExplosion");
        gameObject.SetActive(false);
    }
}
