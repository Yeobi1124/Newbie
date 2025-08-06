using UnityEngine;

public class PlayerMuzzle : MonoBehaviour
{
    private Animator Anim;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    void Shoot()
    {
        Anim.SetTrigger("Shoot");
    }

    void SkillStart()
    {
        Anim.SetBool("UseSkill", true);
    }

    void SkillEnd()
    {
        Anim.SetBool("UseSkill", false);
    }
}
