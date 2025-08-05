using System;
using UnityEditor.Rendering;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class DroneInfo : MonoBehaviour
{
    public float OriginalHealth;
    public float Health;
    public float DroneSpeed = 1;
    public int ShootNum = 0;
    public bool Shootable = true;

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
            Destroyed();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "border") gameObject.SetActive(false);
    }

    
    //파괴시 가동
    private void Destroyed()
    {
        gameObject.SetActive(false);
    }
}
