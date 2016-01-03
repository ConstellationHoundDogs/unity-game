using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject dbg;

	public float speed = 10f;
	public float gravity = 3f;
	public float jump_speed = 2f;

	private float camRayLength = 1000f;     
	private int floorMask;
 	private RaycastHit floorHit;

	private float horizontalInput;
	private float verticalInput;
	private Vector3 movement;

	private bool jump = false;

	private GameObject playerModel;
	private CharacterController charackterController;


	// Use this for initialization
	void Start () {
		charackterController = gameObject.GetComponent<CharacterController> ();
		floorMask = LayerMask.GetMask ("Floor");
		playerModel = transform.Find("Model").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		getInput ();
		TurnModel ();

		//DrawDebug ();
	}

	void DrawDebug () {
		if(dbg){
			dbg.transform.position = floorHit.point;
		}
	}

	void Jump(){
		movement.y = jump_speed;
		jump = false;

	}

	void Move () {
		if (charackterController.isGrounded) {
			movement.Set (horizontalInput, 0, verticalInput);
			movement = transform.TransformDirection(movement);
			movement = movement.normalized * speed * Time.deltaTime;
			if (jump) {
				Jump ();
			}
		}

		movement.y -= gravity * Time.deltaTime;
		charackterController.Move (movement);
	}

	void TurnModel () {
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		// Perform the raycast and if it hits something on the floor layer...
		if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
		{
			// Create a vector from the player to the point on the floor the raycast from the mouse hit.
			Vector3 playerToMouse = floorHit.point - transform.position;

			// Ensure the vector is entirely along the floor plane.
			playerToMouse.y = 0f;

			// Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
			Quaternion newRotatation = Quaternion.LookRotation (playerToMouse);

			playerModel.transform.rotation = newRotatation;
		}
	}

	void FixedUpdate () {
		Move ();
	}

	void getInput(){
		horizontalInput = Input.GetAxis ("Horizontal");
		verticalInput = Input.GetAxis ("Vertical");
		jump = Input.GetButton("Jump");
	}
		
}

