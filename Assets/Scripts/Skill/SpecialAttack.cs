using System.Collections;
using UnityEngine;

public class SpecialAttack : Skill
{
    [SerializeField] private SpecialAttackDrone[] drones;
    [SerializeField] private SpecialAttackLaser laser;
    
    [SerializeField] private float duration = 7f;
    
    [SerializeField] private float attackActivateTime = 1f;

    private bool isLocked = false;
    
    public override bool Use()
    {
        if (isLocked == true || _energy.Energy < _energyConsumption) return false;
        _energy.Energy -= _energyConsumption;

        StartCoroutine(Activate());
        return true;
    }

    private IEnumerator Activate()
    {
        isLocked = true;

        foreach (var drone in drones)
        {
            drone.gameObject.SetActive(true);
            drone.Act();
        }
        
        yield return new WaitForSeconds(attackActivateTime);
        
        laser.gameObject.SetActive(true);
        laser.Act();
        
        yield return new WaitForSeconds(duration);
        
        isLocked = false;
        
        foreach (var drone in drones)
        {
            drone.Init();
            drone.gameObject.SetActive(false);
        }
        
        laser.Init();
        laser.gameObject.SetActive(false);
    }
}