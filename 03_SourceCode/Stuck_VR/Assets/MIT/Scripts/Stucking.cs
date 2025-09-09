using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stucking : MonoBehaviour
{
	public BoxBlur blur;

	public GameObject camera_rig;
	public GameObject player_model;
	public Animator ani;
	public RiggingManager rigManager;

	public Vector3 offsetPos = new Vector3() { x = -8.943f, y = 0.087f, z = -4.375f };
	public Vector3 offsetRot = new Vector3() { x = 0, y = -90, z = -0 };

	public OVRScreenFade fade;
	private Scene_Loader scene_Loader;
	public string load_Scene_name;

	public bool safety;

	private void Awake()
	{
		fade = FindObjectOfType<OVRScreenFade>();
		scene_Loader = GetComponent<Scene_Loader>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "Hand_Trigger" && !safety)
		{
			camera_rig.SetActive(false);
			rigManager.enabled = false;

			player_model.transform.position = offsetPos;
			player_model.transform.eulerAngles = offsetRot;

			ani.Play("Dying");

			StartCoroutine(Blur_Delay());
		}
	}

	private IEnumerator Blur_Delay()
	{
		yield return new WaitForSeconds(0.4f);

		blur.Blur();

		yield return new WaitForSeconds(5f);

		scene_Loader.Scene_Loading(load_Scene_name);
	}

	public void Exit()
	{
		StartCoroutine(Exit_());
	}

	private IEnumerator Exit_()
	{
		yield return new WaitForSeconds(5f);

		fade.FadeOut();

		yield return new WaitForSeconds(fade.fadeTime + 1.5f);

		new Exit_Program().Exit_();
	}
}
