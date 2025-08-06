using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BulletBorder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") == true)
        {
            other.gameObject.SetActive(false);
        }
    }
}
