using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicManager : MonoBehaviour {

	public Vector3 g; // Vecteur représentant la direction de g pour ce GO
	public bool isAttracted; // Dit si le GO est attiré/repousse par les balises
	Rigidbody rigidBody;
	// Use this for initialization
	void Start () {
		rigidBody = this.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 force = new Vector3 ();
		if (isAttracted) {
			force += ComputeBaliseForces ();
		}
		force += rigidBody.mass* 300 * g.normalized;
		force += -10f *rigidBody.velocity;
		//rigidBody.velocity += 0.01f * force; 
		rigidBody.AddForce(0.2f * force);
	}

	Vector3 ComputeBaliseForces() {
		Vector3 force = new Vector3 ();
		GameObject baliseContainer = GameObject.Find ("baliseContainer");
		for (int i = 0; i < baliseContainer.transform.childCount; i++) {
			GameObject child = baliseContainer.transform.GetChild (i).gameObject;
			if (child.activeSelf) {
				Vector3 dir = (child.transform.position - this.transform.position).normalized; // Direction de la force
				dir *= child.GetComponent<BaliseManager>().puissance; // Rajout de la puissance de la force
				force += dir;
			}
		}
		return force;
	}
}
