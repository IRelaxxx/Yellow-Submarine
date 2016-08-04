using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

	bool turnsRight = false;
	Rigidbody2D rb;

	public int MoveSpeed;
	public GameObject Sub;
	public Transform SubHolder;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			ReleaseSub ();
		}
	}

	void FixedUpdate(){
		float x = Input.GetAxis ("Horizontal") * Time.fixedDeltaTime;

		if(x > 0){
			turnsRight = true;
		}
		else{
			turnsRight = false;
		}
		rb.velocity = new Vector2 (x, 0).normalized * MoveSpeed;
		print (new Vector2 (x, 0).normalized);
	}

	void ReleaseSub(){
		Sub.transform.SetParent (null, true);
		Camera.main.gameObject.transform.SetParent (Sub.transform);
		Sub.transform.position -= Vector3.down * -2;
		rb.Sleep ();
		Sub.GetComponent<Rigidbody2D> ().WakeUp ();
		Sub.GetComponent<BoxCollider2D> ().enabled = true;
		Sub.GetComponent<SubmarineController> ().enabled = true;
		this.enabled = false;
	}
}
