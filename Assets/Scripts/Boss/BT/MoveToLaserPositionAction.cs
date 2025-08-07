using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToLaserPosition", story: "[Self] move to [LaserPosition] with [speed]", category: "Action", id: "c142c51aedfcd10353ce8b99fbbffac6")]
public partial class MoveToLaserPositionAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Transform> LaserPosition;
    [SerializeReference] public BlackboardVariable<float> Speed;
    private Transform bossTransform;
    private Vector2 targetPos;
    private float arriveThreshold = 0.1f;
    protected override Status OnStart()
    {
        if (Self == null )
            return Status.Failure;

        bossTransform = Self.Value.transform;
        targetPos = LaserPosition.Value.position;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (bossTransform == null) return Status.Failure;

        Vector2 currentPos = bossTransform.position;
        Vector2 direction = (targetPos - currentPos).normalized;

        bossTransform.Translate(direction * Speed.Value * Time.deltaTime);

        if (Vector2.Distance(currentPos, targetPos) < arriveThreshold)
        {
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

