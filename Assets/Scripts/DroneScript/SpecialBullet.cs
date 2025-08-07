using UnityEngine;

public class SpecialBullet : Attack
{
    public float BulletSpeed;
    public Vector3 moveDirection;
    public bool charging;
    public float timer;
    public float animationDelay;
    void OnEnable()
    {
        charging = true;
        timer = 0f;
        moveDirection = Vector3.zero; // 기본값
    }

    void Update()
    {
        if (charging)
        {
            // 애니메이션 딜레이 동안 대기
            timer += Time.deltaTime;
            if (timer >= animationDelay)
            {
                charging = false;
                timer = 0f;

                // 이동 방향이 비어있으면 기본값으로 설정
                if (moveDirection == Vector3.zero)
                    moveDirection = new Vector3(-1, 0, 0);
            }
        }
        else
        {
            // 애니메이션 딜레이 이후에는 계속 이동
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
