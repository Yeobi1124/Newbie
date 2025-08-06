using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossMissilePattern", story: "[Self] attacks [Player] with [missilePosition]", category: "Action", id: "4a46c270467dbd4ab741c800e64aad47")]
public partial class BossMissilePatternAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    [SerializeReference] public BlackboardVariable<Transform> MissilePosition;
    protected override Status OnStart()
    {
        MissileMake();
        return Status.Running;
    }

    private void MissileMake()
    {
        GameObject missileObj = ObjectManager.instance.PullObject("BulletEnemy");
        missileObj.transform.position = Self.Value.transform.position;


        //MissileController missileController = missileObj.GetComponent<MissileController>();
        //missileController.Init(Player.Value.transform, MissilePosition.position);
    }


    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

