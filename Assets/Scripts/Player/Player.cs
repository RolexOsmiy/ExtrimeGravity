using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	Rigidbody rb;
	public GameObject PlayerCamera;

	[Header("Movement")]
	public float speed;
	public bool cursorLock = true;

	//mouse look
	public float horizontalSpeed = 2.0F;
	public float verticalSpeed = 2.0F;

	void Start () 
	{
		rb = this.gameObject.GetComponent<Rigidbody>();
		Screen.lockCursor = cursorLock;
	}
		
	void Update () 
	{			

		float h = horizontalSpeed * Input.GetAxis("Mouse X");
		transform.Rotate(0, h, 0);

		var x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
		transform.Translate(x, 0, z);

		if (Input.GetKey(KeyCode.Space)) {
			rb.AddForce (Vector3.up*5);
		}
	}
}
