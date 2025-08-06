using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class MoveB : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;                 // 이동 속도
    [SerializeField] private float moveDuration = 2f;              // 방향 유지 시간
    [SerializeField] private float stopDuration = 2f;
    [SerializeField] private Vector2 minBounds = new Vector2(0f, -5f);  // XY 최소 범위
    [SerializeField] private Vector2 maxBounds = new Vector2(9f, 5f);   // XY 최대 범위

    private Vector3 moveDirection;
    private float moveTimer = 0f;
    private float stopTimer = 0f;
    private bool isMoving = true;
    public bool charging;
    public float droneSpeed = 1f;
    private bool summon = true;
    //TODO - RandomMove

    void OnEnable()
    {
        moveSpeed = 3;
        droneSpeed = 1f;
        summon = true;
        gameObject.GetComponent<DroneInfo>().Shootable = false;
    }
    void Start()
    {
        moveSpeed = 3;
        droneSpeed = 1f;
        summon = true;
        gameObject.GetComponent<DroneInfo>().Shootable = false;
    }

    void Update()
    {
        if (summon)
        {
            gameObject.GetComponent<DroneInfo>().Shootable = false;
            InitialMove();
            //SetRandomDirection();
        }
        else
        {
            gameObject.GetComponent<DroneInfo>().Shootable = true;
            RandomMove();
        }

        if (gameObject.GetComponent<DroneInfo>().isDestroyed)
        {
            gameObject.GetComponent<DroneInfo>().Shootable = false;
            droneSpeed = 0;
            moveSpeed = 0;
        }

    }
    private void RandomMove()
    {
        if (isMoving)
        {
            moveTimer += Time.deltaTime;

            Vector3 nextPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

            // X축 경계 체크
            if (nextPosition.x < minBounds.x || nextPosition.x > maxBounds.x)
            {
                moveDirection.x *= -1;
                nextPosition.x = Mathf.Clamp(nextPosition.x, minBounds.x, maxBounds.x);
            }

            // Y축 경계 체크
            if (nextPosition.y < minBounds.y || nextPosition.y > maxBounds.y)
            {
                moveDirection.y *= -1;
                nextPosition.y = Mathf.Clamp(nextPosition.y, minBounds.y, maxBounds.y);
            }

            // 이동 적용
            transform.position = nextPosition;

            if (moveTimer >= moveDuration)
            {
                moveTimer = 0f;
                stopTimer = 0f;
                isMoving = false; // 이동 멈춤 상태로 변경
            }
        }
        else
        {
            // stopTimer += Time.deltaTime;
            // if (stopTimer >= stopDuration)
            // {
            //     stopTimer = 0f;
            //     isMoving = true;      
            //     SetRandomDirection();
            // }

            if (gameObject.GetComponent<DroneInfo>().ShootNum > 2)
            {
                isMoving = true;
                SetRandomDirection();
                gameObject.GetComponent<DroneInfo>().ShootNum = 0;
            }
        }
    }

    private void SetRandomDirection()
    {
        float angle = Random.Range(0f, 360f);
        float rad = angle * Mathf.Deg2Rad;
        moveDirection = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f).normalized;
    }

    private void InitialMove()
    {
        if (transform.position.x > 6.5) transform.Translate(droneSpeed * Time.deltaTime, 0, 0);
        else summon = false;
    }
}
