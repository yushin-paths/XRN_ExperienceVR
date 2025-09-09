using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Rendering;

public class Smoke : MonoBehaviour
{
	public GameObject cameraRig;
	public BoxBlur boxBlur;
	public GameObject mainCamera;
	public Avatar player_avater;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			StartCoroutine(Dying(other));
		}
	}

	private IEnumerator Dying(Collider other)
	{
		cameraRig.SetActive(false);

		var rigManager = other.GetComponent<RiggingManager>();
		var ani = other.GetComponentInChildren<Animator>();
		var rig = other.GetComponentInChildren<RigBuilder>();

		yield return new WaitForSeconds(1f);

		rigManager.enabled = false;
		rig.layers[0].active = false;
		other.transform.position += new Vector3(0, -mainCamera.transform.localPosition.y, 0);
		other.transform.position = new Vector3(other.transform.position.x, -6.843f, other.transform.position.z);


		boxBlur.Blur();


		yield return new WaitForSeconds(4f);

		ani.avatar = player_avater;
		ani.Play("Dying");
	}
}
