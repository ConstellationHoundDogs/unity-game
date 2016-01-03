using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform target;
	public float smooth = 5.0f;
	Vector3 offset;

	void Start(){
		offset = transform.position - target.position;
	}

	void FixedUpdate () {
		Vector3 targetCamPos = target.position + offset;
		Vector3 currentPosition = Vector3.Lerp (transform.position, targetCamPos, smooth * Time.deltaTime);
		currentPosition.y = 40f;
		transform.position = currentPosition;
	}
}
