using UnityEngine;

public class ElliteInfo : Attack, IHittable
{
    public float OriginalHealth=200;
    public float Health;
    public float OriginalSpeed = 3;
    public float DroneSpeed;
    public int ShootNum = 0;
    public bool Shootable = false;
    public bool isFriendly = false;
    public bool isDestroyed = false;
    public float chargeDamage = 20;
    public bool shardGive;
    void Start()
    {
        ResetDrone();
        DroneSpeed = OriginalSpeed;
    }

    void Update()
    {
        if (transform.position.x < -16 || transform.position.x > 16 || transform.position.y < -10 || transform.position.y > 10)
        {
            Destroyed();
        }

        if (Health <= 0)
        {
            if (!shardGive)
            {
                shardGive = true;
                Vector3 shardPoint = gameObject.transform.position;
                Destroyed();
                for (int i = -1; i < 2; i++)
                {
                    GameObject shard = DroneObjectManager.Instance.PullObject("Shard");
                    shard.transform.position = shardPoint;
                    shard.transform.Translate(i, 0, 0);
                }
            }
        }
    }

    public void Hit(float damage, bool parryable = true)
    {
        Health -= damage;
    }

    public bool IsValidTarget(bool isFriendlyToAttacker)
    {
        return isFriendly != isFriendlyToAttacker;
    }


    public void ResetDrone()
    {
        Shootable = true;
        Health = OriginalHealth;
        ShootNum = 0;
        isDestroyed = false;
        shardGive = false;
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("Trigger called?");
        if (col.TryGetComponent(out IHittable hittable))
        {
            //Debug.Log("At List HITTABLE");
            if (hittable.IsValidTarget(isFriendlyToPlayer))
            {
                hittable.Hit(chargeDamage,false);
                DroneSpeed = 0;
            }
        }
        if (col.gameObject.CompareTag("Border"))
        {
            gameObject.GetComponent<DroneAnimation>().OnDead();
        }
    }

    public void Destroyed()
    {
        isDestroyed = true;
        Shootable = false;
        //Debug.Log($"NONE Shootable: {gameObject.GetComponent<DroneInfo>().Shootable}");
        gameObject.GetComponent<DroneAnimation>().OnDead();
        gameObject.GetComponent<ElliteAmove>().droneSpeed = 0;
        gameObject.GetComponent<ElliteAmove>().moveSpeed = 0;
        //GameObject shard = DroneObjectManager.Instance.PullObject("EnergyShard");
    }

}
