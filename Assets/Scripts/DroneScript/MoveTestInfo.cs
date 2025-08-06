using UnityEngine;

public class MoveTestInfo : MonoBehaviour
{
    public float Health = 3;
    public float DroneSpeed = 1;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if (gameObject.transform.position.x < -13 || gameObject.transform.position.x > 13 || gameObject.transform.position.y < -8 || gameObject.transform.position.y > 8)
        // {
        //     gameObject.SetActive(false);
        // }

        if (Health <= 0)
        {
            Destroyed();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "border") gameObject.SetActive(false);
    }


    //파괴시 가동
    private void Destroyed()
    {
        gameObject.SetActive(false);
    }
}
