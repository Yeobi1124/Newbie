using UnityEngine;

public class ChargerAMove : MonoBehaviour
{
    public float moveSpeed = 2f;
    [SerializeField] private float moveDuration = 1f;
    [SerializeField] private Vector2 minBounds = new Vector2(0f, -5f);
    [SerializeField] private Vector2 maxBounds = new Vector2(9f, 5f);

    [SerializeField] private float detectionRange = 8f;
    [SerializeField] public float chargeSpeed = 5f;

    private Vector3 moveDirection;
    private float moveTimer = 0f;

    public float droneSpeed = 1f;
    private bool summon = true;
    private bool isCharging = false;
    private bool isWaiting = false;

    public float waitTimer;
    public float waitDuration = 1.5f;   

    void Start()
    {
        summon = true;
    }
    void OnDisable()
    {
        //Debug.Log("Disable");
        droneSpeed = 0f;
        chargeSpeed = 0f;
    }

    void Update()
    {
        if (summon)
        {
            InitialMove();
        }
        else
        {
            if (isWaiting)
            {
                waitTimer += Time.deltaTime;
                if (waitTimer >= waitDuration)
                {
                    isWaiting = false;
                    isCharging = true;
                    GameObject player = GameObject.Find("Player");
                    moveDirection = (player.transform.position - transform.position).normalized;
                }
                return;
            }

            if (!isCharging)
            {
                Detect(); // 탐지 후 대기 상태 진입 여부 확인
            }

            if (isCharging)
            {
                ChargeToPlayer(); // 돌진 상태
            }
            else
            {
                RandomMove(); // 평상시 이동
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
            isWaiting = true;
            waitTimer = 0f;
            moveDirection = (player.transform.position - transform.position).normalized;
            AudioManager.Instance.PlaySE(AudioManager.SEType.DroneDashReady);
        }
    }

    private void ChargeToPlayer()
    {
        transform.position += moveDirection * chargeSpeed * Time.deltaTime;
    }
}