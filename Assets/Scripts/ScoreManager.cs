using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	private static ScoreManager instance;
	public static ScoreManager Instance{
		get{
			if(instance == null){
				instance = GameObject.FindObjectOfType<ScoreManager>();
			}
			return instance;
		}
	}

	public int Score;
    public UnityEngine.UI.Text ScoreText;
}
