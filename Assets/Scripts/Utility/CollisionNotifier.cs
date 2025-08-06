using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollisionNotifier : MonoBehaviour
{
    public event Action<Collision2D> collisionEnter2D;
    public event Action<Collision2D> collisionExit2D;
    public event Action<Collision2D> collisionStay2D;
    
    public event Action<Collider2D> triggerEnter2D;
    public event Action<Collider2D> triggerExit2D;
    public event Action<Collider2D> triggerStay2D;
    
    private void OnCollisionEnter2D(Collision2D collision) => collisionEnter2D?.Invoke(collision);
    private void OnCollisionExit2D(Collision2D collision) => collisionExit2D?.Invoke(collision);
    private void OnCollisionStay2D(Collision2D collision) => collisionStay2D?.Invoke(collision);

    private void OnTriggerEnter2D(Collider2D other) => triggerEnter2D?.Invoke(other);
    private void OnTriggerExit2D(Collider2D other) => triggerExit2D?.Invoke(other);
    private void OnTriggerStay2D(Collider2D other) => triggerStay2D?.Invoke(other);
}