using UnityEngine;

public class ChasingBullet : Attack
{
    public float BulletSpeed;
    public Vector3 moveDirection;
    void Update()
    {
        
        transform.position += moveDirection * BulletSpeed * Time.deltaTime;

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
                return;
            }
        }
        if (col.gameObject.CompareTag("Border"))
        {
            gameObject.SetActive(false);
        }
    }
}
