using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GlowBackground", story: "[Background] and [Laser] glows", category: "Action", id: "f4d1fea8cc457b83e7ec3fc46fe66057")]
public partial class GlowBackgroundAction : Action
{
    [SerializeReference] public BlackboardVariable<List<GameObject>> Background;
    [SerializeReference] public BlackboardVariable<GameObject> Laser;
	private List<Material> backgroundMaterial = new List<Material>();
	private bool isIncreasing;
	private float timer;
	private Material laserMaterial;
	private GameObject invisibleBullet; // 레이저 패링 판정용.
	protected override Status OnStart()
	{
		isIncreasing = true;
		timer = 0.0f;
		foreach (var backgroundObj in Background.Value)
		{
			if (backgroundObj != null)
			{
				var renderer = backgroundObj.GetComponent<Renderer>();
				if (renderer != null)
				{
					backgroundMaterial.Add(renderer.material);
					renderer.material.SetFloat("_Glow", 0);
				}
			}
		}
		Laser.Value.SetActive(true);
		laserMaterial =	Laser.Value.GetComponent<Renderer>().material;
		laserMaterial.SetFloat("_GhostTransparency", 1);
		laserMaterial.SetFloat("_GhostColorBoost", 0);

		invisibleBullet = Laser.Value.transform.GetChild(0).gameObject;
		invisibleBullet.SetActive(false);

		return Status.Running;
	}

	protected override Status OnUpdate()
	{
		if (isIncreasing)
		{
			timer += Time.deltaTime;


			laserMaterial.SetFloat("_GhostColorBoost",Mathf.Lerp(0,1,timer/2f));
			laserMaterial.SetFloat("_GhostTransparency", Mathf.Lerp(1,0.75f,timer/2f));

			foreach (var backgroundMaterialerial in backgroundMaterial)
			{
				if (backgroundMaterialerial != null)
					backgroundMaterialerial.SetFloat("_Glow", 5*timer);
			}

			if (timer >= 2f)
			{
				return Status.Success;
			}
		}
		return Status.Running;	
	}

    protected override void OnEnd()
    {
    }
}

