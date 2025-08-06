using UnityEngine;

public interface IHittable
{
    public void Hit(float damage); // void or return hit object's info
}

public interface IEnergy
{
    public float Energy { get; set; }
    public float MaxEnergy { get; }
}