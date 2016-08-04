﻿using UnityEngine;
using System.Collections;

public class SubmarineController : MonoBehaviour {

	bool turnsRight = false;
	Rigidbody2D rb;

	public int MoveSpeed;
	public GameObject Ship;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		float dist = Vector3.Distance (transform.position, Ship.transform.position);
		if(dist < 3){
			if (Input.GetKeyDown (KeyCode.Space)) {
				DockAtShip ();
			}
		}
	}

	void FixedUpdate(){
		float x = Input.GetAxis ("Horizontal") * Time.fixedDeltaTime;
		float y = Input.GetAxis ("Vertical") * Time.fixedDeltaTime;

		if(y > 0 && transform.position.y >= 0){
			y = 0;
		}
		if(x > 0){
			turnsRight = true;
		}
		else{
			turnsRight = false;
		}
		rb.velocity = new Vector2 (x, y).normalized * MoveSpeed;
	}

	void DockAtShip(){
		GetComponent<BoxCollider2D> ().enabled = false;
		transform.SetParent (Ship.transform, true);
		Camera.main.gameObject.transform.SetParent (Ship.transform);
		Ship.GetComponent<ShipController> ().enabled = true;
		transform.position = Ship.GetComponent<ShipController> ().SubHolder.position;
		rb.Sleep ();
		Ship.GetComponent<Rigidbody2D> ().WakeUp ();
		this.enabled = false;
	}
}