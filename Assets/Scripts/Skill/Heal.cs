
using UnityEngine;

public class Heal : Skill
{
    [SerializeField] private SpaceShip spaceShip;
    [SerializeField] private float healAmount = 5f;
    
    public override bool Use()
    {
        if (_energy.Energy < _energyConsumption) return false;
        
        _energy.Energy -= _energyConsumption;
        spaceShip.health += healAmount;

        return true;
    }
}
