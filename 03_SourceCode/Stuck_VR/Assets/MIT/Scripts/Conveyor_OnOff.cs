using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Conveyor_OnOff : MonoBehaviour
{
	public bool isOn = false;
	public bool isGrab = false;

	public MeshRenderer meshRenderer;

	[SerializeField] private Material off_materail;
	[SerializeField] private Material on_materail;
	[SerializeField] private GrabInteractable key_grab;
	[SerializeField] private GameObject key_hole;
	[SerializeField] private GameObject safe_cap;

	[SerializeField] private List<Roller> rollers;

	private void Awake()
	{
		rollers = FindObjectsOfType<Roller>().ToList();
	}

	void Update()
	{
		//if (transform.localEulerAngles.z <= 37.5f && transform.localEulerAngles.z != 0)
		//{
		//	//mpb.SetInt("_OPERATION_SET", 1);
		//	meshRenderer.material = on_materail;
		//	isOn = true;
		//	//stucking.SetActive(true);
		//	if (!isGrab)
		//		transform.localEulerAngles = new Vector3(0, 0, 0);
		//}
		//else if (transform.localEulerAngles.z > 37.5f && transform.localEulerAngles.z != 75)
		if (transform.localEulerAngles.z > 37.5f && transform.localEulerAngles.z != 75)
		{
			//mpb.SetInt("_OPERATION_SET", 0);
			meshRenderer.material = off_materail;
			isOn = false;
			foreach (var roller in rollers)
			{
				roller.enabled = false;
			}
			//stucking.SetActive(false);
			if (!isGrab)
				transform.localEulerAngles = new Vector3(0, 0, 75);
		}
	}

	public void Grabbable(bool grabbable)
	{
		isGrab = grabbable;

		if (!isOn)
		{
			gameObject.SetActive(false);
			key_hole.SetActive(true);
			safe_cap.SetActive(true);
		}
	}
}
