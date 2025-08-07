using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class SpecialAttackLaser : MonoBehaviour
{
    [SerializeField] private GameObject target;
    
    public float laserSpeed = 1f;

    [SerializeField]
    private bool _active = false;
    [SerializeField]
    private float _progress; // 0 to 1
    
    [SerializeField] private float _lineMaxWidth;
    [SerializeField] private float _disappearTime = 0.5f;
    
    private LineRenderer _lineRenderer;
    private BoxCollider2D _collider;
    
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        float distance = 2 * (transform.position - target.transform.position).magnitude; // °¢µµ °è»ê ÀÏ´Ü ±ÍÂú¾Æ¼­ »­.
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
        _collider.size = new Vector2(0.01f, _lineMaxWidth);

        _active = false;
    }

    public void Act()
    {
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, transform.position);
        _lineRenderer.widthCurve = new AnimationCurve(new Keyframe(0, _lineMaxWidth));
        
        _collider.enabled = true;
        
        _active = true;
        
        // AudioManager.Instance.PlaySE(AudioManager.SEType.PlayerLaser);
    }

    public void Stop() => StartCoroutine(Disappear());

    private IEnumerator Disappear()
    {
        float time = 0;

        while (time < _disappearTime)
        {
            time += Time.deltaTime;
            _lineRenderer.widthCurve = new AnimationCurve(new Keyframe(0, _lineMaxWidth * (1 - time/_disappearTime)));
            yield return new WaitForEndOfFrame();
        }

        Init();
    }
}