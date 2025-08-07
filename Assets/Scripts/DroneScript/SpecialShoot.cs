using UnityEngine;

public class SpecialShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public GameObject effectPrefab;
    
    public float ShootDelay;

    private float shootCounter = 0f;
    private float timer = 0f;
    private bool effectSpawned = false;
    private bool bulletShot = false;
    private GameObject effectInstance;

    void Update()
    {
        if (shootCounter >= ShootDelay)
        {
            if (!effectSpawned)
            {
                effectInstance = Instantiate(effectPrefab, bulletPos.position, Quaternion.Euler(0, 0, 180));
                effectSpawned = true;
            }

            if (gameObject.GetComponent<ElliteInfo>().isDestroyed == true)
            {
                Destroy(effectInstance);
            }

            timer += Time.deltaTime;

            // Make effect follow the target
            if (effectInstance != null && bulletPos != null)
            {
                effectInstance.transform.position = bulletPos.position; // + optional offset
            }

            if (timer > 0.6f && !bulletShot)
            {
                Instantiate(bullet, bulletPos.position, Quaternion.identity);
                bulletShot = true;
            }

            if (timer > 1f)
            {
                if (effectInstance != null)
                    Destroy(effectInstance);

                // Reset state for next shoot
                timer = 0f;
                shootCounter = 0f;
                effectSpawned = false;
                bulletShot = false;
            }
        }

        shootCounter += Time.deltaTime;
    }
}
