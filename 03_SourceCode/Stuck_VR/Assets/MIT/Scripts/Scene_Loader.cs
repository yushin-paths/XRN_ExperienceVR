using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Loader : MonoBehaviour
{
	private OVRScreenFade fade;

	private void Awake()
	{
		fade = FindObjectOfType<OVRScreenFade>();
	}

	public void Scene_Loading(string str)
	{
		StartCoroutine(Scene_Load(str));
	}

	private IEnumerator Scene_Load(string str)
	{

		fade.FadeOut();

		yield return new WaitForSeconds(fade.fadeTime + 1.5f);

		SceneManager.LoadScene(str);

		yield return null;
	}
}
