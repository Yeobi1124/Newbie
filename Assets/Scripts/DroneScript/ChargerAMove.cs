using UnityEngine;

public class ChrgerMoveA : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveDuration = 1f;
    [SerializeField] private Vector2 minBounds = new Vector2(0f, -5f);
    [SerializeField] private Vector2 maxBounds = new Vector2(9f, 5f);

    [SerializeField] private float detectionRange = 8f;
    [SerializeField] private float chargeSpeed = 5f;

    private Vector3 moveDirection;
    private float moveTimer = 0f;

    public float droneSpeed = 1f;
    private bool summon = true;
    private bool detecting = true;
    private bool isCharging = false;

    void Start()
    {
        summon = true;
        gameObject.GetComponent<DroneInfo>().Shootable = false;
    }

    void Update()
    {
        if (summon)
        {
            InitialMove();
        }
        else
        {
            if (!isCharging)
            {
                Detect(); // 탐지 후 돌진 조건 확인
            }

            if (isCharging)
            {
                ChargeToPlayer(); // 돌진 중이면 플레이어 향해 돌진
            }
            else
            {
                RandomMove(); // 아니면 계속 랜덤 이동
            }
        }
    }

    private void InitialMove()
    {
        if (transform.position.x > 6.5f)
            transform.Translate(droneSpeed * Time.deltaTime, 0, 0);
        else
            summon = false;
    }

    private void RandomMove()
    {
        moveTimer += Time.deltaTime;

        if (moveTimer >= moveDuration)
        {
            moveTimer = 0f;
            SetRandomDirection();
        }

        Vector3 nextPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

        if (nextPosition.x < minBounds.x || nextPosition.x > maxBounds.x)
        {
            moveDirection.x *= -1;
            nextPosition.x = Mathf.Clamp(nextPosition.x, minBounds.x, maxBounds.x);
        }

        if (nextPosition.y < minBounds.y || nextPosition.y > maxBounds.y)
        {
            moveDirection.y *= -1;
            nextPosition.y = Mathf.Clamp(nextPosition.y, minBounds.y, maxBounds.y);
        }

        transform.position = nextPosition;
    }

    private void SetRandomDirection()
    {
        float angle = Random.Range(0f, 360f);
        float rad = angle * Mathf.Deg2Rad;
        moveDirection = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f).normalized;
    }

    private void Detect()
    {
        GameObject player = GameObject.Find("Player");
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= detectionRange)
        {
            isCharging = true;
            moveDirection = (player.transform.position - transform.position).normalized;
        }
    }

    private void ChargeToPlayer()
    {
        transform.position += moveDirection * chargeSpeed * Time.deltaTime;
    }
}