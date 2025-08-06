using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceShip : MonoBehaviour, IHittable, IEnergy
{
    [FormerlySerializedAs("maxVerticalSpeed")]
    [Header("Control")]
    [Header("Vertical")]
    public float verticalMaxSpeed = 2f;
    public AnimationCurve verticalSpeedCurve; // 0 ~ 0.5 : Accelerate, 0.5 ~ 1 : Decelerate
    public float timeToVerticalMaxSpeed = 0.2f;
    public float timeToVerticalStop = 0.1f;
    
    [ReadOnly, SerializeField]
    private float _verticalInputDuration = 0f;
    [ReadOnly, SerializeField]
    private float _noVerticalInputDuration = 0f;
    
    [Header("Horizontal")]
    public float forwardMaxSpeed = 3f;
    public AnimationCurve forwardSpeedCurve;
    public float timeToForwardMaxSpeed = 0.2f;
    public float timeToForwardStop = 0.1f;
    
    public float backwardMaxSpeed = 1f;
    public AnimationCurve backwardSpeedCurve;
    public float timeToBackwardMaxSpeed = 0.2f;
    public float timeToBackwardStop = 0.1f;
    
    [ReadOnly, SerializeField]
    private float _horizontalInputDuration = 0f;
    [ReadOnly, SerializeField]
    private float _noHorizontalInputDuration = 0f;

    [Header("Status")]
    public float health = 20f;
    public float maxHealth = 20f;
    
    [SerializeField]
    private float energy = 0;
    [SerializeField]
    private float maxEnergy = 7;

    public bool isFriendlyToPlayer = true;
    
    public float Energy
    {
        get => energy;
        set => energy = value > maxEnergy ? maxEnergy : value;
    }
    public float MaxEnergy
    {
        get => maxEnergy;
    }

    private Vector2 _moveDir;
    private Rigidbody2D _rb;
    private Animator _animator;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // Move
        float dirX = _moveDir.x;
        float dirY = _moveDir.y;
        
        // Horizontal Move
        if (dirX > 0)
        {
            _rb.linearVelocityX = forwardMaxSpeed * forwardSpeedCurve.Evaluate(MathF.Min(_horizontalInputDuration / timeToForwardMaxSpeed, 0.5f));
            _horizontalInputDuration += Time.fixedDeltaTime;
            _noHorizontalInputDuration = 0;
        }
        else if (dirX < 0)
        {
            _rb.linearVelocityX = -backwardMaxSpeed * backwardSpeedCurve.Evaluate(MathF.Min(_horizontalInputDuration / timeToBackwardMaxSpeed, 0.5f));
            _horizontalInputDuration += Time.fixedDeltaTime;
            _noHorizontalInputDuration = 0;
        }
        else // dirX == 0
        {
            float speedX = 0f;
            
            if (_rb.linearVelocityX > 0) // forward
                speedX = forwardMaxSpeed * forwardSpeedCurve.Evaluate(MathF.Min(_noHorizontalInputDuration / timeToForwardStop + 0.5f, 1));
            else if (_rb.linearVelocityX < 0) // backward
                speedX = -backwardMaxSpeed * backwardSpeedCurve.Evaluate(MathF.Min(_noHorizontalInputDuration / timeToBackwardStop + 0.5f, 1));

            _rb.linearVelocityX = speedX;
            _horizontalInputDuration = 0;
            _noHorizontalInputDuration += Time.fixedDeltaTime;
        }
        
        // Vertical Move
        if (dirY != 0)
        {
            _rb.linearVelocityY = (dirY < 0 ? -1 : 1) * verticalMaxSpeed * verticalSpeedCurve.Evaluate(MathF.Min(_verticalInputDuration/timeToVerticalMaxSpeed, 0.5f));
            _verticalInputDuration += Time.fixedDeltaTime;
            _noVerticalInputDuration = 0;
        }
        else // dirY == 0
        {
            _rb.linearVelocityY = (_rb.linearVelocityY < 0 ? -1 : 1) * verticalMaxSpeed * verticalSpeedCurve.Evaluate(MathF.Min(_noVerticalInputDuration/timeToVerticalStop + 0.5f, 1f));
            _verticalInputDuration = 0;
            _noVerticalInputDuration += Time.fixedDeltaTime;
        }
    }

    public void Move(Vector2 dir) => _moveDir = dir;

    public bool IsValidTarget(bool isFriendlyToPlayer) => isFriendlyToPlayer != this.isFriendlyToPlayer;

    public void Hit(float damage, bool parryable = true)
    {
        health -= damage;

        if (health <= 0)
        {
            _animator.SetTrigger("Dead");
        }
        else
        {
            _animator.SetTrigger("Hit");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shard"))
        {
            EnergyShard energyShard = other.GetComponent<EnergyShard>();
            
            Energy += energyShard.energyFillAmount;
            other.gameObject.SetActive(false);
        }
    }
}
