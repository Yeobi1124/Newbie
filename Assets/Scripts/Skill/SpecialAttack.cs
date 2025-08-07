using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class SpecialAttack : Skill
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private SpecialAttackManage manage;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        manage.OnDone += () => _animator.SetBool("UseSkill", false);
    }

    public override bool Use()
    {
        if (manage.isDone == false || _energy.Energy < _energyConsumption) return false;
        _energy.Energy -= _energyConsumption;
        
        director.time = director.initialTime;
        director.Play();
        
        _animator.SetBool("UseSkill", true);
        
        return true;
    }
}