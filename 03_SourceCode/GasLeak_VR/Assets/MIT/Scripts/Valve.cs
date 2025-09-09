using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valve : MonoBehaviour
{
	public GameObject gass;

	private OVRScreenFade fade;
	private bool grab;

	private bool off;

	private void Awake()
	{
		fade = FindObjectOfType<OVRScreenFade>();
	}

	void Update()
    {
		if (!grab && transform.localEulerAngles.x >= 85 && !off)
		{
			transform.localEulerAngles = new Vector3(90, 0, 0);
			GetComponent<MeshCollider>().enabled = false;
			gass.SetActive(false);

			StartCoroutine(Exit_());

			off = true;
		}
    }

	public void Grab(bool isGrab)
	{
		grab = isGrab;
	}

	private IEnumerator Exit_()
	{
		yield return new WaitForSeconds(5f);

		fade.FadeOut();

		yield return new WaitForSeconds(fade.fadeTime + 1.5f);

		new Exit_Program().Exit();
	}
}
