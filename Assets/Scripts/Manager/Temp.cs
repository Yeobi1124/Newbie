using UnityEngine;

public class Temp : MonoBehaviour
{
    public GameObject muzzle;
    MuzzleFlashAnimation muzzleAnim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        muzzleAnim = muzzle.GetComponent<MuzzleFlashAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            muzzleAnim.Shoot();
        }
    }
}
