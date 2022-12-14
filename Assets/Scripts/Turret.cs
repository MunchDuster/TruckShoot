using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Turret : MonoBehaviour
{
	public InputManager input;
	public Transform shootPoint;
	public GameObject bulletPrefab;
	public Rigidbody rb;
	public UnityEvent onFire;
	public Animator animator;
	public float recoilTime = 3;

	bool isRecoiling;

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
        	if(input.shiftPressed && !isRecoiling) Fire();
		}
    }

	void Fire()
	{
		onFire.Invoke();
		if(animator != null) animator.SetTrigger("Fire");
		GameObject newBullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
		newBullet.GetComponent<Bullet>().parentVelocity = rb.GetPointVelocity(shootPoint.position);
		StartCoroutine(Recoil());
	}

	IEnumerator Recoil()
	{
		isRecoiling = true;
		yield return new WaitForSeconds(recoilTime);
		isRecoiling = false;
	}
}
