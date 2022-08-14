using UnityEngine;

public class FaceTransform : MonoBehaviour
{
	public Transform target;
    void Update()
    {
        transform.LookAt(target);
    }
}
