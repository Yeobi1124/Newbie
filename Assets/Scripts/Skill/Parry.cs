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
    [SerializeField, Tooltip("���� �Ǻ��ϱ� ���� ��� ������ �̻��� ����� �� ����")]
    private float damageCut = 999f;

    [ReadOnly, SerializeField]
    private bool isLocked = false;
    
    [SerializeField]
    public Animator animator;

    private Coroutine coroutine;
    private Material material;
    

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
        animator.SetBool("IsActivate", true);
        
        AudioManager.Instance.PlaySE(AudioManager.SEType.PlayerParry);
        
        yield return new WaitForSeconds(duration);
        
        // Deactivate Parry
        isLocked = false;
        shield.gameObject.SetActive(false);
        animator.SetBool("IsActivate", false);
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
        animator.SetBool("IsActivate", false);
        
        // SFX
        AudioManager.Instance.PlaySE(AudioManager.SEType.PlayerParrySuccess);
        
        // Stop Coroutine, Unlock Skill
        isLocked = false;
        StopCoroutine(coroutine);
    }
}