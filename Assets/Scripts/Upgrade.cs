using UnityEngine;
using System.Collections;
using System;

public class Upgrade : MonoBehaviour
{
    public GameObject Panel;
	public Transform UpdatePoint;
	public Transform Ship;

    void Update()
    {
		if (!Panel.activeSelf)
        {
			if (Vector3.Distance (Ship.position, UpdatePoint.position) < 10) {
				if (Input.GetKeyDown (KeyCode.Tab)) {
					Panel.SetActive (true);
					Time.timeScale = 0;
				}
			}
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            Panel.SetActive(false);
			Time.timeScale = 1;
        }
    }

	class UpgradeDta{
		public string Text ="";
		public int Cost = 0;
		public Sprite Icon = null;
		public Action<UpgradeDta> OnUnlock = null;

		public UpgradeDta(string text, int cost, Sprite icon, Action<UpgradeDta> onUnlock){
			Text = text;
			Cost = cost;
			Icon = icon;
			OnUnlock = onUnlock;
		}
	}

	UpgradeDta[] UpgradeList = {
		new UpgradeDta("10 More Pressure Max", 200, null, (u) => {Stats.Instance.maxPres += 10;})
	};

	void ProcessUpdate(GameObject button, UpgradeDta upgrade){

	}
}
