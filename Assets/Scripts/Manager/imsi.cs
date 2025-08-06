using UnityEngine;

public class imsi : MonoBehaviour
{
    bool temp;
    DroneAnimation Anim;
    public GameObject drone;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        temp = false;
        Anim = drone.GetComponent<DroneAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            temp = !temp;
            if (temp)
            {
                Anim.OnDead();
            }
            else
            {
                drone.SetActive(true);
            }
        }
    }
}
