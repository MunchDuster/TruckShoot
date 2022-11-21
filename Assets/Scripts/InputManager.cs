using UnityEngine;

[System.Serializable]
public abstract class InputManager: MonoBehaviour
{
	[HideInInspector] public float lookX;
	[HideInInspector] public float lookY;
	[HideInInspector] public float moveX;
	[HideInInspector] public float moveY;
	[HideInInspector] public bool mousePressed;
	[HideInInspector] public bool shiftPressed;

	public abstract void UpdateInput();
}