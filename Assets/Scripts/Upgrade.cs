using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public Transform Panel;
	public GameObject upgradeButtonPrefab;

	void Start () {
		for(int i = 0; i < UpgradeList.Length; i++) {
			UpgradeData u2 = UpgradeList [i];
			GameObject btn = ((GameObject)Instantiate(upgradeButtonPrefab));
			btn.transform.SetParent(Panel, false);
			btn.GetComponent<Button>().onClick.AddListener( () => { ProcessUpdate(u2,btn); } );
			btn.GetComponent<Image> ().sprite = u2.Icon;
		}
	}

	[Serializable]
	public class UpgradeData{
		public string Text ="";
		public int Cost = 0;
		public Sprite Icon = null;
		public Action<UpgradeData> OnUnlock = null;

		public UpgradeData(string text, int cost, Sprite icon, Action<UpgradeData> onUnlock){
			Text = text;
			Cost = cost;
			Icon = icon;
			OnUnlock = onUnlock;
		}
	}

	public UpgradeData[] UpgradeList = {
        new UpgradeData("10 More Pressure Max", 200, null, (u) => {AddPresMax(10);}),
        new UpgradeData("10 More Bat Max", 200, null, (u) => {AddBatMax(10);}),
        new UpgradeData("10 More OX Max", 200, null, (u) => {AddOXMax(10);})
	};

    void ProcessUpdate(UpgradeData upgrade, GameObject go){
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
