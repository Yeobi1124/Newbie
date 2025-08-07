
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeavyAttack : Skill
{
    [SerializeField] private GameObject createPoint;
    [SerializeField] private float _bulletSpeed = 5f;

    public override bool Use()
    {
        if(SceneManager.GetActiveScene().name == "StartScene")
        {
            GameObject Missile = Instantiate(gameObject.transform.Find("Missile").gameObject);
            Missile.transform.position = createPoint.transform.position;
            Missile.SetActive(true);
            Missile.GetComponent<Rigidbody2D>().linearVelocityX = _bulletSpeed;
            return true;
        }

        if (_energy.Energy < _energyConsumption) return false;
        
        _energy.Energy -= _energyConsumption;
        
        GameObject bullet = ObjectManager.instance.PullObject("DroneShooter");
        Attack attack = bullet.GetComponent<Attack>();
        attack.damage = _damage;
        attack.isFriendlyToPlayer = true;
        
        bullet.transform.position = createPoint.transform.position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.GetComponent<Rigidbody2D>().linearVelocityX = _bulletSpeed;
        
        AudioManager.Instance.PlaySE(AudioManager.SEType.PlayerMissile);

        return true;
    }
}