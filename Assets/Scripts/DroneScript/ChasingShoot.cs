using UnityEngine;

public class ChasingShoot : MonoBehaviour
{
    private float ShootCounter = 0;
    public int ShootNum = 0;
    public float ShootDelay;
    private Vector3 moveDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
        Debug.Log($"Chasing Shoot: {gameObject.GetComponent<DroneInfo>().Shootable}");
        if (!gameObject.GetComponent<DroneInfo>().Shootable) return;
        GameObject bulletA = DroneObjectManager.Instance.PullObject("BulletChase");
        bulletA.transform.position = transform.position;
        bulletA.transform.Translate(-1.3f, -0.5f, 0, Space.World);
        Detect(bulletA);

        bulletA.GetComponent<ChasingBullet>().moveDirection = moveDirection;

        GameObject bulletB = DroneObjectManager.Instance.PullObject("BulletChase");
        bulletB.transform.position = transform.position;
        bulletB.transform.Translate(-1.0f, -0.55f, 0, Space.World);
        Detect(bulletB);
        bulletB.GetComponent<ChasingBullet>().moveDirection = moveDirection;
        gameObject.GetComponent<DroneInfo>().ShootNum++;
    }

    private void Detect(GameObject bullet)
    {
        GameObject player = GameObject.Find("Player");
        if (player == null) return;
        moveDirection = (player.transform.position - bullet.transform.position).normalized;

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
