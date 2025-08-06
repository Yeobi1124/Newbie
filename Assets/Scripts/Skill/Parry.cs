using System;
using System.Collections;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Parry : Skill
{
    [SerializeField] private CollisionNotifier shield;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float fillEnergyWhenSuccess = 2f;

    [ReadOnly, SerializeField]
    private bool isLocked = false;
    
    private Coroutine coroutine;
    
    public override void Use()
    {
        if (isLocked == false && _energy.Energy >= _energyConsumption)
            _energy.Energy -= _energyConsumption;
        else return;
        
        coroutine = StartCoroutine(Parrying());
    }

    private void OnEnable()
    {
        shield.triggerEnter2D += ParrySuccess;
    }
    private void OnDisable()
    {
        shield.triggerEnter2D -= ParrySuccess;
    }

    private IEnumerator Parrying()
    {
        // Activate Parry
        isLocked = true;
        shield.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(duration);
        
        // Deactivate Parry
        isLocked = false;
        shield.gameObject.SetActive(false);
    }

    private void ParrySuccess(Collider2D other)
    {
        // Temp Code
        if (other.CompareTag("BulletEnemy") == false) return;
        
        // Fill Energy
        _energy.Energy = _energy.Energy + fillEnergyWhenSuccess > _energy.MaxEnergy ? _energy.MaxEnergy : _energy.Energy + fillEnergyWhenSuccess;
        
        // Shield Visual
        Animator shieldAnimator = shield.gameObject.GetComponent<Animator>();
        shieldAnimator.SetTrigger("Destroy");
        
        // Stop Coroutine, Unlock Skill
        isLocked = false;
        StopCoroutine(coroutine);
        
        // Todo : Remove Bullet
    }
}