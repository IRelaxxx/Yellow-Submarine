using UnityEngine;
using System.Collections;

public class Greifarm : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Loot") {
			Destroy (col.gameObject);
			transform.parent.GetComponent<SubmarineController> ().ArmUp ();
			Stats.GetInstance.Score += 100;
		}
	}
}
