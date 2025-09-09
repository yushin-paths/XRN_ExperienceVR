using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Vector3_Offset
{
	public Vector3 _position;
	public Vector3 _rotation;
}

public class RiggingManager : MonoBehaviour
{
	public Transform leftHandIK;
	public Transform rightHandIK;
	public Transform headIK;

	public Transform leftHandController;
	public Transform rightHandController;
	public Transform hmd;

	[Tooltip("Left Offset")] public Vector3_Offset leftOffset;
	[Tooltip("Right Offset")] public Vector3_Offset rightOffset;
	[Tooltip("Head Offset")] public Vector3_Offset headOffset;

	public float smoothValue = 0.1f;
	public float modelHeight = 1.65f;

	private void LateUpdate()
	{
		MappingHandTransform(leftHandIK, leftHandController, true);
		MappingHandTransform(rightHandIK, rightHandController, false);
		MappingBodyTransform(headIK, hmd);
		MappingHeadTransform(headIK, hmd);
	}

	private void MappingHandTransform(Transform ik, Transform controller, bool isLeft)
	{
		var offset = isLeft ? leftOffset : rightOffset;
		ik.position = controller.TransformPoint(offset._position);
		ik.rotation = controller.rotation * Quaternion.Euler(offset._rotation);
	}

	private void MappingBodyTransform(Transform ik, Transform hmd)
	{
		transform.position = new Vector3(hmd.position.x, hmd.position.y - modelHeight, hmd.position.z);
		float yaw = hmd.eulerAngles.y;
		var targetRotation = new Vector3(transform.eulerAngles.x, yaw, transform.eulerAngles.z);
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation), smoothValue);
	}

	private void MappingHeadTransform(Transform ik, Transform hmd)
	{
		ik.position = hmd.TransformPoint(headOffset._position);
		ik.rotation = hmd.rotation * Quaternion.Euler(headOffset._rotation);
	}
}
