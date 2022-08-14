using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Seat : Interactable
{
	public Transform playerPoint;
		
	public UnityEvent OnPlayerEnter;

	public override void Interact()
	{
		OnPlayerEnter.Invoke();
		Player.current.ReleaseCamera(playerPoint);
	}
}
