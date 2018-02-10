using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
	
	public float verticalSpeed = 2.0F;	

	void Update() {	
		float v = verticalSpeed * Input.GetAxis("Mouse Y");
		transform.Rotate(-v, 0, 0);
	}
}
