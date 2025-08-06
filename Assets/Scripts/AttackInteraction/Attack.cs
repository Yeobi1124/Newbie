using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Attack : MonoBehaviour
{
    public float damage;
    public bool isFriendlyToPlayer;

    protected abstract void OnTriggerEnter2D(Collider2D other);
}
