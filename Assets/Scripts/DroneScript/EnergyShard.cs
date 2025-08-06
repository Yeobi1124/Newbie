using System;
using UnityEngine;

public class EnergyShard : MonoBehaviour
{
    public float energyFillAmount = 1f;
    [SerializeField] private float amplitude = 1f;
    [SerializeField] private float period = 1f;
    [SerializeField] private AnimationCurve verticalMove;
    [SerializeField] private float horizontalSpeed = 3f;

    private float time;

    private void Update()
    {
        time += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        transform.localPosition = new Vector3(transform.localPosition.x - horizontalSpeed * Time.fixedDeltaTime, amplitude * verticalMove.Evaluate(time % period));
    }
}
