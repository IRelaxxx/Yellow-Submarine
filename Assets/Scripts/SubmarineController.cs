using UnityEngine;
using System.Collections;

public class SubmarineController : MonoBehaviour {

	Rigidbody2D rb;

	public int MoveSpeed;
	public GameObject Ship;
	public Transform Greifarm;
	public float grabSpeed;

	public const float maxBat = 100;
	public float Bat = maxBat;
	public const float maxOX = 100;
	public float OX = maxOX;
	public const float maxPres = 100;
	public float Pres = 0;

	public float OXNeedRate = 0.1f; //Per second
	public float BatNeedRate = 1;


	bool block = false;
	bool down = false;
	bool up = false;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		ProcessNeeds ();

		if(Input.GetKeyDown(KeyCode.E)){
			if (up == false && block == false) {
				down = true;
				block = true;
			}
		}

		Grab ();

		if (block)
			return;
		float dist = Vector3.Distance (transform.position, Ship.transform.position);
		if(dist < 3){
			if (Input.GetKeyDown (KeyCode.Space)) {
				DockAtShip ();
			}
		}
	}

	void FixedUpdate(){
		if (block)
			return;
		
		float x = Input.GetAxis ("Horizontal") * Time.fixedDeltaTime;
		float y = Input.GetAxis ("Vertical") * Time.fixedDeltaTime;

		if(y > 0 && transform.position.y >= 0){
			y = 0;
		}
		rb.velocity = new Vector2 (x, y).normalized * MoveSpeed;
	}

	void DockAtShip(){
		GetComponent<BoxCollider2D> ().enabled = false;
		Greifarm.GetComponent<BoxCollider2D> ().enabled = false;
		transform.SetParent (Ship.transform, true);
		Ship.GetComponent<ShipController> ().enabled = true;
		transform.position = Ship.GetComponent<ShipController> ().SubHolder.position;
		rb.Sleep ();
		Ship.GetComponent<Rigidbody2D> ().WakeUp ();
		Ship.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation;
		Camera.main.GetComponent<Follow> ().target = Ship.transform;
		this.enabled = false;
	}

	void Grab(){
		if (down) {
			Greifarm.Translate (new Vector3 (-1, 0, 0) * Time.deltaTime * grabSpeed);
			if(Greifarm.localPosition.y <= -2){
				down = false;
				up = true;
			}
		}
		if(up){
			Greifarm.Translate (new Vector3 (1, 0, 0) * Time.deltaTime * grabSpeed);
			if(Greifarm.localPosition.y >= 0){
				up = false;
				Greifarm.transform.localPosition = new Vector3 (0, 0, 0);
				block = false;
			}
		}
	}

	public void ArmUp(){
		up = true;
		down = false;
	}

	void ProcessNeeds(){
		Pres = Mathf.Abs (transform.position.y);
		OX -= OXNeedRate * Time.deltaTime;
		Bat -= BatNeedRate * Time.deltaTime;

		if(transform.position.y >= 0){
			OX = maxOX;
		}

		if(Bat <= 0){
			Bat = 0;
			block = true;
			rb.velocity = Vector2.zero;
		}
		if(OX <= 0){
			OX = 0;
			block = true;
			rb.velocity = Vector2.zero;
		}
		if(Pres >= maxPres){
			block = true;
			rb.velocity = Vector2.zero;
		}
	}
}
