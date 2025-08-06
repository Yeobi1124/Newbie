    using System;
using UnityEngine;

public class ChragerInfo : Attack, IHittable
{
    public float OriginalHealth;
    public float Health;
    public float DroneSpeed = 1;
    public int check = 0;
    public bool detect = false;
    public float chargeDamage= 20;
    private bool isEnemy = true; // true: 적, false: 플레이어 편
    private bool isDestroyed = false;
    void Awake()
    {
        gameObject.GetComponent<DroneAnimation>().Anim.enabled = false;
    }
    void Start()
    {
        isDestroyed = false;
        ResetCharger();
        Health = OriginalHealth;
        gameObject.GetComponent<DroneAnimation>().Anim.enabled = false;
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
            Debug.Log("Dead?");
            isDestroyed = true;
            check++;
            Destroyed();
        }
    }
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("Trigger called?");
        if (col.TryGetComponent(out IHittable hittable))
        {
            //Debug.Log("At List HITTABLE");
            if (hittable.IsValidTarget(isFriendlyToPlayer))
            {
                hittable.Hit(chargeDamage);
                Destroyed();
                gameObject.GetComponent<ChargerAMove>().chargeSpeed = 0;
                gameObject.GetComponent<ChargerAMove>().droneSpeed = 0;
                return;
            }
        }
        if (col.gameObject.CompareTag("Border"))
        {
            gameObject.GetComponent<DroneAnimation>().OnDead();
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
        gameObject.GetComponent<DroneAnimation>().OnDead();
        
    }

    public void ResetCharger()
    {
        Health = OriginalHealth;
        isDestroyed = false;
        detect = false;
    }
}

