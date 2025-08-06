using UnityEngine;


public class SpecialShoot : MonoBehaviour
{
    private float ShootCounter = 0;
    public int ShootNum = 0;
    private GameObject EnemyBullet;
    public float ShootDelay;

    public GameObject muzzle;
    public MuzzleFlashAnimation muzzleAnim;
    GameObject bulletA;
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
        bulletA.transform.position = muzzle.transform.position;
        gameObject.GetComponent<DroneInfo>().ShootNum++;
    }
}




