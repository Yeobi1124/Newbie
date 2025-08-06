using UnityEngine;

public class DroneInfo : MonoBehaviour, IHittable
{
    public float OriginalHealth;
    public float Health;
    public float DroneSpeed = 1;
    public int ShootNum = 0;
    public bool Shootable = true;
    public bool isFriendly = false; // true: 플레이어 편, false: 적군
    private bool isDestroyed = false;

    void Start()
    {
        ResetDrone();
    }

    void Update()
    {
        // 화면 밖으로 나가면 비활성화
        if (transform.position.x < -16 || transform.position.x > 16 ||
            transform.position.y < -10 || transform.position.y > 10)
        {
            gameObject.SetActive(false);
        }

        // 체력이 0 이하면 비활성화
        if (Health <= 0 && !isDestroyed)
        {
            isDestroyed = true;
            Destroyed();
        }
    }

    public void Hit(float damage)
    {
        Health -= damage;
    }
    
    public bool IsValidTarget(bool isFriendlyToAttacker)
    {
        return isFriendly != isFriendlyToAttacker;
    }

    private void Destroyed()
    {
        gameObject.SetActive(false);
    }

    public void ResetDrone()
    {
        Shootable = true;
        Health = OriginalHealth;
        ShootNum = 0;
        isDestroyed = false;
    }
}