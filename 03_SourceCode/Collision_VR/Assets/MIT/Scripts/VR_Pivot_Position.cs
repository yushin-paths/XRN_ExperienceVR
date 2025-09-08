using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Pivot_Position : MonoBehaviour
{
	public GameObject main_Camera;
	public GameObject main_Pivot;
	public AnimationCurve curve;
	public float lerpSpeed = 1f;

	// Update is called once per frame
	void Update()
	{
		 Vector3 cameraPos = new Vector3(main_Camera.transform.position.x, main_Pivot.transform.position.y, main_Camera.transform.position.z);
		 Vector3 pivotPos = new Vector3(main_Pivot.transform.position.x, main_Camera.transform.position.y, main_Pivot.transform.position.z);

		main_Pivot.transform.position = cameraPos;
		main_Camera.transform.position = pivotPos;
	}
}
