using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float AttackPower;
    private Vector2 InputVector;

    public GameObject Bullet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();

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
}
