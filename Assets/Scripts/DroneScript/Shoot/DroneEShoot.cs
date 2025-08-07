using UnityEngine;

public class DroneEShoot : MonoBehaviour
{
    private float ShootCounter = 0;
    public GameObject EnemyBullet;
    public float ShootDelay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShootDelay = Random.Range(0f, 3f);
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
        AudioManager.Instance.PlaySE(AudioManager.SEType.DroneShootA);
        GameObject bulletA = DroneObjectManager.Instance.PullObject("BulletEnemy");
        bulletA.transform.position = transform.position;
        bulletA.transform.Translate(0.7f, -0.04f, 0);

        GameObject bulletB = DroneObjectManager.Instance.PullObject("BulletEnemy");
        bulletB.transform.position = transform.position;
        bulletB.transform.Translate(0.5f, -0.1f, 0);

        ShootDelay = Random.Range(0.3f, 3f);
        gameObject.GetComponent<DroneInfo>().ShootNum++;
    }
}
