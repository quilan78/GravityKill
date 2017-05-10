using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManagerCh : MonoBehaviour {

	CharacterController controller;
	public float speed= 0.1f;
	public float sensibilite = 0.1f;
	public float intensiteGravite = 0.1f;
	public float jumpspeed = 10f;
	public Vector3 g = new Vector3();

	Quaternion rotationDep;
	Quaternion rotationArr;
	public float SensibiliteRotation = 0.1f;

	Vector3 lastPosition = new Vector3();
	float fallingSpeed = 1.0f;
	public float incrementChute = 0.1f;
	public float incrementMaxChute = 3.0f;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		Cursor.lockState = CursorLockMode.Locked;
	}

	// Update is called once per frame
	void FixedUpdate () {
		lastPosition = transform.position;
		if (transform.up != -g && g != Vector3.zero) {
			rotationDep = transform.rotation;

			/*Debug.Log (transform.forward);
			Debug.Log (transform.right);
			Debug.Log (g);

			if (g == this.transform.forward) {
				transform.Rotate (this.transform.right, 90);
				rotationArr = transform.rotation;
				transform.Rotate (this.transform.right, 90);
			}
			if ( g == -this.transform.forward) {
				transform.Rotate (this.transform.right, 90);
				rotationArr = transform.rotation;
				transform.Rotate (-this.transform.right, 90);
			}
			if (g == this.transform.right) {
				transform.Rotate (this.transform.forward, 90);
				rotationArr = transform.rotation;
				transform.Rotate (-this.transform.forward, 90);
			}
			if (g == -this.transform.right) {
				transform.Rotate (-this.transform.forward, 90);
				rotationArr = transform.rotation;
				transform.Rotate (this.transform.forward, 90);
			}
			if (g == this.transform.up) {
				transform.Rotate (this.transform.forward, 180);
				rotationArr = transform.rotation;
				transform.Rotate (-this.transform.forward, 180);
			}*/
			Vector3 save = transform.up;
			Vector3 save2 = transform.forward;
			this.transform.up = -g;
			bool rot = false;
			if (this.transform.forward == -save2) {
				transform.Rotate (this.transform.up, 180);
				rot = true;
			}
			rotationArr = transform.rotation;
			if ( rot ) 
				transform.Rotate (this.transform.up, 180);
			transform.up = save;
			transform.rotation = Quaternion.Lerp (rotationDep, rotationArr, SensibiliteRotation);
			//this.transform.GetChild (0).LookAt (toLook);
		}
		if (Input.GetKey (KeyCode.Z)) {
			controller.Move (speed * transform.GetChild(0).forward);
		}
		if (Input.GetKey (KeyCode.Q)) {
			controller.Move (speed *  -transform.GetChild(0).right);
		}
		if (Input.GetKey (KeyCode.D)) {
			controller.Move (speed * transform.GetChild(0).right);
		}
		if (Input.GetKey (KeyCode.S)) {
			controller.Move (speed *  -transform.GetChild(0).forward);
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			controller.Move (jumpspeed * transform.up);
		}

		//Debug.Log (lastPosition);
		//Debug.Log (transform.position);
		Debug.Log (fallingSpeed);
		//Debug.Log (Vector3.Dot (lastPosition, g));
		//Debug.Log (Vector3.Dot (transform.position, g));
		controller.Move (fallingSpeed * intensiteGravite * g.normalized);

		if (Vector3.Dot (lastPosition, g) < Vector3.Dot (transform.position, g) && fallingSpeed < incrementMaxChute) {
			fallingSpeed = fallingSpeed + incrementChute;
		} else if ( Vector3.Dot (lastPosition, g) == Vector3.Dot (transform.position, g) ){
			fallingSpeed = 1.0f;
		}

		//Debug.Log (transform.position);
		float deltaX = Input.GetAxis("Mouse X");
		float deltaY = Input.GetAxis("Mouse Y");

		if (Input.GetKeyDown (KeyCode.I)) {
			g = new Vector3 (0, 1, 0);
		}
		if (Input.GetKeyDown (KeyCode.J)) {
			g = new Vector3 (0, -1, 0);
		}
		if (Input.GetKeyDown (KeyCode.H)) {
			g = new Vector3 (1, 0, 0);
		}
		if (Input.GetKeyDown (KeyCode.K)) {
			g = new Vector3 (-1, 0, 0);
		}

		this.transform.GetChild (0).Rotate(new Vector3(0, sensibilite*deltaX, 0), Space.Self);
		this.transform.GetChild (0).GetChild(0).Rotate (new Vector3 (sensibilite*-deltaY, 0, 0), Space.Self);
		// Si on regarde trop haut ou trop bas on annule la rotation
		if ( Vector3.Angle(this.transform.GetChild(0).GetChild(0).forward, this.transform.up) < 10 || Vector3.Angle(this.transform.GetChild(0).GetChild(0).forward, -this.transform.up) < 10 ) 
			this.transform.GetChild (0).GetChild(0).Rotate (new Vector3 (sensibilite*deltaY, 0, 0), Space.Self);

	}
}
