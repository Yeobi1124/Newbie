
using UnityEngine;

public class Heal : Skill
{
    [SerializeField] private SpaceShip spaceShip;
    [SerializeField] private float healAmount = 5f;
    
    public override void Use()
    {
        if (_energy.Energy < _energyConsumption) return;
        
        _energy.Energy -= _energyConsumption;
        spaceShip.health += healAmount;
    }
}
