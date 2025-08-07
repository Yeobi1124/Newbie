using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossMovePattern", story: "[Boss] rush to [PlayerTransform]", category: "Action", id: "bbfeeb7d01958ea209cf410e2af5559f")]
public partial class BossMovePatternAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Boss;
    [SerializeReference] public BlackboardVariable<Transform> PlayerTransform;
    Vector2 bossPos;
    Vector2 playerPos;
    bool IsReturning;
    float percent; 
    protected override Status OnStart()
    {
        bossPos = Boss.Value.transform.position;
        //playerPos = Player.Value.transform.position;
        playerPos = PlayerTransform.Value.position;
        percent = 0;
        IsReturning = false;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (!IsReturning)
            percent += Time.deltaTime;
        else
            percent -= Time.deltaTime / 2;

        Boss.Value.transform.position = Vector2.Lerp(bossPos, playerPos, percent/1.5f);

        if (percent <= 0.0f&&IsReturning)
            return Status.Success;
        if (percent >= 1.5f)
            IsReturning = true;

        return Status.Running;

    }

    protected override void OnEnd()
    {
    }
}

