using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DeathAnimation", story: "[Self] move to [DeathPoint] with [speed]", category: "Action", id: "4afc072ada93f0340e12b0ea8ce6b811")]
public partial class DeathAnimationAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Transform> DeathPoint;
    [SerializeReference] public BlackboardVariable<float> Speed;
    Transform bossTransform;
    Vector2 direction;
    float thresh = 0.1f;
    protected override Status OnStart()
    {
        bossTransform = Self.Value.GetComponent<Transform>();
        direction = (DeathPoint.Value.position-bossTransform.position).normalized;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        bossTransform.Translate(direction*Time.deltaTime*Speed.Value);

        if (bossTransform.position == DeathPoint.Value.position)
            return Status.Success;

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

