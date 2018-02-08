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
	
	GameObject shot;

	public GameObject shot_normal;
	public GameObject shot_rewind;
	public GameObject shot_slow;

	public Transform shotSpawn;

	public float nextFire;
	public float fireRate;
	public int toggleShot = 0;

	public float slowDownFactor = 0.05f;
	public float slowDownLength = 5f;

	void Update() {

		if (Input.GetKeyDown(KeyCode.R)) {

			toggleShot = (toggleShot + 1) % 3;
			if (toggleShot == 0) {
				shot = shot_normal;
				fireRate = 0.25f;
			}
			if (toggleShot == 1) {
				shot = shot_rewind;
				fireRate = 0.5f;
			}
			if (toggleShot == 2) {
				shot = shot_slow;
				fireRate = 0.5f;
			}
		}

		if (Input.GetButton("Fire1") && Time.time > nextFire) {

			if (shot == null) {
				shot = shot_normal;
			}
		
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		
		}

		Time.timeScale += (1f/ slowDownLength) * Time.unscaledDeltaTime;
		Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
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
