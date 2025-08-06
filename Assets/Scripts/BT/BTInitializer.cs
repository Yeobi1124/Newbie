using UnityEngine;
using Unity.Behavior;

public class BTInitializer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private BehaviorGraphAgent behaviorTree;
    void Start()
    {
        behaviorTree = GetComponent<BehaviorGraphAgent>();
        behaviorTree.SetVariableValue("Player", player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
