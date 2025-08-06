using UnityEngine;

public class DroneBullet : Attack
{
    public float BulletSpeed;
    void Update()
    {
        transform.Translate(BulletSpeed * Time.deltaTime, 0, 0);

        if (transform.position.x < -10)
        {
            gameObject.SetActive(false);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Trigger called?");
        if (col.TryGetComponent(out IHittable hittable))
        {
            Debug.Log("At List HITTABLE");
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
