using UnityEngine;

public class DroneGShoot : MonoBehaviour
{
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
        if (gameObject.GetComponent<DroneInfo>().Shootable)
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
        GameObject bullet = DroneObjectManager.Instance.PullObject("BulletEnemy");
        bullet.transform.position = transform.position;
        gameObject.GetComponent<DroneInfo>().ShootNum++;
    }

}
