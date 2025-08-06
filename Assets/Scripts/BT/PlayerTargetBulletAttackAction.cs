using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PlayerTargetBulletAttack", story: "[Self] attacks [Target] in each [duration]", category: "Action", id: "fbe62009d3003d27378aeec2d5cc9a94")]
public partial class PlayerTargetBulletAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> Duration;
    private float curDuration;
    private int bulletCnt;
    protected override Status OnStart()
    {
        ResetDuration();
        bulletCnt = 0;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (bulletCnt >= 10)
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
        Debug.Log(Self.Value.transform.position+" "+ Target.Value);
        GameObject bulletObj = ObjectManager.instance.PullObject("BulletEnemy");
        bulletObj.transform.position = Self.Value.transform.position;
        

        Vector2 bulletDir = CalculateBulletDir(Self.Value.transform.position, Target.Value.transform.position);
        BulletController bulletController = bulletObj.GetComponent<BulletController>();
        bulletController.Init(bulletDir);
    }

    private Vector2 CalculateBulletDir(Vector2 bossPosition, Vector2 targetPosition) => (bossPosition - targetPosition).normalized;

    private bool IsDurationOver() => curDuration >= Duration.Value; 
    private void ResetDuration()
    {
        curDuration = 0.0f;
    }
    

    protected override void OnEnd()
    {
    }
}

