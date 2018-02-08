using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInTime {

	public Vector3 t;
	public Quaternion q;

	public PointInTime(Vector3 transform, Quaternion quaternion) {
		t = transform;
		q = quaternion;
	}

}
