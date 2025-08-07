using UnityEngine;

public class BulletCopy : MonoBehaviour
{
    public float delay = 2f;
    private float time;
    private GameObject bullet;

    private void Start()
    {
        time = delay;
        bullet = gameObject.transform.Find("DroneBullet").gameObject;
    }

    void Update()
    {
        if (time < 0)
        {
            GameObject newbullet = Instantiate(bullet, transform.position, Quaternion.identity);
            newbullet.SetActive(true);
            time = delay;
        }
        time -= Time.deltaTime;
    }
}
