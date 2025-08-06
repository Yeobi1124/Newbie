using UnityEngine;

public class MissileController : MonoBehaviour
{
    private IMissileMover mover;

    public void Initialize(IMissileMover mover)
    {
        this.mover = mover;
        mover.Initialize(transform,new Vector2(0,0));
    }

    private void Update()
    {
        mover?.Move(Time.deltaTime);
    }
}
