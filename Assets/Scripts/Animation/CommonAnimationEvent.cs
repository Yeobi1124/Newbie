using UnityEngine;

/// <summary>
/// This class is for Animation Event
/// </summary>
public class CommonAnimationEvent : MonoBehaviour
{
    public void SetActive(int active) => gameObject.SetActive(active != 0);
}
