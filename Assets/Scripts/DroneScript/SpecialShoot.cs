using UnityEngine;

public class SpecialShoot : MonoBehaviour
{
    public GameObject specialBullet;
    private float ShootCounter = 0;
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
        Debug.Log("Special shooting");
        GameObject bulletA = DroneObjectManager.Instance.PullObject("BulletEnemyBig");
        bulletA.transform.position = transform.position;
        gameObject.GetComponent<DroneInfo>().ShootNum++;
    }


}

