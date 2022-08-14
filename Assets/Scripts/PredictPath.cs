using UnityEngine;
using System.Collections.Generic;

public class PredictPath: MonoBehaviour
{
	public LineRenderer lineRenderer;
	public Turret turret;
	public Bullet bullet;
	public LayerMask layermask;
	public int maxPoints = 200;
	
	void FixedUpdate()
	{
		List<Vector3> points = new List<Vector3>();

		Vector3 pos = turret.shootPoint.position;
		Vector3 velocity = turret.shootPoint.forward * bullet.speed;
		for(int i = 0; i < maxPoints; i++)
		{
			points.Add(pos);
			velocity += Physics.gravity * Time.fixedDeltaTime;
			pos += velocity * Time.fixedDeltaTime;

			if(Physics.Raycast(pos, velocity.normalized, out RaycastHit hit, velocity.magnitude * Time.fixedDeltaTime, layermask))
			{
				points.Add(hit.point);
				break;
			}

		}

		lineRenderer.positionCount = points.Count;
		lineRenderer.SetPositions(points.ToArray());
	}
}