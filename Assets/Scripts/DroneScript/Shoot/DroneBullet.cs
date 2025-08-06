using UnityEngine;

public class DroneBullet : MonoBehaviour
{
    public float BulletSpeed;
    public int damage = 1;
    public bool isFriendlyToPlayer = false; 

    void Update()
    {
        transform.Translate(BulletSpeed * Time.deltaTime, 0, 0);

        if (transform.position.x < -10)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
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
