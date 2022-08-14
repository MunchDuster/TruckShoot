using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FollowPathBot : InputManager
{
	public UnityEvent OnCompletedPath;
    public Transform pointsParent;
	public float turnSensitivity = 3;
	public float pointCompletionDistance = 2;

	public Transform finishedTarget;
	public float finishedDistance = 30;

	int pointIndex = 0;
	Vector3 targetOffset;
	bool finishedPath = false;
	public bool autoOffsetFromPath = true;
	Transform targetPoint;

	// Start is called before the first frame update
	private void Start()
	{
		targetOffset = transform.localPosition;
		targetPoint = pointsParent.GetChild(pointIndex);
		moveY = 1;
	}
	
	public void AssertAsInputForVehicle()
	{
		GetComponent<Vehicle>().input = this;
	}

	void FixedUpdate()
	{

		Vector3 targetPosition = (autoOffsetFromPath) ? targetPoint.position + targetOffset: targetPoint.position;
		Vector3 offset = targetPosition - transform.position;

		if(!finishedPath)
		{
			bool hasPassedPoint = Vector3.Dot(targetPoint.forward, -offset.normalized) > 0;
			bool isCloseToPoint = Vector3.Distance(transform.position, targetPosition) < pointCompletionDistance;
			if(hasPassedPoint || isCloseToPoint)
			{
				
				if(pointIndex + 1 >= pointsParent.childCount)
				{
					OnCompletedPath.Invoke();
					finishedPath = true;
				}
				else
				{
					pointIndex++;
					targetPoint = pointsParent.GetChild(pointIndex);
				}
			}
			else
			{
				Vector3 localOffset = transform.InverseTransformDirection(offset.normalized);
				moveX = Mathf.Clamp(localOffset.x * turnSensitivity, -1f, 1f);
			}
		}
		else
		{
			bool isCloseToTarget = Vector3.Distance(transform.position, targetPosition) < finishedDistance;
			if(!isCloseToTarget)
			{
				Vector3 localOffset = transform.InverseTransformDirection(offset.normalized);
				moveX = Mathf.Clamp(localOffset.x * turnSensitivity, -1f, 1f);
				shiftPressed = false;
			}
			else
			{
				shiftPressed = true;
			}
		}
		
	}

	#if UNITY_EDITOR
	void OnDrawGizmos()
	{
		if(pointsParent == null) return;

		if(targetOffset == null) targetOffset = transform.localPosition;

		Gizmos.color = Color.white;
		for(int i = 0; i < pointsParent.childCount; i++)
		{
			Transform targetPoint = pointsParent.GetChild(i);
			Vector3 targetPosition = (autoOffsetFromPath) ? targetPoint.position + transform.TransformPoint(targetOffset): targetPoint.position;
			Gizmos.DrawWireSphere(targetPosition, 0.4f);
		}

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(pointsParent.GetChild(pointIndex).position, 0.4f);
		Gizmos.DrawLine(transform.position, pointsParent.GetChild(pointIndex).position);

		Gizmos.color = Color.green;
		for(int i = 0; i < pointsParent.childCount - 1; i++)
		{
			Gizmos.DrawLine(pointsParent.GetChild(i).position, pointsParent.GetChild(i + 1).position);
		}
	}
	#endif
}
