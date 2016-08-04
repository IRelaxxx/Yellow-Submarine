using UnityEngine;
using System.Collections;

public class SharkAI : MonoBehaviour {

	//Assumes that points are at the same height
	public Transform Point1; //left point
	public Transform Point2; //right point
	public float MoveSpeed;

	bool turnsRight = true;
	Rigidbody2D rb;

	void Start(){
		rb = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate(){
		if(turnsRight == false){
			rb.velocity = new Vector2 (-1, 0) * Time.fixedDeltaTime * MoveSpeed;
			if (Vector3.Distance (transform.position, Point1.position) < 0.5f) {
				turnsRight = true;
				transform.rotation = Quaternion.Euler (0, 0, 0);
			}
		}
		if (turnsRight == true){
			rb.velocity = new Vector2 (1, 0) * Time.fixedDeltaTime * MoveSpeed;
			if (Vector3.Distance (transform.position, Point2.position) < 0.5f) {
				turnsRight = false;
				transform.rotation = Quaternion.Euler (0, 180, 0);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.GetComponent<ShipController>() != null){
			//col.gameObject.GetComponent<ShipController> (); Take Damage
		}
	}
}
