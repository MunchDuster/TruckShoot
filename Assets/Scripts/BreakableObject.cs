using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BreakableObject : MonoBehaviour
{
	public UnityEvent OnBreak;
    public void Break()
	{
		OnBreak.Invoke();
		foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
		{
			rb.isKinematic = false;
		}

		this.enabled = false;
	}
}
