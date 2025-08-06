using UnityEngine;

public class SpecialBullet : Attack
{
    public GameObject muzzleEffect;
    public float BulletSpeed;
    public Vector3 moveDirection;
    public bool charging;

    void OnEnable()
    {
        charging = true;
        gameObject.GetComponent<MuzzleFlashAnimation>().ChargeStart();
    }
    void Update()
    {   
        charging = gameObject.GetComponent<MuzzleFlashAnimation>().charging;
        if (!charging)
        {
            gameObject.GetComponent<MuzzleFlashAnimation>().Shoot();
            if (moveDirection == new Vector3(0, 0, 0)) moveDirection = new Vector3(-1, 0, 0);
            transform.position += moveDirection * BulletSpeed * Time.deltaTime;
        }
        if (transform.position.x < -10)
        {
            gameObject.SetActive(false);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("Trigger called?");
        if (col.TryGetComponent(out IHittable hittable))
        {
            //Debug.Log("At List HITTABLE");
            if (hittable.IsValidTarget(isFriendlyToPlayer))
            {
                hittable.Hit(damage);
                gameObject.SetActive(false);
                moveDirection = new Vector3(0, 0, 0);
                return;
            }
        }
        if (col.gameObject.CompareTag("Border"))
        {
            gameObject.SetActive(false);
        }
    }

}
