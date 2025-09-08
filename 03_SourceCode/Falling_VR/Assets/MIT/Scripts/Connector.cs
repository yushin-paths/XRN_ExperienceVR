using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Connector : MonoBehaviour
{
	[Header("=== Target ===")]
	public GameObject target;
	public float distance_by_target;

	[Header("Me")]
	private List<MeshRenderer> meshRenderer;
	public Color hover_color = Color.white;
	//public Color unHover_color = Color.white;
	public Color default_color = Color.white;

	public Color placed_color = Color.cyan;

	public bool isGrabbable = false;
	public bool isHover = false;
	public bool placed = false;

	private void Awake()
	{
		meshRenderer = GetComponentsInChildren<MeshRenderer>().ToList();
	}

	void Update()
    {
		float distance = Vector3.Distance(transform.position, target.transform.position);

		if (distance <= distance_by_target)
		{
			if (!placed)
			{
				//foreach (var mesh in meshRenderer) { mesh.material.color = placed_color; }
				target.SetActive(true);

				if (!isGrabbable)
				{
					placed = true;

					transform.parent = null;

					transform.position = target.transform.position;
					transform.rotation = target.transform.rotation;

					target.SetActive(false);
					//foreach (var mesh in meshRenderer) { mesh.material.color = default_color; }
				}
				else
				{
					target.SetActive(true);
				}
			}



			//if (isHover && distance == 0)
			//	foreach (var mesh in meshRenderer) { mesh.material.color = hover_color; }
			//else if (!isHover && distance == 0)
			//	foreach (var mesh in meshRenderer) { mesh.material.color = default_color; }
		}
		//else
		//{
		//	target.SetActive(false);
		//}
		//else if (!isHover)
		//{
		//	foreach (var mesh in meshRenderer) { mesh.material.color = default_color; }
		//}
		//else if (isHover)
		//{
		//	foreach (var mesh in meshRenderer) { mesh.material.color = hover_color; }
		//}
    }

	public void IsGrabbable(bool b)
	{
		placed = false;
		isGrabbable = b;
	}

	public void Hover_Color_Change()
	{
		foreach (var mesh in meshRenderer) { mesh.material.color = hover_color; }
		isHover = true;
	}
	public void UnHover_Color_Change()
	{
		foreach (var mesh in meshRenderer) { mesh.material.color = default_color; }
		isHover = false;
	}
}
