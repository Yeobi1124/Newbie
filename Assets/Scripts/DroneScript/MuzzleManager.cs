using BehaviorDesigner.Runtime.Tasks.Unity.Timeline;
using UnityEngine;

public class MuzzleManager : MonoBehaviour
{
    Animation animationComponent;
    public AnimationClip animationClip;
    public float timer;
    public float animationDelay;
    void OnEnable()
    {
        //GetComponent<Animator>().enabled = true;
        timer = 0;
        if (animationComponent != null && animationClip != null)
        {
            Debug.Log("IsPlaying!");
            animationComponent.Play(animationClip.name);
        }
    }

}
