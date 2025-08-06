using UnityEngine;

public class MuzzleFlashAnimation : MonoBehaviour
{
    Animator Anim;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) Shoot();
        if(Input.GetMouseButtonDown(1)) ChargeStart();
    }

    public void Shoot()
    {
        Anim.SetTrigger("Shoot");
    }

    public void ChargeStart()
    {
        Anim.SetTrigger("ChargeShoot");
    }
    public void ChargeEnd()
    {
        // 차징 끝나는 타이밍에 호출되는 함수 (인수 하나까지 가능)
        Debug.Log("차징 끝, 발사!");
    }
}
