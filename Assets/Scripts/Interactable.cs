using UnityEngine;

public abstract class Interactable: MonoBehaviour
{
	public string hoverInfo;
	public abstract void Interact();
}