using UnityEngine;

public class ChragerInfo : MonoBehaviour, IHittable
{
    public float OriginalHealth;
    public float Health;
    public float DroneSpeed = 1;
    public int ShootNum = 0;
    public int check = 0;
    public bool detect = false;

    private bool isEnemy = true; // true: 적, false: 플레이어 편
    private bool isDestroyed = false;

    void Start()
    {
        ResetCharger();
    }

    void Update()
    {
        // 화면 밖으로 나가면 비활성화
        if (transform.position.x < -16 || transform.position.x > 16 ||
            transform.position.y < -10 || transform.position.y > 10)
        {
            gameObject.SetActive(false);
        }

        // 체력이 0 이하일 때 한 번만 파괴 처리
        if (Health <= 0 && !isDestroyed)
        {
            isDestroyed = true;
            check++;
            Destroyed();
        }
    }

    public void Hit(float damage)
    {
        Health -= damage;
    }

    public bool IsValidTarget(bool isFriendlyToAttacker)
    {
        return isEnemy != isFriendlyToAttacker;
    }

    private void Destroyed()
    {
        gameObject.SetActive(false);
    }

    public void ResetCharger()
    {
        Health = OriginalHealth;
        ShootNum = 0;
        isDestroyed = false;
        detect = false;
    }
}

