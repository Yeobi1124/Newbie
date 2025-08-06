using UnityEngine;
using System.Collections;

public class DroneAnimation : MonoBehaviour
{
    public Animator Anim;
    private SpriteRenderer SpriteRender;
    [SerializeField] private Sprite OriginSprite;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        SpriteRender = GetComponent<SpriteRenderer>();
        OriginSprite = SpriteRender.sprite;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        Anim.enabled = false;
        SpriteRender.sprite = OriginSprite;
    }

    public void OnDead()
    {
        StartCoroutine(DieAnim());
    }

    IEnumerator DieAnim()
    {
        Anim.enabled=true;
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
