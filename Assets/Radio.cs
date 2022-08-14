using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Radio : Interactable
{
	public UnityEvent OnInteract;
	public UnityEvent OnLineFinished;
	public UnityEvent OnLine2Finished;
	public AudioClip line;
	public AudioClip line2;

	bool started = false;
    public override void Interact()
	{
		if(started) return;

		OnInteract.Invoke();
		started = true;
		StartCoroutine(CallNext());
	}
	IEnumerator CallNext()
	{
		yield return new WaitForSeconds(line.length + 2f);
		OnLineFinished.Invoke();
		yield return new WaitForSeconds(line2.length + 2f);
		OnLine2Finished.Invoke();
	}
}
