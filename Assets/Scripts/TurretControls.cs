using UnityEngine;

public class TurretControls: MonoBehaviour
{
	public InputManager input;
	public Transform yawTransform;
	public Transform pitchTransform;

	[Space(10)]
	public float turnSpeed = 45;

	[Space(10)]
	public float maxPitch = 45;
	public float minPitch = 0;

	float pitch = 0;
	float yaw = 0;

	public void UsePlayerInput()
	{
		input = Player.current.input;
	}

	public Vector3 yawAxis = Vector3.up;
	public Vector3 pitchAxis = Vector3.right;

	//Update is called every frame.
	private void Update()
	{
		if(input == null) return;
		float yawInput = input.lookX;
		float pitchInput = input.lookY;

		float yawDelta = yawInput * turnSpeed * Time.deltaTime;
		yaw += yawDelta;

		float pitchDelta = pitchInput * turnSpeed * Time.deltaTime;
		pitch = Mathf.Clamp(pitch + pitchDelta, minPitch, maxPitch);

		yawTransform.localRotation = Quaternion.AngleAxis(yaw, yawAxis);
		pitchTransform.localRotation = Quaternion.AngleAxis(-pitch, pitchAxis);
	}

	public void SetPitch(float pitch)
	{
		this.pitch = pitch;
	}
	public void AddYaw(float deltaYaw)
	{
		yaw += deltaYaw;
	}
}