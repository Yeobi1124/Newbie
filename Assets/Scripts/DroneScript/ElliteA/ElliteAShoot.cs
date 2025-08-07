using System.Threading;
using AllIn1SpriteShader;
using UnityEngine;

public class ElliteAShoot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float randomShootCounter = 0;
    public float randomShootDelay;
    private float arcShootCounter = 0;
    public float arcShootDelay;
    public float delayTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        randomShootDelay = Random.Range(0.3f, 2f);
    }


    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<ElliteInfo>().Shootable)
        {
            if (randomShootCounter >= randomShootDelay)
            {
                RandomShoot();
                randomShootCounter = 0;
            }

            if (arcShootCounter >= arcShootDelay)
            {
                ArcShoot();
                arcShootCounter = 0;
            }

            randomShootCounter += Time.deltaTime;
            arcShootCounter += Time.deltaTime;
        }
    }

    public void RandomShoot()
    {
        Debug.Log("RandomShoot");
        GameObject bulletA = DroneObjectManager.Instance.PullObject("BulletEnemy");
        bulletA.transform.position = transform.position;
        bulletA.transform.Translate(-2f, -1.3f, 0,Space.World);
        bulletA.transform.localScale = new Vector3(-1, 1, 1);
        gameObject.GetComponent<ElliteInfo>().ShootNum++;
        randomShootDelay = Random.Range(0.3f, 2f);
    }

    public void Delay(float time)
    {
        delayTimer = 0;
        while (delayTimer <= time) delayTimer += Time.deltaTime;
    }

    public void ArcShoot()
    {
        Debug.Log("Arc Shoot!");
        float angleStep = 15f; // 총알 간의 각도 차이
        float startAngle = -angleStep; // 왼쪽에서 시작 (총 3발)

        for (int i = 0; i < 5; i++)
        {
            float angle = startAngle + angleStep * i;
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;
            GameObject bullet = DroneObjectManager.Instance.PullObject("BulletEnemy");
            float angleSpin = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, angleSpin);
            bullet.transform.position = transform.position;
            bullet.transform.Translate(-2.0f, -1.3f, 0, Space.World);
            bullet.transform.localScale = new Vector3(-1, 1, 1);
            bullet.GetComponent<DroneBullet>().moveDirection = direction * (-1);
        }
        gameObject.GetComponent<ElliteInfo>().ShootNum++;
    }
}
