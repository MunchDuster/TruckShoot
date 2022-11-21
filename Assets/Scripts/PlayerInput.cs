using UnityEngine;

public class PlayerInput: InputManager
{
	public override void UpdateInput()
	{
		lookX = Input.GetAxis("Mouse X");
		lookY = Input.GetAxis("Mouse Y");
		moveX = Input.GetAxis("Horizontal");
		moveY = Input.GetAxis("Vertical");
		mousePressed = Input.GetMouseButton(0);
		shiftPressed = Input.GetKey(KeyCode.LeftShift);
	}
}