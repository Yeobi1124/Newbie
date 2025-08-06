using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossMissilePattern", story: "[Self] attacks [Player] to [missilePosition] with [speed]", category: "Action", id: "4a46c270467dbd4ab741c800e64aad47")]
public partial class BossMissilePatternAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    [SerializeReference] public BlackboardVariable<Transform> MissilePosition;
    [SerializeReference] public BlackboardVariable<float> Speed;
    protected override Status OnStart()
    {
        MissileMake();
        return Status.Running;
    }

    private void MissileMake()
    {
        GameObject missileObj = ObjectManager.instance.PullObject("DroneShooter"); // 수정하기
        var mover = new WaypointThenChase(MissilePosition.Value.position, Player.Value.transform, Speed.Value);
        missileObj.transform.position = Self.Value.transform.position;

        Attack missileAtk = missileObj.GetComponent<Attack>();
        missileAtk.isFriendlyToPlayer = false;

        MissileController missileController = missileObj.GetComponent<MissileController>();
        if(missileController == null)
        {
			missileController = missileObj.AddComponent<MissileController>();
		}
        missileController.Initialize(mover);
    }


    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

