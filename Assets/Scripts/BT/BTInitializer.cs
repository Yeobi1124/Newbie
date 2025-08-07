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
    public void Init(GameObject player,IMissileMover mover,Transform missileAbove, Transform missileBelow, List<GameObject> backgrounds,GameObject laser,float hp,List<GameObject> WayPoints,Transform laserTransform)
    {
        behaviorTree.SetVariableValue("Player", player);
        behaviorTree.SetVariableValue("MissilePosition_Above", missileAbove);
        behaviorTree.SetVariableValue("MissilePosition_Below", missileBelow);
		behaviorTree.SetVariableValue("Backgrounds", backgrounds);
		behaviorTree.SetVariableValue("Laser", laser);
        behaviorTree.SetVariableValue("maxHP", hp);
        HPUpdate(hp);
        behaviorTree.SetVariableValue("WayPoints", WayPoints);
        behaviorTree.SetVariableValue("LaserTransform", laserTransform);
    }

    public void HPUpdate(float hp)
    {
        behaviorTree.SetVariableValue("curHP", hp);

    }

    
}
