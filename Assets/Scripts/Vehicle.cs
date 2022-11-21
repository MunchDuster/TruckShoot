using UnityEngine;

public class Vehicle: MonoBehaviour
{
	public InputManager input;

	[Header("Settings")]
	public float maxTorque = 100;
	public float maxSteer = 45;
	public float steerSpeed = 10;
	public AnimationCurve rpmToTorque;
	public float maxRPM = 50;
	public float brakeTorque = 100;

	[Space(10)]
	public Vector3 centerOfMass = Vector3.zero;

	[Header("Inner Refs")]
	public WheelCollider[] drivingWheels;
	public WheelCollider[] steeringWheels;

	public AudioSource engineSource;
	public float maxPitch = 4;
	public float minPitch = 1;

	private float curSteer;
	public Rigidbody rb;
	// Start is called before the first frame update
	void Start()
	{
		rb.centerOfMass = centerOfMass;
	}

	InputManager oldInput;
	public void UsePlayerInput()
	{
		oldInput = input;
		input = Player.current.input;
	}

	void FixedUpdate()
	{
		if(input == null) return;
		input.UpdateInput();
		UpdateSteeringWheels();
		UpdateDrivingWheels();
		UpdateEngineSound();
	}
	void UpdateEngineSound()
	{
		float speed = drivingWheels[0].rpm / maxRPM;
		engineSource.pitch = Mathf.Lerp(minPitch, maxPitch, speed);
	}
	void UpdateDrivingWheels()
	{
		if(input.shiftPressed)
		{
			foreach (WheelCollider wheelCollider in drivingWheels)
			{
				wheelCollider.brakeTorque = brakeTorque;
				wheelCollider.motorTorque = 0;
			}
		}
		else
		{
			foreach (WheelCollider wheelCollider in drivingWheels)
			{
				wheelCollider.brakeTorque = 0;
				wheelCollider.motorTorque = maxTorque * input.moveY * rpmToTorque.Evaluate(Mathf.Clamp01(wheelCollider.rpm / maxRPM));
			}
		}
	}
	void UpdateSteeringWheels()
	{
		float targetSteer = maxSteer * input.moveX;
		curSteer = Mathf.Lerp(curSteer, targetSteer, steerSpeed * Time.fixedDeltaTime);
		foreach (WheelCollider wheelCollider in steeringWheels)
		{
			wheelCollider.steerAngle = curSteer;
		}
	}
}