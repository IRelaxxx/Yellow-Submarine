using UnityEngine;

//Handles opening and closing the upgrade panel
public class UpdateHouse : MonoBehaviour
{
    public Transform Panel;
    public Transform Ship;

    private void OnMouseDown()
    {
        if (!Panel.gameObject.activeSelf)
        {
            if (Vector3.Distance(Ship.position, transform.position) < 10)
            {
                Panel.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void CloseUpdatePanel()
    {
        Panel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
