using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour {

	public int selectedWeapon = 0;
	AudioSource audioSource;

	[System.Serializable]
	public class Guns
	{
		public string gunName = "name"; 
		public Image curGun;
		public AudioClip shot;
	}
	public Guns[] gunCount;
	public Text curGunText;

	[Header("Shooting")]
	public GameObject hitPos;
	public float hookSpeed;
	public float objectSpeed;
	GameObject point;
	RaycastHit hit;
	GameObject target = null;
	public LineRenderer lineRender;
	public Rigidbody playerBody;
	[Header("Rope")]
	public GameObject firstObject;
	public GameObject secondObject;
	bool condition = true;

	void Start () 
	{
		audioSource = GetComponent<AudioSource>();
	}

	void Update () 
	{
		float step = hookSpeed * Time.deltaTime;


		if (firstObject && secondObject) {
			lineRender.enabled = true;
			if (Vector3.Distance (firstObject.transform.position, secondObject.transform.position) >= 2) {
				firstObject.transform.position = Vector3.MoveTowards (firstObject.transform.position, secondObject.transform.position, objectSpeed * Time.deltaTime);	
				secondObject.transform.position = Vector3.MoveTowards (secondObject.transform.position, firstObject.transform.position, objectSpeed * Time.deltaTime);
			}
			lineRender.SetPosition (0, firstObject.transform.position);
			lineRender.SetPosition (1, secondObject.transform.position);

		} else {
			lineRender.enabled = false;
		}

		if (selectedWeapon == 0 || !firstObject && !secondObject) {
			if (target) {
				transform.position = Vector3.MoveTowards (transform.position, target.transform.position, step);	
				lineRender.SetPosition (0, gameObject.transform.position);
				lineRender.SetPosition (1, target.transform.position);
				lineRender.enabled = true;
			} else {
				lineRender.enabled = false;
			}
		}


		WriteGun ();
		if (Input.GetAxis ("Mouse ScrollWheel") > 0f && selectedWeapon < gunCount.Length - 1) {
			selectedWeapon++;
		} 
		if (Input.GetAxis ("Mouse ScrollWheel") < 0f && selectedWeapon > 0) 
		{
			selectedWeapon--;
		}

		if (Input.GetButtonDown("Jump")) {
			playerBody.useGravity = true;
			target = null;
		}
		if (Input.GetKey (KeyCode.Q)) {
			hookSpeed = 10;
		} else {
			hookSpeed = 0;
		}
		if (Input.GetKey (KeyCode.E)) {
			objectSpeed = 20;
		} else {
			objectSpeed = 0;
		}
		if (Input.GetButtonDown ("Fire1")) 
		{	
			
			Ray ray = Camera.main.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0f));
			if (Physics.Raycast (ray, out hit)) {
				point = Instantiate (hitPos, hit.point, hit.transform.rotation);
				target = point;
				playerBody.useGravity = false;

				if (condition) {
					firstObject = hit.collider.gameObject;
					condition = false;
				} else {
					playerBody.useGravity = true;
					lineRender.enabled = false;
					target = null;
					secondObject = hit.collider.gameObject;
					condition = true;
				}
			}
		}
	}

	public void WriteGun()
	{
		curGunText.text = gunCount[selectedWeapon].gunName;
	}

	void OnCollisionEnter (Collision col)
	{
		playerBody.useGravity = true;
		target = null;
	}
}
