
using UnityEngine;

public class HeavyAttack : Skill
{
    [SerializeField] private GameObject createPoint;
    [SerializeField] private float _bulletSpeed = 5f;
    public override bool Use()
    {
        if (_energy.Energy < _energyConsumption) return false;
        
        _energy.Energy -= _energyConsumption;
        
        GameObject bullet = ObjectManager.instance.PullObject("DroneShooter");
        Attack attack = bullet.GetComponent<Attack>();
        attack.damage = _damage;
        attack.isFriendlyToPlayer = true;
        
        bullet.transform.position = createPoint.transform.position;
        bullet.GetComponent<Rigidbody2D>().linearVelocityX = _bulletSpeed;

        return true;
    }
}