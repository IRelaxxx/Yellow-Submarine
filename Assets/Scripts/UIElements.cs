using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIElements : MonoBehaviour {

    private static UIElements instance;
    public static UIElements Instance{ get { return instance; } set { instance = value; } }

    public Slider pres;
    public Slider bat;
    public Slider ox;

    void Awake(){
        instance = this;
    }
}
