using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManagerCh : MonoBehaviour {

	CharacterController controller;
	public float speed= 0.1f;
	public float sensibilite = 0.1f;
	public float intensiteGravite = 0.1f;
	public float jumpspeed = 10f;
    public float masse = 70;
    public Vector3 vitesse;
    public Vector3 acceleration;
    public Vector3 force;   
	public Vector3 g = new Vector3();
    public Vector3 lastMoving;
    Vector3 dir;

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
        vitesse = new Vector3();
        acceleration = new Vector3();
        force = new Vector3();
        dir = new Vector3();
        lastMoving = new Vector3();
	}


    void Update()
    {

        dir = Vector3.zero;
        if (Input.GetKey(KeyCode.Z))
        {
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


        if (Input.GetKeyDown(KeyCode.I))
        {
            g = new Vector3(0, 1, 0);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            g = new Vector3(0, -1, 0);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            g = new Vector3(1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            g = new Vector3(-1, 0, 0);
        }


        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");

        this.transform.GetChild(0).Rotate(new Vector3(0, sensibilite * deltaX, 0), Space.Self);
        this.transform.GetChild(0).GetChild(0).Rotate(new Vector3(sensibilite * -deltaY, 0, 0), Space.Self);
        // Si on regarde trop haut ou trop bas on annule la rotation
        if (Vector3.Angle(this.transform.GetChild(0).GetChild(0).forward, this.transform.up) < 10 || Vector3.Angle(this.transform.GetChild(0).GetChild(0).forward, -this.transform.up) < 10)
            this.transform.GetChild(0).GetChild(0).Rotate(new Vector3(sensibilite * deltaY, 0, 0), Space.Self);
    }

    // Update is called once per frame
    void FixedUpdate () {
        force = Vector3.zero;
        acceleration = Vector3.zero;
		lastPosition = transform.position;
		if (transform.up != -g && g != Vector3.zero) {
			rotationDep = transform.rotation;
            
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
		}


        //Gravité
        force += intensiteGravite*g;

        Vector3 going = transform.GetChild(0).right * dir.x + transform.GetChild(0).forward * dir.z;
        //rigidbody.velocity += intensiteGravite * g.normalized * Time.deltaTime;
        //rigidbody.velocity += speed * going.normalized * Time.deltaTime;
        //lastdir = dir;
        force -= speed * lastMoving.normalized;
        force += speed * going.normalized;

        lastMoving = going;

        acceleration += force / masse;
        vitesse += acceleration;
        controller.Move(vitesse);

        //Debug.Log (transform.position);


	}
}
