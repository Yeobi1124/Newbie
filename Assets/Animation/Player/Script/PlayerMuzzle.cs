using UnityEngine;

public class PlayerMuzzle : MonoBehaviour
{
    private Animator Anim;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    public void Shoot()
    {
        Anim.SetTrigger("Shoot");
    }

    public void SkillStart()
    {
        Anim.SetBool("UseSkill", true);
    }

    public void SkillEnd()
    {
        Anim.SetBool("UseSkill", false);
    }
}
