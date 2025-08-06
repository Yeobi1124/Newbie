using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class SpecialAttack : Skill
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private SpecialAttackManage manage;
    
    public override bool Use()
    {
        if (manage.isDone == false || _energy.Energy < _energyConsumption) return false;
        _energy.Energy -= _energyConsumption;

        Debug.Log("Special Attack");
        
        director.time = director.initialTime;
        director.Play();
        return true;
    }
}