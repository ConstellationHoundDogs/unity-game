using UnityEngine;
using System.Collections;

public class PlayerBulletController : MonoBehaviour {

	public float speed = 1.0f;

	private Rigidbody rb;

	void Start(){
		rb = gameObject.GetComponent<Rigidbody> ();
		rb.AddForce (transform.forward * speed);
		Debug.Log ("Bullet fired");
	}

	void FixedUpdate () {
	}
}
