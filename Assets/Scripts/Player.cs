using UnityEngine;
using TMPro;

public class Player: MonoBehaviour
{
	public static Player current;

	public bool canLook = true;
	public bool canMove = true;
	public bool canSense = true;

	public Camera camera;
	public Transform cameraParent;
	public Transform body;
	public CharacterController character;
	public InputManager input;
	public float raycastDist = 3;
	public TextMeshProUGUI text;

	// Start is called before the first frame update
	private void Start()
	{
		current = this;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void ReleaseCamera(Transform newParent)
	{
		transform.SetParent(newParent);
		camera.transform.localRotation = Quaternion.identity;
		camera.transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		transform.localPosition = Vector3.zero;
		canMove = false;
		canLook = false;
		canSense = false;
		character.enabled = false;
	}
	public void TakeBackCamera(Transform exitPlace)
	{
		transform.SetParent(null);
		camera.transform.localRotation = Quaternion.identity;
		camera.transform.localPosition = Vector3.zero;
		transform.localRotation = exitPlace.rotation;
		transform.localPosition = exitPlace.position;
		canMove = true;
		canLook = true;
		canSense = true;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public float lookSpeed = 360;
	public float walkSpeed = 5;
	public float jumpSpeed = 1;
	public float groundDist = 1.2f;
	float lookX;
	float yVelocity = 0;

	void Update()
	{
		if(canSense && Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit hit, raycastDist))
		{
			Interactable interactable = hit.collider.GetComponent<Interactable>();
			if(interactable != null)
			{
				if(Input.GetMouseButtonDown(0))
				{
					interactable.Interact();
					text.text = "";
				}
				else
				{
					text.text = interactable.hoverInfo;
				}
			}
			else
			{
				text.text = "";
			}
		}
	}

	void FixedUpdate()
	{
		if(canLook)
		{
			float inputX = Input.GetAxis("Mouse X");
			float inputY = Input.GetAxis("Mouse Y");
			float delta = Time.deltaTime * lookSpeed;
			lookX -= inputY * delta;
			lookX = Mathf.Clamp(lookX, -90f, 90f);
			cameraParent.localRotation = Quaternion.Euler(lookX, 0, 0);
			body.Rotate(0, inputX * delta, 0);
		}

		Vector3 move = Vector3.zero;
		bool touchingGround = character.isGrounded;

		if(touchingGround) yVelocity = -1;
		else yVelocity += Time.fixedDeltaTime * Physics.gravity.y;

		if(canMove)
		{
			float inputX = Input.GetAxis("Horizontal");
			float inputY = Input.GetAxis("Vertical");
			float delta = Time.deltaTime * walkSpeed;
			move = transform.TransformDirection(new Vector3(inputX * delta, 0, inputY * delta));

			bool jumpPressed = Input.GetKey(KeyCode.Space);
			if(jumpPressed && touchingGround)yVelocity = jumpSpeed;

			move += Vector3.up *  yVelocity;
			character.Move(move);
		}
		

	}

}