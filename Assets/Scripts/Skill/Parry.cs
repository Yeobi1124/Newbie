using System;
using System.Collections;
using AllIn1SpriteShader;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Parry : Skill
{
    [SerializeField] private Shield shield;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float fillDefaultEnergy = 1f;
    [SerializeField] private float fillEnergyRate = 0.2f;
    [SerializeField] private float fillEnergyMax = 2f;
    [SerializeField, Tooltip("즉사기 판별하기 위해 어느 데미지 이상을 즉사기로 볼 건지")]
    private float damageCut = 999f;

    [ReadOnly, SerializeField]
    private bool isLocked = false;
    
    private Coroutine coroutine;
    private Material material;
    [SerializeField]
    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        material = GetComponent<Renderer>().material;
    }

    public override bool Use()
    {
        if (isLocked == false && _energy.Energy >= _energyConsumption)
            _energy.Energy -= _energyConsumption;
        else return false;
        
        coroutine = StartCoroutine(Parrying());
        
        return true;
    }

    private IEnumerator Parrying()
    {
        // Activate Parry
        isLocked = true;
        shield.gameObject.SetActive(true);
        
        material.EnableKeyword("HOLOGRAM_ON");
        material.EnableKeyword("OUTBASE_ON");
        
        yield return new WaitForSeconds(duration);
        
        // Deactivate Parry
        isLocked = false;
        shield.gameObject.SetActive(false);
        
        material.DisableKeyword("HOLOGRAM_ON");
        material.DisableKeyword("OUTBASE_ON");
    }

    public void ParrySuccess(float damage)
    {
        // Fill Energy
        if (damage < damageCut)
        {
            float fillEnergy = fillDefaultEnergy + fillEnergyRate * damage;
            if(fillEnergy > fillEnergyMax) fillEnergy = fillEnergyMax;
        
            _energy.Energy = _energy.Energy + fillEnergy > _energy.MaxEnergy ? _energy.MaxEnergy : _energy.Energy + fillEnergy;
        }
        else
        {
            _energy.Energy = _energy.MaxEnergy;
        }
        
        // Shield Visual
        animator.SetTrigger("Destroy");
        
        material.DisableKeyword("HOLOGRAM_ON");
        material.DisableKeyword("OUTBASE_ON");
        
        // Stop Coroutine, Unlock Skill
        isLocked = false;
        StopCoroutine(coroutine);
    }
}