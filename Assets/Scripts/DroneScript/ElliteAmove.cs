using System.Collections;
using UnityEngine;

public class ElliteAmove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;                 // 이동 속도
    [SerializeField] private float moveDuration = 1f;              // 방향 유지 시간
    [SerializeField] private Vector2 minBounds = new Vector2(0f, -5f);  // XY 최소 범위
    [SerializeField] private Vector2 maxBounds = new Vector2(9f, 5f);   // XY 최대 범위

    private Vector3 moveDirection;     // 현재 이동 방향
    private float moveTimer = 0f;
    private bool shootable;
    public float xCenter;
    public float yCenter;
    float radius;
    public float timer;
    public float droneSpeed = 1f;
    private bool summon = true;
    private float startAngle = 0f;
    private float stayTimer = 0; 
    private bool isMoving = true;
    private bool stayOver;

    void Onable()
    {
        timer = 0;
        xCenter = 6f;
        yCenter = 0f;
        stayOver = false;
        isMoving = true;
        summon = true;
    }

    void Update()
    {
        if (summon)
        {
            gameObject.GetComponent<ElliteInfo>().Shootable = false;
            InitialMove();
            //SetRandomDirection();
        }
        else
        {
            gameObject.GetComponent<ElliteInfo>().Shootable = true;
            RandomMove();
        }

        if (gameObject.GetComponent<ElliteInfo>().isDestroyed)
        {
            gameObject.GetComponent<ElliteInfo>().Shootable = false;
            droneSpeed = 0;
            moveSpeed = 0;
        }

    }

    void RandomMove(){
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
                isMoving = false; // 이동 멈춤 상태로 변경
            }
        }
        else
        {
            if (gameObject.GetComponent<ElliteInfo>().ShootNum > 2)
            {
                if (stayOver)
                {
                    isMoving = true;
                    SetRandomDirection();
                    gameObject.GetComponent<ElliteInfo>().ShootNum = 0;
                    stayOver = false;
                }

                else
                {
                    Stay();
                }
            }
        }
    }



    private void SetRandomDirection()
    {
        float angle = Random.Range(0f, 360f);
        float rad = angle * Mathf.Deg2Rad;
        moveDirection = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f).normalized;
        gameObject.GetComponent<ElliteInfo>().Shootable = false;
    }

    private void InitialMove()
    {
        if (transform.position.x > 6.5) transform.Translate(droneSpeed * Time.deltaTime, 0, 0);
        else
        {
            Vector3 center = new Vector3(xCenter, yCenter, 0f);
            Vector3 dir = transform.position - center;
            radius = dir.magnitude;
            summon = false;
            startAngle = Mathf.Atan2(dir.y, dir.x);
        }
    }

    private void CircleMove()
    {

        float angularSpeed = 2f;
        Vector3 circleCenter = new Vector3(xCenter, yCenter, 0f);

        float angle = startAngle + timer * angularSpeed;

        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        transform.position = circleCenter + new Vector3(x, y, 0f);
        timer += Time.deltaTime;
    }

    private void Stay()
    {
        gameObject.GetComponent<ElliteInfo>().Shootable = false;
        if (stayTimer >= 2f)
        {
            stayOver = true;
            stayTimer = 0;
        }
        else stayTimer += Time.deltaTime;
    }
}
