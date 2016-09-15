using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
	public Transform Panel;
	public Transform UpdatePoint;
	public Transform Ship;
	public GameObject upgradeButtonPrefab;

	void Start () {
		for(int i = 0; i < UpgradeList.Length; i++) {
			UpgradeDta u2 = UpgradeList [i];
			GameObject btn = ((GameObject)Instantiate(upgradeButtonPrefab));
			btn.transform.SetParent(Panel, false);
			btn.GetComponent<Button>().onClick.AddListener( () => { ProcessUpdate(u2,btn); } );
			btn.GetComponent<Image> ().sprite = u2.Icon;
		}
	}


    void Update()
    {
		if (!Panel.gameObject.activeSelf)
        {
			if (Vector3.Distance (Ship.position, UpdatePoint.position) < 10) {
				if (Input.GetKeyDown (KeyCode.Tab)) {
					Panel.gameObject.SetActive (true);
					Time.timeScale = 0;
				}
			}
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
			Panel.gameObject.SetActive(false);
			Time.timeScale = 1;
        }
    }

	[Serializable]
	public class UpgradeDta{
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

	public UpgradeDta[] UpgradeList = {
        new UpgradeDta("10 More Pressure Max", 200, null, (u) => {AddPresMax(10);}),
        new UpgradeDta("10 More Bat Max", 200, null, (u) => {AddBatMax(10);}),
        new UpgradeDta("10 More OX Max", 200, null, (u) => {AddOXMax(10);})
	};

    void ProcessUpdate(UpgradeDta upgrade, GameObject go){
		if(ScoreManager.Instance.Score < upgrade.Cost){
			return;
		}

		ScoreManager.Instance.Score -= upgrade.Cost;
		upgrade.OnUnlock (upgrade);
        Destroy(go);
	}

    static void AddPresMax(int amt){
        Stats.Instance.maxPres += amt;
        UIElements.Instance.pres.maxValue += amt;
    }

    static void AddBatMax(int amt){
        Stats.Instance.maxBat += amt;
        UIElements.Instance.bat.maxValue += amt;
    }

    static void AddOXMax(int amt){
        Stats.Instance.maxOX += amt;
        UIElements.Instance.ox.maxValue += amt;
    }
}
