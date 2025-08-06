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
        // ��¡ ������ Ÿ�ֿ̹� ȣ��Ǵ� �Լ� (�μ� �ϳ����� ����)
        Debug.Log("��¡ ��, �߻�!");
    }
}
