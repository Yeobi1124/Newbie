using System;
using UnityEngine;

public class SpecialAttackManage : MonoBehaviour
{
    public bool isDone = true;
    public event Action OnDone;

    public void SetDone(bool isDone)
    {
        this.isDone = isDone;
        if (isDone) OnDone?.Invoke();
    }

    public void PlaySE()
    {
        AudioManager.Instance.PlaySE(AudioManager.SEType.PlayerLaser);
    }
}
