using UnityEngine;

public class MoveA : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;                 // 이동 속도
    [SerializeField] private float moveDuration = 1f;              // 방향 유지 시간
    [SerializeField] private Vector2 minBounds = new Vector2(0f, -5f);  // XY 최소 범위
    [SerializeField] private Vector2 maxBounds = new Vector2(9f, 5f);   // XY 최대 범위

    private Vector3 moveDirection;    
    private float moveTimer = 0f;


    public float droneSpeed = 1f;
    private bool summon = true;
    //TODO - RandomMove

    void Start()
    {
        summon = true;
        gameObject.GetComponent<DroneInfo>().Shootable = false;
    }

    void Update()
    {
        if (summon)
        {
            gameObject.GetComponent<DroneInfo>().Shootable = false;
            InitialMove();
        }
        else
        {
            gameObject.GetComponent<DroneInfo>().Shootable = true;
            RandomMove();
        }
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

        // 이동 적용
        transform.position = nextPosition;
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
