using UnityEngine;
using System.Collections;

public class Upgrade : MonoBehaviour
{
    public GameObject Panel;
    public GameObject House;
    public bool PausePanel = false;





    void Update()
    {
        if (PausePanel == false)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Panel.SetActive(true);
                PausePanel = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            Panel.SetActive(false);
            PausePanel = false;
        }
        

    }
}
