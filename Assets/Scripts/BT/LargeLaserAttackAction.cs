using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "LargeLaserAttack", story: "Agent attacks [target] with laser", category: "Action", id: "220bc9d9342aac0aa7a79d476bfed5b6")]
public partial class LargeLaserAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

