using UnityEngine;

public class DroneFShoot : MonoBehaviour
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
        GameObject bulletA = DroneObjectManager.Instance.PullObject("BulletEnemy");
        bulletA.transform.position = transform.position;
        bulletA.transform.Translate(0.8f,-0.2f,0);
        GameObject bulletB = DroneObjectManager.Instance.PullObject("BulletEnemy");
        bulletB.transform.position = transform.position;
        bulletB.transform.Translate(0.6f, -0.23f, 0);
        gameObject.GetComponent<DroneInfo>().ShootNum++;
    }

}
