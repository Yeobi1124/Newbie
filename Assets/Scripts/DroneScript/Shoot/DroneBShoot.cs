using UnityEngine;

public class DroneBShoot : MonoBehaviour
{
    private float ShootCounter = 0;
    public int ShootNum = 0;
    private GameObject EnemyBullet;
    public float ShootDelay;

    public GameObject muzzle;
    private MuzzleFlashAnimation muzzleAnim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        muzzleAnim = muzzle.GetComponent<MuzzleFlashAnimation>();
    }

    void OnEnable()
    {
        ShootCounter = 0;
        ShootNum = 0;
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
        muzzleAnim.Shoot();

        GameObject bulletA = DroneObjectManager.Instance.PullObject("BulletEnemy");
        bulletA.transform.position = transform.position;
        bulletA.transform.Translate(1.3f, -0.5f, 0);

        GameObject bulletB = DroneObjectManager.Instance.PullObject("BulletEnemy");
        bulletB.transform.position = transform.position;
        bulletB.transform.Translate(1.0f, -0.55f, 0);
        gameObject.GetComponent<DroneInfo>().ShootNum++;
    }
}
