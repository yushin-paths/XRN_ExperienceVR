using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
	public Animator ani;
	public AnimationCurve curve;
	public float move_delay = 1;
	public float lerpSpeed = 1f;
	public bool default_facing = true;
	public bool trun;

	public bool debugLog;

	private void Awake()
	{
		ani = GetComponent<Animator>();

		StartCoroutine(Worker_animtion());
	}

	IEnumerator Worker_animtion()
	{
		while (true)
		{
			if (ani.GetCurrentAnimatorStateInfo(1).normalizedTime >= 0.9f && default_facing)
			{
				default_facing = false;


				yield return null;
				ani.SetFloat("Speed", 0);
				ani.SetTrigger("Stop");

				yield return new WaitForSeconds(move_delay);

				//if (debugLog)
				//	Debug.Log(transform.eulerAngles.y);

				var current_eulerAngles = transform.eulerAngles;
				if (trun)
					current_eulerAngles = new Vector3(current_eulerAngles.x, current_eulerAngles.y + 180, current_eulerAngles.z);
				else
					current_eulerAngles = new Vector3(current_eulerAngles.x, current_eulerAngles.y - 180, current_eulerAngles.z);


				float time = 0;
				while (time < 1)
				{
					transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, current_eulerAngles, curve.Evaluate(time));
					time += Time.deltaTime * lerpSpeed;

					yield return null;
				}

				transform.eulerAngles = current_eulerAngles;
				yield return new WaitForSeconds(move_delay);


				ani.SetTrigger("Idle_Walking");
				ani.SetFloat("Speed", 1f);
			}
			else if (ani.GetCurrentAnimatorStateInfo(1).normalizedTime >= 0.9f && !default_facing)
			{
				default_facing = true;


				yield return null;
				ani.SetFloat("Speed", 0);
				ani.SetTrigger("Stop");

				yield return new WaitForSeconds(move_delay);

				//if (debugLog)
				//	Debug.Log(transform.eulerAngles.y);

				var current_eulerAngles = transform.eulerAngles;
				if (trun)
					current_eulerAngles = new Vector3(current_eulerAngles.x, current_eulerAngles.y - 180, current_eulerAngles.z);
				else
					current_eulerAngles = new Vector3(current_eulerAngles.x, current_eulerAngles.y + 180, current_eulerAngles.z);


				float time = 0;
				while (time < 1)
				{
					transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, current_eulerAngles, curve.Evaluate(time));
					time += Time.deltaTime * lerpSpeed;

					yield return null;
				}

				transform.eulerAngles = current_eulerAngles;
				yield return new WaitForSeconds(move_delay);


				ani.SetTrigger("Idle_Walking");
				ani.SetFloat("Speed", 1f);
			}

			yield return null;
		}
	}
}
