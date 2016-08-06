using UnityEngine;
using System.Collections;

public class SharkAI : MonoBehaviour {

	//Assumes that points are at the same height
	public Transform Point1; //left point
	public Transform Point2; //right point

	bool turnsRight = true;
	Rigidbody2D rb;

	void Start(){
		rb = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate(){
		if(turnsRight == false){
			rb.velocity = new Vector2 (-1, 0) * Time.fixedDeltaTime * Stats.Instance.SharkSpeed;
			if (Dist (transform.position.x, Point1.position.x) < 0.5f) {
				turnsRight = true;
				transform.rotation = Quaternion.Euler (0, 0, 0);
			}
		}
		if (turnsRight == true){
			rb.velocity = new Vector2 (1, 0) * Time.fixedDeltaTime * Stats.Instance.SharkSpeed;
			if (Dist (transform.position.x, Point2.position.x) < 0.5f) {
				turnsRight = false;
				transform.rotation = Quaternion.Euler (0, 180, 0);
			}
		}

		if(transform.position.y < Point1.transform.position.y){
			rb.velocity += Vector2.up * Time.fixedDeltaTime * Stats.Instance.SharkSpeed;
		}
		if(transform.position.y > Point1.transform.position.y){
			rb.velocity += Vector2.down * Time.fixedDeltaTime * Stats.Instance.SharkSpeed;
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.GetComponent<ShipController>() != null){
			//col.gameObject.GetComponent<ShipController> (); Take Damage
		}
	}

	float Dist(float p1,float p2){
		return Mathf.Abs(p1 - p2);
	}
}
