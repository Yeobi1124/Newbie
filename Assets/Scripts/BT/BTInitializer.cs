using UnityEngine;
using Unity.Behavior;
using System.Collections.Generic;

public class BTInitializer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private BehaviorGraphAgent behaviorTree;
    [SerializeField] private IMissileMover mover;
    [SerializeField] private Transform missileAbove;
    [SerializeField] private Transform missileBelow;
    [SerializeField] private List<GameObject> backgrounds;
    [SerializeField] private GameObject laser;
    void Start()
    {
        behaviorTree = GetComponent<BehaviorGraphAgent>();
        behaviorTree.SetVariableValue("Player", player);
        behaviorTree.SetVariableValue("MissilePosition_Above", missileAbove);
        behaviorTree.SetVariableValue("MissilePosition_Below", missileBelow);
		behaviorTree.SetVariableValue("Backgrounds", backgrounds);
		behaviorTree.SetVariableValue("Laser", laser);
	}
    void Update()
    {
        
    }
}
