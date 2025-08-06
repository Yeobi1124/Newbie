using UnityEngine;

public class SpecialBullet : Attack
{
    public float BulletSpeed;
    public Vector3 moveDirection;
    public bool charging;
    public GameObject muzzle;
    public MuzzleFlashAnimation muzzleAnim;
    public float timer;
    public float duration;
    void OnEnable()
    {
        timer = 0;
        charging = true;
        muzzleAnim = muzzle.GetComponent<MuzzleFlashAnimation>();
    }
    void Update()
    {
        if (!charging)
        {
            if (moveDirection == new Vector3(0, 0, 0)) moveDirection = new Vector3(-1, 0, 0);
            transform.position += moveDirection * BulletSpeed * Time.deltaTime;
            charging = true;
        }
        if (transform.position.x < -10)
        {
            gameObject.SetActive(false);
        }

        if (timer >= duration)
        {
            charging = false;
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
