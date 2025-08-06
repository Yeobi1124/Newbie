using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float AttackPower;
    private Vector2 InputVector;

    public GameObject Bullet;

    public Slider HP_bar;
    private int HP;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HP = 100;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();

        HP = Mathf.Clamp(HP, 0, 100);
        //HP_bar.value = HP;
    }

    void Move()
    {
        transform.Translate(InputVector * Speed * Time.deltaTime);
    }

    void Fire()
    {

    }

    void OnMove(InputValue value)
    {
        InputVector = value.Get<Vector2>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BulletEnemy")
        {
            HP -= 20;
            collision.gameObject.SetActive(false);
        }
    }
}
