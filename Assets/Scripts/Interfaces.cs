using UnityEngine;

public interface IHittable
{
    /// <summary>
    /// ������ �÷��̾�, �� �� ������ ������ �����ϰ�, ��ȿ�� �������� �Ǻ��ϴ� �Լ�
    /// </summary>
    /// <param name="isFriendlyToPlayer">�÷��̾�, ���� ���� ����. true�� �÷��̾�, false�� ��</param>
    /// <returns>
    /// ���� ���� ������ �÷��̾�/������ ������ �����̸� false, �ƴϸ� true
    /// </returns>
    public bool IsValidTarget(bool isFriendlyToPlayer);
    
    /// <summary>
    /// �������� ���ڷ� �޴� �ǰ� �� ���� �Լ�
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