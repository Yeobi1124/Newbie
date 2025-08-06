using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class SpecialAttackLaser : MonoBehaviour
{
    [SerializeField] private GameObject target;
    
    public float laserSpeed = 1f;

    [SerializeField]
    private bool _active = false;
    private float _progress; // 0 to 1
    
    private LineRenderer _lineRenderer;
    private BoxCollider2D _collider;
    
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        float distance = (transform.position - target.transform.position).magnitude; // °¢µµ °è»ê ÀÏ´Ü ±ÍÂú¾Æ¼­ »­.
        _collider.size = new Vector2(distance * _progress, _collider.size.y);
        _collider.offset = new Vector2(distance * _progress * 0.5f, _collider.offset.y);
    }

    private void LateUpdate()
    {
        if (!_active) return;
        
        _progress += Time.deltaTime * laserSpeed;
        if (_progress > 1) _progress = 1;
        
        Vector3 dir = target.transform.position - transform.position;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, transform.position + _progress * dir);
    }

    public void Init()
    {
        _lineRenderer.positionCount = 0;
        _progress = 0;
        
        _collider.enabled = false;

        _active = false;
    }

    public void Act()
    {
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, transform.position);
        
        _collider.enabled = true;
        
        _active = true;
    }
}