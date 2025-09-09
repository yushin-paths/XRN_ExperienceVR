using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grab_Motion : MonoBehaviour
{
	public Animator ani;

	public RectTransform left_hand;
	public RectTransform right_hand;

	private void Update()
	{
		//if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger)/* || left*/)
		//	ani.Play("Grab_Left");
		//if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger)/* || right*/)
		//	ani.Play("Grab_Right");

		//if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger)/* || !left*/)
		//	ani.Play("Grab_Left Reverse");
		//if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger)/* || !right*/)
		//	ani.Play("Grab_Right Reverse");

		left_hand.transform.localPosition = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick) * 20;
		right_hand.transform.localPosition = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick) * 20;
	}
}
