using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class BoxBlur : MonoBehaviour
{
	[SerializeField] private Volume volume;

	[SerializeField] private AnimationCurve curve;
	public float lerpTime = 3f;


	public void Blur()
	{
		StartCoroutine(Bluring());
	}

	private IEnumerator Bluring()
	{
		float t = 0;
		while (t < 1)
		{
			volume.weight = Mathf.Lerp(0, 1, curve.Evaluate(t));

			t += Time.deltaTime / lerpTime;
			yield return null;
		}
		volume.weight = 1;
		yield return null;
	}
}
