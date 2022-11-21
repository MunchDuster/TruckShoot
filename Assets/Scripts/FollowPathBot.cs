using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FollowPathBot : InputManager
{
	public UnityEvent OnCompletedPath;
    public Path path;
	public BotDriverSettings settings;

	int pointIndex = 0;
	Vector3 targetOffset;
	bool finishedPath = false;

	// Start is called before the first frame update
	private void Start()
	{
		targetOffset = transform.localPosition;
		moveY = settings.inputMultiplier;
	}
	
	public void AssertAsInputForVehicle()
	{
		GetComponent<Vehicle>().input = this;
	}

	public float nextTurnSensitivity = 0.1f;

	public override void UpdateInput()
	{

		Vector3 targetPosition = path.GetPoint(pointIndex);
		Vector3 nextTargetPosition = path.GetPoint(pointIndex + 1);
		Vector3 offset = targetPosition - transform.position;
		Vector3 nextOffset = nextTargetPosition - transform.position;

		if(!finishedPath)
		{
			Vector3 direction = settings.inputMultiplier * (nextTargetPosition - targetPosition).normalized;
			bool hasPassedPoint = Vector3.Dot(direction, -offset.normalized) < 0;
			bool isCloseToPoint = Vector3.Distance(transform.position, targetPosition) < settings.pointCompletionDistance;
			if(hasPassedPoint || isCloseToPoint)
			{
				
				if(path.IsPointPastFinish(pointIndex + 1))
				{
					OnCompletedPath.Invoke();
					finishedPath = true;
				}
				else
				{
					pointIndex++;
				}
			}
			else
			{
				Vector3 localOffset = transform.InverseTransformDirection(offset);
				Vector3 nextlocalOffset = transform.InverseTransformDirection(nextOffset);
				float move = localOffset.x * settings.turnSensitivity + nextlocalOffset.x * settings.nextTurnSensitivity;
				moveX = Mathf.Clamp(move * settings.inputMultiplier, -1f, 1f);
			}
		}		
	}

	
}
