using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;
using UnityEngine.UIElements;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "LargeLaserAttack", story: "[Laser] shoot and [Backgrounds] reset", category: "Action", id: "220bc9d9342aac0aa7a79d476bfed5b6")]
public partial class LargeLaserAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Laser;
    [SerializeReference] public BlackboardVariable<List<GameObject>> Backgrounds;
    private enum State { LaserShooting, LaserOff};
	private State curState;
	private GameObject invisibleBullet; // ������ �и� ������.
    private float timer = 0f;
	private Material laserMaterial;
	private List<Material> backgroundMaterial = new List<Material>();
	protected override Status OnStart()
    {
		timer = 0f;
		curState = State.LaserShooting;
		laserMaterial = Laser.Value.GetComponent<Renderer>().material;

		foreach (var backgroundObj in Backgrounds.Value)
		{
			if (backgroundObj != null)
			{
				var renderer = backgroundObj.GetComponent<Renderer>();
				if (renderer != null)
				{
					backgroundMaterial.Add(renderer.material);
				}
			}
		}

		return Status.Running;
    }

    protected override Status OnUpdate()
	{

		timer += Time.deltaTime;

		if (curState == State.LaserShooting)
		{

			laserMaterial.SetFloat("_GhostColorBoost", Mathf.Lerp(1f, 5f, timer*4));
			
			laserMaterial.SetFloat("_GhostTransparency", Mathf.Lerp(0.75f, 0.3f, timer*4));

			if (timer >= 0.25f)
			{

				invisibleBullet = Laser.Value.transform.GetChild(0).gameObject;
				invisibleBullet.transform.position = new Vector2(0, 0);
				invisibleBullet.SetActive(true);
				Rigidbody2D invisibleBulletRb = invisibleBullet.GetComponent<Rigidbody2D>();
				invisibleBulletRb.linearVelocityX = -50f;

				timer = 0f;
				curState = State.LaserOff;
			}
		}
		else if(curState == State.LaserOff){
			
			laserMaterial.SetFloat("_GhostColorBoost", Mathf.Lerp(5f, 0f, timer));
			laserMaterial.SetFloat("_GhostTransparency", Mathf.Lerp(0.3f, 1f, timer));

			foreach (var backgroundMaterialerial in backgroundMaterial)
			{
				if (backgroundMaterialerial != null)
					backgroundMaterialerial.SetFloat("_Glow", Mathf.Lerp(5, 0.0f, timer));
			}

			if (timer >= 1f)
				return Status.Success;
		}
		Debug.Log(timer);
		return Status.Running;
		
    }

    protected override void OnEnd()
    {
		invisibleBullet.SetActive(false);
		Laser.Value.SetActive(false);
    }
}

