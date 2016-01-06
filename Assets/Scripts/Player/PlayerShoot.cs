using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

	public float shootDelay = 10.0f;
	public GameObject bullet;

	private GameObject playerModel;

	private bool shooting;
	private bool firePressed;
	private float timePassed;

	private float emmitionPositionOffset = 1f;

	void Start(){
	}
		
	void Update () {
		GetInput ();
		Shoot ();
	}

	void FireBullet(){
		//TODO:
		//Bullet firing should be handled by emmiter and bullet objects should be stored and retrieved by BulletObjectPool

		Vector3 emmitionPosition = transform.position;
		Instantiate (bullet, emmitionPosition + transform.forward * emmitionPositionOffset, transform.rotation);
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
