using System.Collections;
using UnityEngine;

public class BotTurretFirer: InputManager
{
	public float minTime = 4f;
	public float maxTime = 6f;
	public float minDistanceFire = 5;
	public float maxRaycastDist = 10;
	public string avoidTag;
	public Transform shootPoint;
	public BotTurret turretter;
	//Start is called before first update.
	private void Start()
	{
		StartCoroutine(WaitFire());
	}

	public override void UpdateInput(){}

	IEnumerator WaitFire()
	{
		while(true)
		{
			yield return new WaitForSeconds(Random.Range(minTime, maxTime));

			bool cantFire = true;
			while(cantFire)
			{
				if(turretter.inRange)
				{
					if(Physics.Raycast(shootPoint.position, shootPoint.forward, out RaycastHit hit, maxRaycastDist))
					{
						if(hit.distance > minDistanceFire && hit.collider.gameObject.tag != avoidTag)
						{
							cantFire = false;
						}
					}
					else
					{
						cantFire = false;
					}
				}
				
				yield return new WaitForEndOfFrame();
			}
			shiftPressed = true;
			yield return new WaitForSeconds(0.1f);
			shiftPressed = false;
		}
	}
}