using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject dbg;

	public float speed = 10f;
	public float gravity = 3f;
	public float jump_speed = 2f;

	private int movementAngle = -45;

	private float camRayLength = 1000f;     
	private int floorMask;
 	private RaycastHit floorHit;

	private float horizontalInput;
	private float verticalInput;
	private Vector3 movement;

	private bool jump = false;

	private GameObject playerModel;
	private CharacterController charackterController;

	void DrawDebug () {
		if (dbg) {
			dbg.transform.position = floorHit.point;
		}
	}

	void Jump () {
		movement.y = jump_speed;
		jump = false;
	}

	void Move () {
		if (charackterController.isGrounded) {
			movement.Set (horizontalInput, 0, verticalInput);
			movement = movement.normalized * speed * Time.deltaTime;
			movement = Quaternion.AngleAxis(movementAngle, Vector3.up) * movement;
			if (jump) {
				Jump ();
			}
		}

		movement.y -= gravity * Time.deltaTime;
		charackterController.Move (movement);
	}

	void Turn () {
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;
			Quaternion newRotatation = Quaternion.LookRotation (playerToMouse);

			transform.rotation = newRotatation;
		}
	}

	void getInput(){
		horizontalInput = Input.GetAxis ("Horizontal");
		verticalInput = Input.GetAxis ("Vertical");
		jump = Input.GetButton("Jump");
	}
		
	Quaternion getRotation(){
		return transform.rotation;
	}

	// Use this for initialization
	void Start () {
		charackterController = gameObject.GetComponent<CharacterController> ();
		floorMask = LayerMask.GetMask ("Floor");
		playerModel = transform.Find("Model").gameObject;
		Debug.Log (transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
		getInput ();
		Turn ();
	}
		
	void FixedUpdate () {
		Move ();
	}
		
}

