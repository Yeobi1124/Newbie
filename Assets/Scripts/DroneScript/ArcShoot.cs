using UnityEngine;

public class ArcShoot : MonoBehaviour
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
        float angleStep = 30f; // 총알 간의 각도 차이
        float startAngle = -angleStep; // 왼쪽에서 시작 (총 3발)

        for (int i = 0; i < 3; i++)
        {
            float angle = startAngle + angleStep * i;
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;
            GameObject bullet = DroneObjectManager.Instance.PullObject("BulletEnemy");
            float angleSpin = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, angleSpin);
            bullet.transform.position = transform.position;
            bullet.transform.Translate(-1.0f, 0, 0, Space.World);
            bullet.transform.localScale = new Vector3(-1, 1, 1);
            bullet.GetComponent<DroneBullet>().moveDirection = direction*(-1);
        }

        ShootDelay = Random.Range(0.3f, 3f);
        gameObject.GetComponent<DroneInfo>().ShootNum++;
    }
}



