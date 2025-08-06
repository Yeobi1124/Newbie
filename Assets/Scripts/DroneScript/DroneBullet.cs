using UnityEngine;

public class DroneBullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float BulletSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(BulletSpeed * Time.deltaTime, 0, 0);
        if (gameObject.transform.position.x <-10) {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Border") gameObject.SetActive(false);
    }
}
