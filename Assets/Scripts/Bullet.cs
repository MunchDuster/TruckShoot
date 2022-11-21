using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[Header("Refs")]
	public Vector3 parentVelocity;
	public Rigidbody rb;
	public GameObject hitEffect;
	public AudioSource hitAudio;

	[Header("Settings")]
	public float speed = 20;
	public float explosionRadius = 4;
	public float explosionForce = 4;

	[Header("Camera Shake")]
	public float camShake = 0.8f;
	public float maxShakeDist = 100;
	public AnimationCurve distShakeCurve;

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
    }

    void OnCollisionEnter(Collision collision)
	{
		GameObject FX = Instantiate(hitEffect, collision.contacts[0].point, Quaternion.identity);

		hitAudio.transform.SetParent(FX.transform);
		hitAudio.Play();

		float dist = Vector3.Distance(collision.contacts[0].point, Camera.main.transform.position) / maxShakeDist;
		if(dist < maxShakeDist)Camera.main.GetComponent<CameraShake>().Shake(distShakeCurve.Evaluate(dist) * camShake);

		foreach(Collider collider in Physics.OverlapSphere(collision.contacts[0].point, explosionRadius))
		{
			Rigidbody rb = collider.GetComponent<Rigidbody>();
			if(rb != null)
			{
				rb.AddExplosionForce(explosionForce, collision.contacts[0].point, explosionRadius);
			}
			BreakableObject bo = collider.GetComponentInParent<BreakableObject>();
			if(bo != null)
			{
				bo.Break();
			}
		}

		Destroy(gameObject);
	}

	// Update is called every frame
	private void Update()
	{
		if(rb.velocity.normalized.magnitude > 0f) transform.rotation = Quaternion.LookRotation(rb.velocity.normalized);
	}
}
