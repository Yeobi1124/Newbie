
using UnityEngine;
using UnityEngine.SceneManagement;

public class DroneBullet : Attack
{
    public float BulletSpeed;
    public Vector3 moveDirection;
    public Quaternion originalRotation;
    public GameObject particle;
    void Start()
    {
        originalRotation = Quaternion.Euler(0, 0, 0);
    }
    void OnEnable()
    {
        //Debug.Log("?????");
        moveDirection = Vector3.zero;
        gameObject.transform.rotation = originalRotation;
    }
    void Update()
    {
        if (moveDirection == new Vector3(0, 0, 0)) moveDirection = new Vector3(-1, 0, 0);
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
                // Debug.Log("GetHit");
                AudioManager.Instance.PlaySE(AudioManager.SEType.PlayerHit);
                Instantiate(particle, gameObject.transform.position, Quaternion.identity);
                hittable.Hit(damage);
                gameObject.SetActive(false);
                moveDirection = new Vector3(0, 0, 0);
                return;
            }
        }
        if (col.gameObject.CompareTag("Border"))
        {
            if (SceneManager.GetActiveScene().name == "StartScene") Destroy(gameObject);
            else gameObject.SetActive(false);
        }

    }


}

