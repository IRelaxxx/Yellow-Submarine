using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

	private static Stats instance;
	public static Stats Instance {
		get{
			if(instance == null){
				instance = GameObject.FindObjectOfType<Stats> ();
			}
			return instance;
		}
	}

	public float ShipSpeed;
	public float SubSpeed;
	public float GreifarmSpeed;
	public float SharkSpeed;

	public float maxBat;
	public float maxOX;
	public float maxPres;

	public float BatUseRate;
	public float OXUseRate;
}
