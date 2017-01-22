using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIElements : MonoBehaviour {

    private static UIElements instance;
    public static UIElements GetInstance{ get { return instance; } set { instance = value; } }

    public Slider pres;
    public Slider bat;
    public Slider ox;

    public Text ScoreText;

    void Awake(){
        instance = this;
    }
}
