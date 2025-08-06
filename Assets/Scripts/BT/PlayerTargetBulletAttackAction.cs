using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PlayerTargetBulletAttack", story: "[Self] attacks [Target] [bulletNumber] time in [duration]", category: "Action", id: "fbe62009d3003d27378aeec2d5cc9a94")]
public partial class PlayerTargetBulletAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<int> BulletNumber;
    [SerializeReference] public BlackboardVariable<float> Duration;
    private float curDuration;
    private int bulletCnt;
    private float bulletSpeed;
    protected override Status OnStart()
    {
        ResetDuration();
        bulletCnt = 0;
        bulletSpeed = 10;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (bulletCnt >= BulletNumber.Value)
        {
            return Status.Success;
        }

        curDuration += Time.deltaTime;

        if (curDuration >= Duration.Value)
        {
            BulletMake();
            curDuration = 0f;
            bulletCnt++;
        }

        return Status.Running;
    }


    private void BulletMake()
    {
        GameObject bulletObj = ObjectManager.instance.PullObject("BulletEnemy");
        bulletObj.transform.position = Self.Value.transform.position;

        Vector2 bulletDir = CalculateBulletDir(Self.Value.transform.position, Target.Value.transform.position);
        Rigidbody2D bulletRb = bulletObj.GetComponent<Rigidbody2D>();
        bulletRb.linearVelocity = bulletDir * bulletSpeed;

        float angle = Mathf.Atan2(bulletDir.y, bulletDir.x) * Mathf.Rad2Deg;
        bulletObj.transform.rotation = Quaternion.Euler(0,0, angle);
    }

    private Vector2 CalculateBulletDir(Vector2 bossPosition, Vector2 targetPosition) => (targetPosition-bossPosition).normalized;

    private bool IsDurationOver() => curDuration >= Duration.Value; 
    private void ResetDuration()
    {
        curDuration = 0.0f;
    }
    

    protected override void OnEnd()
    {
    }
}

