using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

	private static Stats instance;
	public static Stats GetInstance {
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

    private int score;
    public int Score { get { return score; } set { score = value; UIElements.GetInstance.ScoreText.text = score.ToString(); } }
}
