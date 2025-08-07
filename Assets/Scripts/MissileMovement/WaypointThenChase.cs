using BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion;
using UnityEngine;

public class WaypointThenChase : IMissileMover
{
    private enum State {ToWayPoint, Wait, ToTarget};
    private State curState = State.ToWayPoint;

    private Vector2 direction;
    private Transform missileTransform;
    private Vector2 wayPoint;
    private Transform playerTransform;
    private Vector2 target;
    private float speed;
    private float arriveThreshold = 0.2f;
    private float timer = 0.0f;

    public bool IsArrived { get; private set; }


    public WaypointThenChase(Vector2 wayPoint, Transform playerTransform, float speed)
    {
        this.wayPoint = wayPoint;
        this.playerTransform = playerTransform;
        target = new Vector2(0, 0);
        this.speed = speed;
    }

    public void Initialize(Transform missileTransform, Vector2 targetPosition)
    {
        this.missileTransform = missileTransform;
        missileTransform.rotation = Quaternion.Euler(0, 0, 180);
        curState = State.ToWayPoint;
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
            missileTransform.Translate(direction * speed * deltaTime,Space.World);

			
			Vector2 aimDir = ((Vector2)playerTransform.position - wayPoint).normalized;
			RotateSlowly(aimDir, deltaTime);

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
			Vector2 aimDir = ((Vector2)playerTransform.position - curPos).normalized;
			RotateSlowly(aimDir, deltaTime);
			if (timer >= 0.5f)
            {
                curState = State.ToTarget;
                speed *= 3;
				target = playerTransform.position;
                direction = MakeDirection(curPos, target);
            }
        }
        //발사
        else if (curState == State.ToTarget)
        {
            missileTransform.Translate(direction * speed * deltaTime,Space.World);
        }
    }

    private Vector2 MakeDirection(Vector2 curPos, Vector2 destination) => (destination - curPos).normalized;

	private void RotateSlowly(Vector2 targetDirection, float deltaTime)
	{
		float rotateSpeed = 720f; // deg/sec
		float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

		Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
		missileTransform.rotation = Quaternion.RotateTowards(
			missileTransform.rotation,
			targetRotation,
			rotateSpeed * deltaTime
		);
	}
}
