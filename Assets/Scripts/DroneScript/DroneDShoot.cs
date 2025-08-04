using UnityEngine;

public class DroneDShoot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float ShootCounter = 0;
    public GameObject EnemyBullet;
    public float ShootDelay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ShootCounter >= ShootDelay)
        {
            Shoot();
            ShootCounter = 0;
        }

        ShootCounter += Time.deltaTime;
    }

    public void Shoot()
    {
        GameObject bulletA = DroneObjectManager.Instance.PullObject("BulletEnemy");
        bulletA.transform.position = transform.position;
        bulletA.transform.Translate(0.6f, -0.35f, 0);
        GameObject bulletB = DroneObjectManager.Instance.PullObject("BulletEnemy");
        bulletB.transform.position = transform.position;
        bulletB.transform.Translate(0.45f, -0.4f, 0);
        
    }

}
