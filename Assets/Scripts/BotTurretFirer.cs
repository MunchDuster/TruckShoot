using System.Collections;
using UnityEngine;

public class BotTurretFirer: InputManager
{
	public float minTime = 4f;
	public float maxTime = 6f;
	//Start is called before first update.
	private void Start()
	{
		StartCoroutine(WaitFire());
	}

	IEnumerator WaitFire()
	{
		while(true)
		{
			yield return new WaitForSeconds(Random.Range(minTime, maxTime));
			shiftPressed = true;
			yield return new WaitForSeconds(0.1f);
			shiftPressed = false;
		}
	}
}