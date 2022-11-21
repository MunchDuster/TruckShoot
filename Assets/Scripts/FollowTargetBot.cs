using UnityEngine;

public class FollowTargetBot: InputManager
{
	public Transform target;
	public float stopDistance = 20;
	public BotDriverSettings settings;

	// Start is called before the first frame update
	private void Start()
	{
		moveY = 1;
	}
	
	public void AssertAsInputForVehicle()
	{
		GetComponent<Vehicle>().input = this;
	}
	
	public override void UpdateInput()
	{
		bool isCloseToTarget = Vector3.Distance(transform.position, target.position) < stopDistance;
		if(!isCloseToTarget)
		{
			Vector3 offset = target.position - transform.position;
			Vector3 localOffset = transform.InverseTransformDirection(offset.normalized);
			moveX = Mathf.Clamp(localOffset.x * settings.turnSensitivity, -1f, 1f);
			shiftPressed = false;
		}
		else
		{
			shiftPressed = true;
		}
	}
}