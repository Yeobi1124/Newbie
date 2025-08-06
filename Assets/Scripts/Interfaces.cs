using UnityEngine;

public interface IHittable
{
    /// <summary>
    /// 공격이 플레이어, 적 중 누구의 것인지 구분하고, 유효한 공격인지 판별하는 함수
    /// </summary>
    /// <param name="isFriendlyToPlayer">플레이어, 적의 공격 구분. true면 플레이어, false면 적</param>
    /// <returns>
    /// 공격 대상과 공격이 플레이어/적으로 동일한 진영이면 false, 아니면 true
    /// </returns>
    public bool IsValidTarget(bool isFriendlyToPlayer);
    
    /// <summary>
    /// 데미지를 인자로 받는 피격 시 반응 함수
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="fillEnergyAmount"></param>
    /// <param name="parryable"></param>
    public void Hit(float damage, bool parryable = true);
}

public interface IEnergy
{
    public float Energy { get; set; }
    public float MaxEnergy { get; }
}


public interface IMissileMover
{
    void Initialize(Transform transform, Vector2 targetPosition);
    void Move(float deltaTime);
    bool IsArrived { get; }
}