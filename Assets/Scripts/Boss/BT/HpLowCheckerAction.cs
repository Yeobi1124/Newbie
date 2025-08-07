using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "HPLowChecker", story: "IF [curHP] less than [PhaseHpPercent] percent of [maxHP]", category: "Action", id: "f1a7728cefa288f784703674bf6320f7")]
public partial class HpLowCheckerAction : Action
{
    [SerializeReference] public BlackboardVariable<float> CurHP;
    [SerializeReference] public BlackboardVariable<float> PhaseHpPercent;
    [SerializeReference] public BlackboardVariable<float> MaxHP;

    float percentFloat;
    protected override Status OnStart()
    {
        percentFloat = PhaseHpPercent.Value / 100.0f;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return CurHP <= percentFloat * MaxHP.Value ? Status.Success : Status.Failure; 
    }

    protected override void OnEnd()
    {
    }
}

