using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ELV_Trigger : MonoBehaviour
{
	public enum Trigger_Type
	{
		GetIn,
		Move,
		GetOut
	}
	private enum Open_Close
	{
		Open,
		Close
	}

	public Trigger_Type trigger_Type;
	public GameObject ELV;
	private Animator elv_ani;

	private void Awake()
	{
		elv_ani = ELV.GetComponent<Animator>();
	}

	private void OnTriggerEnter(Collider other)
	{
		switch (trigger_Type)
		{
			case Trigger_Type.GetIn:
				ELV_Open_Close(Open_Close.Open);
				break;
			case Trigger_Type.Move:
				StartCoroutine(ELV_Move());
				break;
			case Trigger_Type.GetOut:
				ELV_Open_Close(Open_Close.Close);
				break;
			default:
				break;
		}
	}

	private void ELV_Open_Close(Open_Close o_c)
	{
		switch (o_c)
		{
			case Open_Close.Open:
				elv_ani.SetTrigger("Open");
				break;
			case Open_Close.Close:
				elv_ani.SetTrigger("Close");
				break;
		}
	}

	private IEnumerator ELV_Move()
	{
		ELV_Open_Close(Open_Close.Close);

		yield return null;

		while (elv_ani.GetCurrentAnimatorStateInfo(1).IsName("ELV_Close") && elv_ani.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f)
		{
			yield return null;
		}

		yield return null;

		elv_ani.SetTrigger("Move");

		yield return null;

		while (elv_ani.GetCurrentAnimatorStateInfo(0).IsName("ELV_Move") && elv_ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
		{
			yield return null;
		}

		yield return null;

		ELV_Open_Close(Open_Close.Open);

		yield return null;
	}
}
