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
    public void Init(GameObject player,IMissileMover mover,Transform missileAbove, Transform missileBelow, List<GameObject> backgrounds,GameObject laser,float hp)
    {
        behaviorTree.SetVariableValue("Player", player);
        behaviorTree.SetVariableValue("MissilePosition_Above", missileAbove);
        behaviorTree.SetVariableValue("MissilePosition_Below", missileBelow);
		behaviorTree.SetVariableValue("Backgrounds", backgrounds);
		behaviorTree.SetVariableValue("Laser", laser);
        behaviorTree.SetVariableValue("maxHP", hp);
        HPUpdate(hp);
    }

    public void HPUpdate(float hp)
    {
        behaviorTree.SetVariableValue("curHP", hp);

    }

    
}
