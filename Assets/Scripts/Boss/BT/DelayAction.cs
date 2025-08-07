using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Delay", story: "Wait for [Duration] Seconds", category: "Action", id: "6db46c818c29a60af9185d7edaeff0ac")]
public partial class DelayAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Duration;
    private float curDuration;
    protected override Status OnStart()
    {
        ResetDuration();
        return Status.Running;
    }
    private void ResetDuration()
    {
        curDuration = 0.0f;
    }

    protected override Status OnUpdate()
    {
        curDuration += Time.deltaTime;
        if (IsDurationOver())
            return Status.Success;
        return Status.Running;
    }

    private bool IsDurationOver() => curDuration >= Duration.Value;
    
    protected override void OnEnd()
    {
    }
}

