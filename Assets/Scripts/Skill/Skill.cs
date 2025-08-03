using System;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] private IEnergy _energy;
    
    [Header("Status")]
    [SerializeField] private float _energyConsumption;
    [SerializeField] private float _damage;

    protected virtual void Awake()
    {
        if(_energy == null)
            _energy = GetComponent<IEnergy>();
    }

    public abstract void Use();
}