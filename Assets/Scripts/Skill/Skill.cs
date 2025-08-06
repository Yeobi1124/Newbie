using System;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    protected IEnergy _energy;
    
    [Header("Status")]
    [SerializeField] protected float _energyConsumption;
    [SerializeField] protected float _damage;

    protected virtual void Awake()
    {
        if(_energy == null)
            _energy = GetComponent<IEnergy>();
    }

    public abstract bool Use();
}