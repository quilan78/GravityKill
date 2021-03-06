﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManagerRb : MonoBehaviour {

	Rigidbody rigidbody;
	public float speed= 1f;
	public float sensibilite = 2f;
	public float intensiteGravite = 10f;
	public float jumpspeed = 20f;
	public Vector3 g = new Vector3();
    Vector3 lastMoving = new Vector3();
    Vector3 dir = new Vector3();

    bool[] moving = new bool[4];

	Quaternion rotationDep;
	Quaternion rotationArr;
	public float SensibiliteRotation = 0.1f;
	// Use this for initialization
	void Start () {
        //rigidbody = this.transform.GetChild(0).GetComponent<Rigidbody>();
        rigidbody = this.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;

        for(int i=0; i<4; i++)
        {
            moving[i] = false;
        }
	}

    // Update is called once per frame
    void Update() {
        
       
        Vector3 dirForce = new Vector3();
        Vector3 slowForce = new Vector3();

        dir = Vector3.zero;
        if (Input.GetKey(KeyCode.Z)) {
            dir.z += 1;
		}
		if (Input.GetKey(KeyCode.Q))
        {
            dir.x -= 1;
		}
        if (Input.GetKey(KeyCode.D))
        {
            dir.x += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dir.z -= 1;
        }
        if (Input.GetKeyDown (KeyCode.Space)) {
			rigidbody.AddForce (jumpspeed * transform.up, ForceMode.Impulse);
		}
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

    void FixedUpdate()
    {
        if (transform.up != -g && g != Vector3.zero)
        {
            rotationDep = transform.rotation;

            Vector3 save = transform.up;
            Vector3 save2 = transform.forward;
            this.transform.up = -g;
            bool rot = false;
            if (this.transform.forward == -save2)
            {
                transform.Rotate(this.transform.up, 180);
                rot = true;
            }
            rotationArr = transform.rotation;
            if (rot)
                transform.Rotate(this.transform.up, 180);
            transform.up = save;
            transform.rotation = Quaternion.Lerp(rotationDep, rotationArr, SensibiliteRotation);
            //this.transform.GetChild (0).LookAt (toLook);
        }

        Debug.Log(dir);
        //rigidbody.velocity -= Vector3.Dot(rigidbody.velocity, transform.GetChild(0).right) * transform.GetChild(0).right + Vector3.Dot(rigidbody.velocity, transform.GetChild(0).forward) * transform.GetChild(0).forward;
        Vector3 going = transform.GetChild(0).right * dir.x + transform.GetChild(0).forward * dir.z;
        //rigidbody.velocity += intensiteGravite * g.normalized * Time.deltaTime;
        //rigidbody.velocity += speed * going.normalized * Time.deltaTime;
        //lastdir = dir;
        rigidbody.AddForce(-speed * lastMoving.normalized, ForceMode.Impulse);
        rigidbody.AddForce(speed * going.normalized, ForceMode.Impulse);
        rigidbody.AddForce(intensiteGravite * g.normalized, ForceMode.Impulse);
        lastMoving = going;
        //rigidbody.AddForce(speed * slowForce * Vector3.Dot(slowForce, rigidbody.velocity))
    }
    
}
