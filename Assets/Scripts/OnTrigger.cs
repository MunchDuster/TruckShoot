using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTrigger : MonoBehaviour
{
	public string tag;
	public UnityEvent OnCall;

	bool hasDone = false;
    void OnTriggerEnter(Collider collider)
	{
		if(!hasDone && collider.gameObject.tag == tag)
		{
			hasDone = true;
			OnCall.Invoke();
		}
	}
}
