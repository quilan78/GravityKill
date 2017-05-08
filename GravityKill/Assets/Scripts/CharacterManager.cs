using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

	Rigidbody rigidBody;
	public float speed=2f;
	public float sensibilite = 0.1f;
	float mouseX =0;
	float mouseY = 0;
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		mouseX = Input.mousePosition.x;
		mouseY = Input.mousePosition.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey (KeyCode.Z)) {
			rigidBody.AddForce (speed * transform.forward, ForceMode.Impulse);
		}
		if (Input.GetKey (KeyCode.Q)) {
			rigidBody.AddForce (speed *  -transform.right, ForceMode.Impulse);
		}
		if (Input.GetKey (KeyCode.D)) {
			rigidBody.AddForce (speed * transform.right, ForceMode.Impulse);
		}
		if (Input.GetKey (KeyCode.S)) {
			rigidBody.AddForce (speed *  -transform.forward, ForceMode.Impulse);
		}
		float deltaX = Input.mousePosition.x - mouseX;
		float deltaY = Input.mousePosition.y - mouseY;
		mouseX = Input.mousePosition.x;
		mouseY = Input.mousePosition.y;
		Cursor.lockState = CursorLockMode.Confined;
		Quaternion vert = Quaternion.Euler (0, mouseY, 0);
		Quaternion hori = Quaternion.Euler (0, 0, mouseX);
		this.transform.Rotate(new Vector3(0, sensibilite*deltaX, 0));
		this.transform.GetChild (0).Rotate (new Vector3 (sensibilite*-deltaY, 0, 0));
	}
}
