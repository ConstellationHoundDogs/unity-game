using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

	public float shootDelay = 10.0f;
	public GameObject bullet;

	private GameObject playerModel;

	private bool shooting;
	private bool ableToShoot = true;
	private bool firePressed;
	private float timePassed;

	void Start(){
		playerModel = transform.Find("Model").gameObject;
	}
		
	void Update () {
		GetInput ();
		Shoot ();
	}

	void FireBullet(){

		//TODO:
		//Bullet firing should be handled by emmiter
		//Rotation for player should be stored not in model

		Vector3 emmitingPosition = playerModel.transform.position;

		Instantiate (bullet, emmitingPosition + playerModel.transform.forward * 1f, playerModel.transform.rotation);
	}

	void Shoot(){

		timePassed += Time.deltaTime;
		if(timePassed >= shootDelay){
			if(firePressed){
				timePassed = 0f;
				FireBullet ();
			}
		}
	}

	void GetInput(){
		firePressed = Input.GetButton ("Fire1");
	}
}
