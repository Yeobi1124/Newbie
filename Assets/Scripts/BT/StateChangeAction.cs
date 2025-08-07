using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StateChange", story: "[State] change to [newState]", category: "Action", id: "89d4d917af77435579f16b5654af0d80")]
public partial class StateChangeAction : Action
{
    [SerializeReference] public BlackboardVariable<State> State;
    [SerializeReference] public BlackboardVariable<State> NewState;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        State.Value = NewState.Value;
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

