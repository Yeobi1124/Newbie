using UnityEngine;

public class SpecialBullet :Attack
{
    //ToDo - Animation check
    public bool isAnimationIsOver;
    public float BulletSpeed;


    void Start()
    {
        isAnimationIsOver = false;
    }
    void Update()
    {
        if (isAnimationIsOver)
        {
            Shooting();
        }
    }


    private void Shooting()
    {
        transform.Translate(BulletSpeed * Time.deltaTime, 0, 0);

        if (transform.position.x < -10)
        {
            gameObject.SetActive(false);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out IHittable hittable))
        {
            if (hittable.IsValidTarget(isFriendlyToPlayer))
            {
                hittable.Hit(damage);
                gameObject.SetActive(false);
                return;
            }
        }
        if (col.gameObject.CompareTag("Border"))
        {
            gameObject.SetActive(false);
        }
    }

}
