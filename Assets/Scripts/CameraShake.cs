using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	public AnimationCurve falloff;
	public float duration;

	public void Shake(float maxRotation)
	{
		StartCoroutine(ShakeCamera(maxRotation));
	}

    IEnumerator ShakeCamera(float maxRotation)
	{
		float elapsed = 0;

		while(elapsed < duration)
		{
			Vector3 eulers = new Vector3(
				Random.Range(-1f, 1f),
				Random.Range(-1f, 1f),
				Random.Range(-1f, 1f)
			) * maxRotation * falloff.Evaluate(elapsed / duration);

			transform.localRotation = Quaternion.Euler(eulers);

			elapsed += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}
}
