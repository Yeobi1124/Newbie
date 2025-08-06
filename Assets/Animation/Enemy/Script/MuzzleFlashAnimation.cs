using UnityEngine;

public class MuzzleFlashAnimation : MonoBehaviour
{
    Animator Anim;
    public bool charging;
    private void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    public void Shoot()
    {
        transform.localScale = Vector3.one * 0.2f;
        Anim.SetTrigger("Shoot");
    }

    public void ChargeStart()
    {
        transform.localScale = Vector3.one;
        Anim.SetTrigger("ChargeShoot");
        charging = true;
    }
    public void ChargeEnd()
    {
        transform.localScale = Vector3.one * 0.2f;
        charging = false;
    }
}
