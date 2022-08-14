using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEvent : MonoBehaviour
{
	[System.Serializable]
	public struct CustomEvent {
    	public UnityEvent Event;
		public string name;
	}

	public CustomEvent[] events;

	public void Call(string eventName)
	{
		for(int i = 0; i < events.Length; i++)
		{
			if(events[i].name == eventName)
			{
				events[i].Event.Invoke();
				break;
			}
		}
	}
}
