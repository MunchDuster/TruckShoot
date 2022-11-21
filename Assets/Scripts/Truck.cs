using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Truck : MonoBehaviour
{
	public UnityEvent OnEnterTruck;
	public UnityEvent OnExitTruck;
	public UnityEvent OnTruckStart;
	public UnityEvent OnTruckOff;

	public Vehicle vehicle;
	[System.Serializable]
	public struct Wheel {
		public Transform mesh;
		public WheelCollider collider;
	}

	bool on = false;

	public Wheel[] wheels;

	public void EnterTruck()
	{		
		OnEnterTruck.Invoke();
	}

	public InputManager input;
	public void UsePlayerInput()
	{
		input = Player.current.input;
	}

    // Update is called once per frame
    void Update()
    {
		if(input != null)
		{
			input.UpdateInput();
			if(input.shiftPressed && !on)
			{
				OnTruckStart.Invoke();
				on = true;
			} 

		}
		
        UpdateWheelMeshes();
    }
	
	private void UpdateWheelMeshes()
	{
		foreach (Wheel wheel in wheels)
		{
			Vector3 pos = Vector3.zero;
			Quaternion rot = Quaternion.identity;

			wheel.collider.GetWorldPose(out pos, out rot);

			wheel.mesh.position = pos;
			wheel.mesh.rotation = rot;
		}
	}
}
