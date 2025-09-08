using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class BoxBlur : MonoBehaviour
{
	[SerializeField] private Volume volume;

	[SerializeField] private AnimationCurve curve;
	[SerializeField] private float lerpSpeed = 0.1f;


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

			t += Time.deltaTime * lerpSpeed;
			yield return null;
		}
		volume.weight = 1;
		yield return null;
	}
}
