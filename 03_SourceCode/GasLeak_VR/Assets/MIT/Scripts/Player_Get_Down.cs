using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_Get_Down : MonoBehaviour
{
	public GameObject player_rig;
	public GameObject teleport_target;

	public GameObject dolly_track;
	public GameObject dolly_Camera;

	public Transform pos;

	public TextMeshProUGUI text;

	public bool safety = false;

	public string Loadscene_name;
	private OVRScreenFade fade;
	private Scene_Loader scene_Loader;

	void Update()
	{
		fade = FindObjectOfType<OVRScreenFade>();
		scene_Loader = GetComponent<Scene_Loader>();

		if (text != null)
			text.text = $"{pos.position.x}, {pos.position.y}, {pos.position.z}";
	}

	public void GetingDown()
	{
		//a = true;
		StartCoroutine(DownDelay());
	}

	private IEnumerator DownDelay()
	{
		//player_rig.transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(false);
		player_rig.SetActive(false);
		dolly_track.SetActive(true);
		dolly_Camera.SetActive(true);

		yield return new WaitForSeconds(6f);

		player_rig.transform.position = teleport_target.transform.position;
		Physics.SyncTransforms();
		player_rig.transform.eulerAngles = new Vector3(0, teleport_target.transform.eulerAngles.y, 0);
		player_rig.SetActive(true);

		dolly_track.gameObject.SetActive(false);
		dolly_Camera.gameObject.SetActive(false);

		yield return new WaitForSeconds(10f);

		if (!safety)
			scene_Loader.Scene_Loading(Loadscene_name);
		//else
		//{
		//	fade.FadeOut();

		//	yield return new WaitForSeconds(fade.fadeTime + 1f);

		//	new Exit_Program().Exit();
		//}

		//player_rig.SetActive(false);
	}
}
