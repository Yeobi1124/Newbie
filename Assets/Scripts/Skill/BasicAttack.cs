using UnityEngine;

public class BasicAttack : Skill
{
    [SerializeField] private GameObject createPoint;
    [SerializeField] private float _bulletSpeed = 5f;
    public override void Use()
    {
        GameObject bullet = ObjectManager.instance.PullObject("BulletPlayer");
        bullet.GetComponent<Attack>().damage = _damage;
        
        bullet.transform.position = createPoint.transform.position;
        // bullet.GetComponent<Rigidbody2D>().linearVelocity = bullet.transform.forward * _bulletSpeed; // forward 백터가 zero vector인 듯?
        bullet.GetComponent<Rigidbody2D>().linearVelocityX = _bulletSpeed;
    }
}