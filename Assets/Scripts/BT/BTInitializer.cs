using UnityEngine;
using Unity.Behavior;

public class BTInitializer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private BehaviorGraphAgent behaviorTree;
    [SerializeField] private IMissileMover mover;
    [SerializeField] private Transform missileAbove;
    [SerializeField] private Transform missileBelow;
    void Start()
    {
        behaviorTree = GetComponent<BehaviorGraphAgent>();
        behaviorTree.SetVariableValue("Player", player);
        behaviorTree.SetVariableValue("MissilePosition_Above", missileAbove);
        behaviorTree.SetVariableValue("MissilePosition_Below", missileBelow);
        //mover ³Ö±â
    }
    void Update()
    {
        
    }
}
