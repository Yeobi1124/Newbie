
using System.Collections;
using UnityEngine;

public class Heal : Skill
{
    [SerializeField] private SpaceShip spaceShip;
    [SerializeField] private float healAmount = 5f;

    [SerializeField] private GameObject healParticle;
    [SerializeField] private float vfxDuration = 2f;

    private Coroutine coroutine;
    
    private IEnumerator ActivateVFX()
    {
        healParticle.SetActive(true);
        
        yield return new WaitForSeconds(vfxDuration);
        
        healParticle.SetActive(false);
        coroutine = null;
    }
    
    public override bool Use()
    {
        if (_energy.Energy < _energyConsumption) return false;
        
        _energy.Energy -= _energyConsumption;
        spaceShip.health += healAmount;

        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(ActivateVFX());
        
        AudioManager.Instance.PlaySE(AudioManager.SEType.PlayerHeal);
        
        return true;
    }
}
