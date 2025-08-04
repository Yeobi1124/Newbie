using System.Numerics;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class DroneBShoot : MonoBehaviour
{
    private float ShootCounter = 0;
    public int ShootNum = 0;
    private GameObject EnemyBullet;
    public bool Shootable = false;
    public float ShootDelay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void OnEnable()
    {
        Shootable = false;
        ShootCounter = 0;
        ShootNum = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Shootable)
        {
            if (ShootCounter >= ShootDelay)
            {
                Shoot();
                ShootCounter = 0;
            }

            ShootCounter += Time.deltaTime;
        }
    }

    public void Shoot()
    {
        GameObject bulletA = DroneObjectManager.Instance.PullObject("BulletEnemy");
        bulletA.transform.position = transform.position;
        bulletA.transform.Translate(1.3f, -0.5f, 0);

        GameObject bulletB = DroneObjectManager.Instance.PullObject("BulletEnemy");
        bulletB.transform.position = transform.position;
        bulletB.transform.Translate(1.0f, -0.55f, 0);
        ShootNum++;
    }
}
