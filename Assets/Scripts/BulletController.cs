using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float BulletSpeed;
    [SerializeField]private Vector2 direction;
    
    void Awake()
    {
        if(direction==null)
            direction = Vector2.right;
    }

    public void Init(Vector2 dir)
    {
        direction = dir;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform. rotation.y , angle);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(BulletSpeed * direction.x * Time.deltaTime, BulletSpeed * direction.y * Time.deltaTime, 0 , Space.World);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Border") gameObject.SetActive(false);
    }
}
