using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossChargeEffect", story: "[Self] change material to Red", category: "Action", id: "6fc2877d2aa315a2eb629130cebfee64")]
public partial class BossChargeEffectAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    private Material mat;
    private bool isIncreasing;
	private float timer;

    protected override Status OnStart()
    {
        isIncreasing = true;
        timer = 0.0f;
        mat = Self.Value.GetComponent<SpriteRenderer>().material;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (isIncreasing)
        {
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {
                timer = 0.5f;
                isIncreasing = false;
            }
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer = 0f;
                return Status.Success;
            }
        }
        mat.SetFloat("_InnerOutlineGlow", timer*20);
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

