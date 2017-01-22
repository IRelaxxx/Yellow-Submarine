using UnityEngine;
using System.Collections;

public class SharkAI : MonoBehaviour {

	//Assumes that points are at the same height
	public Transform Point1; //left point
	public Transform Point2; //right point

    public int Damage = 10;

	bool turnsRight = true;
	Rigidbody2D rb;
	float rangeLow;
	float rangeHigh;

	void Start(){
		rb = GetComponent<Rigidbody2D> ();
		rangeLow = Point1.transform.position.y + 0.2f;
		rangeHigh = Point1.transform.position.y - 0.2f;
	}

	void FixedUpdate(){
		if(turnsRight == false){
			rb.velocity = new Vector2 (-1, 0) * Time.fixedDeltaTime * Stats.GetInstance.SharkSpeed;
			if (Dist (transform.position.x, Point1.position.x) < 0.5f) {
				turnsRight = true;
				transform.rotation = Quaternion.Euler (0, 0, 0);
			}
		}
		if (turnsRight == true){
			rb.velocity = new Vector2 (1, 0) * Time.fixedDeltaTime * Stats.GetInstance.SharkSpeed;
			if (Dist (transform.position.x, Point2.position.x) < 0.5f) {
				turnsRight = false;
				transform.rotation = Quaternion.Euler (0, 180, 0);
			}
		}

		if(transform.position.y < rangeLow){
			rb.velocity += Vector2.up * Time.fixedDeltaTime * Stats.GetInstance.SharkSpeed;
		}
		if(transform.position.y > rangeHigh){
			rb.velocity += Vector2.down * Time.fixedDeltaTime * Stats.GetInstance.SharkSpeed;
		}
	}

	void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.GetComponent<SubmarineController>() != null){
            col.gameObject.GetComponent<SubmarineController>().Damage(Damage);
		}
	}

	float Dist(float p1,float p2){
		return Mathf.Abs(p1 - p2);
	}
}
