using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall_Down : MonoBehaviour
{
	[SerializeField] private GameObject player_hip;
	private BoxBlur blur;
	private Scene_Loader scene_loader;
	[SerializeField] private string scene_name;

	private void Awake()
	{
		blur = GetComponent<BoxBlur>();
		scene_loader = FindObjectOfType<Scene_Loader>();

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player_hip)
			StartCoroutine(Load());
	}

	private IEnumerator Load()
	{
		yield return new WaitForSeconds(2f);

		blur.Blur();

		yield return new WaitForSeconds(blur.lerpTime);

		scene_loader.Scene_Loading(scene_name);
	}
}
