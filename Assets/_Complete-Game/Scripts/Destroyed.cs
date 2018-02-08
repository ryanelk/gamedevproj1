using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyed : MonoBehaviour {


	public GameObject explosion;
	public GameObject playerExplosion;
	public float rewindTime = 1f;
	public float slowDownFactor = 0.05f;
	public float slowDownLength = 5f;

	bool isRewinding = false;
	List<PointInTime> prevLocations;
	Rigidbody rb;


	void Start () {
		prevLocations = new List<PointInTime>();
		rb = GetComponent<Rigidbody>();
		
	}

	void FixedUpdate() {
		if (!isRewinding) {
			if (prevLocations.Count > Mathf.Round(rewindTime / Time.fixedDeltaTime)) {
				prevLocations.RemoveAt(prevLocations.Count - 1);
			}
			prevLocations.Insert(0, new PointInTime(transform.position, transform.rotation));
		}

	}

	void Rewind() {
		if (prevLocations.Count > 0) {
			startRewind();
		}
	}

	void startRewind() {
		isRewinding = true;
		StartCoroutine("rewind");
	}



	void OnTriggerEnter(Collider other) {

		if (other.tag == "Boundary") {
			return;
		}

		if (other.tag == "Player") {
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
		}
		
		if (other.tag == "Rewind Shot") {
			Destroy (other.gameObject);
			Rewind();

		} else if (other.tag == "Slow Shot") {
			Destroy (other.gameObject);
			DoSlowmotion();	

		} else {
			Instantiate(explosion, transform.position, transform.rotation);
			Destroy (other.gameObject);
			Destroy (gameObject);
		}

	}

	void DoSlowmotion() {
		Time.timeScale = slowDownFactor;
		Time.fixedDeltaTime = Time.timeScale * .02f;

	}

	IEnumerator rewind() {
	    while (prevLocations.Count != 0) {
	    	transform.position = prevLocations[0].t;
			transform.rotation = prevLocations[0].q;
			prevLocations.RemoveAt(0); 
	        
	        yield return null;
	    }
	}
		
}
