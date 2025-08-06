using UnityEngine;
using System.Collections;

public class DroneAnimation : MonoBehaviour
{
    private Animator Anim;
    private SpriteRenderer SpriteRender;
    public Sprite Sprite;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        SpriteRender = GetComponent<SpriteRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        Anim.ResetTrigger("DroneDead");
        SpriteRender.sprite = Sprite;
    }

    public void OnDead()
    {
        StartCoroutine(DieAnim());
    }

    IEnumerator DieAnim()
    {
        Anim.SetTrigger("DroneDead");
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
