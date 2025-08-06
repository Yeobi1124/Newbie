using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SpecialAttackDrone : MonoBehaviour
{
    [SerializeField] private GameObject target;
    
    public float laserSpeed = 1f;

    [SerializeField]
    private bool _active = false;
    private float _progress; // 0 to 1
    
    private LineRenderer lineRenderer;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // Test
        Act();
    }

    private void LateUpdate()
    {
        if (!_active) return;
        
        _progress += Time.deltaTime * laserSpeed;
        if (_progress > 1) _progress = 1;
        
        Vector3 dir = target.transform.position - transform.position;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + _progress * dir);
    }

    public void Init()
    {
        lineRenderer.positionCount = 0;
        _progress = 0;

        _active = false;
    }

    public void Act()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
        
        _active = true;
    }
}
