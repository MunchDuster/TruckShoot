using UnityEngine;

public class Path: MonoBehaviour
{
	public Vector3 GetPoint(int index)
	{
		if(index < transform.childCount)
		{
			return transform.GetChild(index).position;
		}
		else
		{
			return transform.GetChild(transform.childCount - 1).position;
		}
	}

	public bool IsPointPastFinish(int index)
	{
		return !(index <= transform.childCount);
	}

	#if UNITY_EDITOR
	void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		for(int i = 0; i < transform.childCount; i++)
		{
			Vector3 targetPosition = transform.GetChild(i).position;
			Gizmos.DrawWireSphere(targetPosition, 0.8f);
		}

		Gizmos.color = Color.green;
		for(int i = 0; i < transform.childCount - 1; i++)
		{
			Vector3 targetPosition = transform.GetChild(i).position;
			Vector3 nextTargetPosition = transform.GetChild(i + 1).position;
			Gizmos.DrawLine(targetPosition, nextTargetPosition);
		}
	}
	#endif
}