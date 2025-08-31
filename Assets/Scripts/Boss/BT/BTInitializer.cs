using UnityEngine;
using Unity.Behavior;
using System.Collections.Generic;

public class BTInitializer
{
    private BehaviorGraphAgent behaviorTree;

    public BTInitializer(BehaviorGraphAgent behaviorTree)
    {
        this.behaviorTree = behaviorTree;
    }
    public void Init(GameObject player,IMissileMover mover, List<GameObject> backgrounds,GameObject laser,float hp,List<GameObject> WayPoints,Transform laserTransform,Transform deathPos)
    {
        behaviorTree.SetVariableValue("Player", player);
		behaviorTree.SetVariableValue("Backgrounds", backgrounds);
		behaviorTree.SetVariableValue("Laser", laser);
        behaviorTree.SetVariableValue("maxHP", hp);
        HPUpdate(hp);
        behaviorTree.SetVariableValue("WayPoints", WayPoints);
        behaviorTree.SetVariableValue("LaserTransform", laserTransform);
        behaviorTree.SetVariableValue("DeathPoint", deathPos);
    }

    public void HPUpdate(float hp)
    {
        behaviorTree.SetVariableValue("curHP", hp);

    }

    public void DestroyBehavior()
    {
        behaviorTree.SetVariableValue("curHP", 0.0);
    }

    public void BTDisable()
    {
        behaviorTree.enabled = false;
    }

    
}
