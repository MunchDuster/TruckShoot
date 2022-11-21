using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTrigger : MonoBehaviour
{	
	public UnityEvent OnTriggered;
	public string triggerTag;


	bool fired = false;

	void OnTriggerEnter(Collider collider)
	{
		if(collider.tag == triggerTag && !fired)
		{
			fired = true;
			OnTriggered.Invoke();
		}
	}
}	