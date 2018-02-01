using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {

	public float xMin, xMax, zMin, zMax;

}


public class PlayerController : MonoBehaviour {
	
	public Boundary boundary;
	private Rigidbody rig;

	public float speed;
	public float tilt;
	
	public GameObject shot;
	public Transform shotSpawn;

	public float nextFire;
	public float fireRate;

	void Update() {

		if (Input.GetButton("Fire1") && Time.time > nextFire) {
		
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		
		}
	}

	void FixedUpdate() {
		rig = GetComponent<Rigidbody>();
		float moveVertical = Input.GetAxis("Vertical");
		float moveHorizontal = Input.GetAxis("Horizontal");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rig.velocity = movement * speed;

		rig.position = new Vector3
		(
			Mathf.Clamp(rig.position.x, boundary.xMin, boundary.xMax), 
			0.0f,
			Mathf.Clamp(rig.position.z, boundary.zMin, boundary.zMax)
		);

		rig.rotation = Quaternion.Euler(0.0f, 0.0f, rig.velocity.x * -tilt);

	}
}
