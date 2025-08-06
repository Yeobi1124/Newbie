using UnityEngine;

public class WaypointThenChase : IMissileMover
{
    private enum State {ToWayPoint, Wait, ToTarget};
    private State curState = State.ToWayPoint;

    private Vector2 direction;
    private Transform missileTransform;
    private Vector2 wayPoint;
    private Transform targetTransform;
    private Vector2 target;
    private float speed;
    private float arriveThreshold = 0.1f;
    private float timer = 0.0f;

    public bool IsArrived { get; private set; }


    public WaypointThenChase(Vector2 wayPoint, Transform targetTransform, float speed)
    {
        this.wayPoint = wayPoint;
        this.targetTransform = targetTransform;
        target = new Vector2(0, 0);
        this.speed = speed;
    }

    public void Initialize(Transform missileTransform, Vector2 targetPosition)
    {
        this.missileTransform = missileTransform;
        timer = 0.0f;
    }

    public void Move(float deltaTime)
    {
        if (IsArrived || missileTransform == null) return;

        Vector2 curPos = missileTransform.position;

        Vector2 destination = (curState == State.ToWayPoint) ? wayPoint : target;
        //미사일 위치로 대기하기
        if (curState == State.ToWayPoint)
        {
            direction = MakeDirection(curPos, destination);
            missileTransform.Translate(direction * speed * deltaTime);

            if (Vector2.Distance(curPos, destination) < arriveThreshold)
            {
                curState = State.Wait;
                timer = 0f;
            }
        }
        //대기 상태
        else if (curState == State.Wait)
        {
            timer += deltaTime;
            if (timer >= 0.5f)
            {
                curState = State.ToTarget;
                speed *= 5;

                target = targetTransform.position;
                direction = MakeDirection(curPos, target);
            }
        }
        //발사
        else if (curState == State.ToTarget)
        {
            missileTransform.Translate(direction * speed * deltaTime);
        }
    }

    private Vector2 MakeDirection(Vector2 curPos, Vector2 destination) => (destination - curPos).normalized;
}
