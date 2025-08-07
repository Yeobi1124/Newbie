
using UnityEngine;

public class ObjectManagerTest : MonoBehaviour
{
    [ContextMenu("Summon Player Bullet")]
    public void SummonPlayerBullet()
    {
        GameObject obj = ObjectManager.instance.PullObject("PlayerBullet");
        obj.transform.position = transform.position;
    }
}