using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceShip : MonoBehaviour, IHittable, IEnergy
{
    [Header("Control")]
    public float verticalSpeed = 2f;
    public AnimationCurve verticalSpeedCurve; // 0 ~ 0.5 : Accelerate, 0.5 ~ 1 : Decelerate
    
    public float forwardSpeed = 3f;
    public float backwardSpeed = 1f;

    [Header("Status")]
    public float health = 20f;
    public float maxHealth = 20f;
    
    [SerializeField]
    private float energy = 0;
    [SerializeField]
    private float maxEnergy = 7;
    
    public float Energy
    {
        get => energy;
        set => energy = value > maxEnergy ? maxEnergy : value;
    }
    public float MaxEnergy
    {
        get => maxEnergy;
    }
    
    
    public void MoveVertical(float dir){}
    public void MoveForward(){}
    public void MoveBackward(){}

    public void Hit(float damage){}
}
