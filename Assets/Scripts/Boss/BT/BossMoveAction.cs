using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossMove", story: "[Self] Randomly Wanders [WayPoints] with [speed]", category: "Action", id: "daae4b4b992c5f6aae8d57dea09958d6")]
public partial class BossMoveAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<List<GameObject>> WayPoints;
    [SerializeReference] public BlackboardVariable<float> Speed;
    private Transform bossTransform;
    private Vector2 targetPos;
    private float arriveThreshold = 0.1f;

    protected override Status OnStart()
    {
        if (Self == null || WayPoints == null || WayPoints.Value == null || WayPoints.Value.Count == 0)
            return Status.Failure;

        bossTransform = Self.Value.transform;
        SetRandomTarget();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (bossTransform == null) return Status.Failure;

        Vector2 currentPos = bossTransform.position;
        Vector2 direction = (targetPos - currentPos).normalized;

        bossTransform.Translate(direction * Speed.Value*Time.deltaTime);

        if (Vector2.Distance(currentPos, targetPos) < arriveThreshold)
        {
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }

    private void SetRandomTarget()
    {
        var wayPointsList = WayPoints.Value;
        if (wayPointsList == null || wayPointsList.Count == 0) return;
        int index = UnityEngine.Random.Range(0, wayPointsList.Count);
        targetPos = wayPointsList[index].transform.position;
    }
}

