using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotTurret : InputManager
{
	public Transform shootPoint;
	public Transform[] targets;
	public Bullet bullet;

	public TurretControls turret;

	public float k = 1;
	public float u1K = 1;
	public float s1K = 1;

	public float startA = 1;

	Transform trackingTarget;
	int t = 0;

	public int loops = 4;

	public float maxDistance = 50;
	public AnimationCurve curve;

	public bool inRange;

    // FixedUpdate is called every physics update
	public override void UpdateInput()
	{
		if(trackingTarget == null) trackingTarget = targets[t++];

		Vector3 targetPoint = trackingTarget.position; //Include tracking target velocity later

		//Aim at target point
		//using kinematic equations: https://en.wikipedia.org/wiki/Equations_of_motion

		//Calculate angle to fire to reach target
		Vector3 right = Vector3.ProjectOnPlane(targetPoint - shootPoint.position, Vector3.up);
		Vector3 up = Vector3.up;

		float dXZ = right.magnitude;

		inRange = dXZ < maxDistance;

		if(!inRange) return;

		float dY = targetPoint.y - shootPoint.position.y;
		float a = Physics.gravity.y;
		float angle = curve.Evaluate(dXZ / maxDistance) * 45f;



		// for(int i = 0; i < curve.keys.Length; i++)
		// {
		// 	curve.RemoveKey(0);
		// }

		// float delta = 0;

		// for(int i = 0; i < loops; i++)
		// {
		// 	float time = GetTime(angle, dY, a);

		// 	if(float.IsNaN(time))
		// 	{
		// 		Debug.Log("NaN");
		// 		angle -= delta / 2f;
		// 	}
		// 	else
		// 	{
		// 		float rightSpeed = Mathf.Cos(angle * Mathf.Deg2Rad) * bullet.speed;
		// 		float distX = time * rightSpeed;
		// 		float realDistX = right.magnitude;
		// 		delta = (k / (i + 1)) * (realDistX - distX);
		// 		angle += delta;
		// 	}
		// 	curve.AddKey((float)i/loops, angle/45f);
		// }

		lookY = Mathf.Sign(angle-turret.GetPitch());
		
		Vector3 forward = yawForward.forward;
		Vector3 toTarget = (targetPoint - transform.position).normalized;
		float angleY = Vector3.SignedAngle(toTarget, forward, Vector3.up);
		Debug.DrawRay(transform.position, toTarget, Color.red);
		Debug.DrawRay(transform.position, forward, Color.green);
		lookX = -Mathf.Sign(angleY);
	}
	public Transform yawForward;

	float GetTime(float angle, float dY, float a)
	{
		float u1 = Mathf.Sin(angle * Mathf.Deg2Rad) * bullet.speed;

		//Pointing downwards
		if(u1 <= 0)
		{
			
			// if(dY < 0)
			// Debug.Log("u1 < 0");
			// float v2 = Mathf.Sqrt(u1*u1 + 2 * a * dY);
			// float t2 = (v2 - u1) / a;
			// return t2;
		}
		//Pointing upwards
		else
		{
			
		}

		float s1 = -(u1 * u1) / (2 * a);
			
			//Object height > max height of path
			if(dY > s1 ) 
			{
				Debug.Log("dY > s1");
				float u2 = Mathf.Sqrt(2 * -a * s1);
				//float 
				return -u2 / a;
			}
			//Object height < max height of path
			else
			{
				float t1 = u1 / -a;
				float s2 = dY - s1;
				float v2 = Mathf.Sqrt(2 * a * -Mathf.Abs(s2));
				float t2 = v2 / -a;

				return t2 + t1;
			}
	}
}
