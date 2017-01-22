using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public Transform Panel;
    public GameObject upgradeButtonPrefab;

    private GameObject[] buttonGo;
    public bool[] UpgradeAcquired;

    private static Upgrade instance;
    public static Upgrade GetInstance { get { if (instance == null) instance = new GameObject("Upgrade").AddComponent<Upgrade>(); return instance; } }
    public int UpgradeCount;

    private void Awake()
    {
        instance = this;
    }

    void Start () {
        buttonGo = new GameObject[UpgradeList.Length];
        UpgradeAcquired = new bool[UpgradeList.Length];
        UpgradeCount = UpgradeList.Length;
		for(int i = 0; i < UpgradeList.Length; i++) {
			UpgradeData u2 = UpgradeList [i];
            u2.Id = i;
			GameObject btn = ((GameObject)Instantiate(upgradeButtonPrefab));
			btn.transform.SetParent(Panel, false);
			btn.GetComponent<Button>().onClick.AddListener( () => { ProcessUpdate(u2); } );
			btn.GetComponent<Image> ().sprite = u2.Icon;
            buttonGo[i] = btn;
            UpgradeAcquired[i] = false;
		}
	}

    public void ApplyUpgrade(int id)
    {
        UpgradeList[id].Cost = 0;//Prob not best idea to cheat like that
        ProcessUpdate(UpgradeList[id]);
    }

    public void RebuildUpgradeUI()
    {
        for(int i = 0; i < buttonGo.Length; i++)
        {
            Destroy(buttonGo[i]);
        }

        for (int i = 0; i < UpgradeList.Length; i++)
        {
            if (UpgradeAcquired[i] == false)
            {
                UpgradeData u2 = UpgradeList[i];
                //u2.Id = i;
                GameObject btn = ((GameObject)Instantiate(upgradeButtonPrefab));
                btn.transform.SetParent(Panel, false);
                btn.GetComponent<Button>().onClick.AddListener(() => { ProcessUpdate(u2); });
                btn.GetComponent<Image>().sprite = u2.Icon;
                buttonGo[i] = btn;
            }
        }
    }
	[Serializable]
	public class UpgradeData{
		public string Text ="";
		public int Cost = 0;
		public Sprite Icon = null;
		public Action<UpgradeData> OnUnlock = null;
        public int Id;

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

    void ProcessUpdate(UpgradeData upgrade){
		if(Stats.GetInstance.Score < upgrade.Cost){
			return;
		}

        Stats.GetInstance.Score -= upgrade.Cost;
		upgrade.OnUnlock (upgrade);
        UpgradeAcquired[upgrade.Id] = true;
        Destroy(buttonGo[upgrade.Id]);
	}

    static void AddPresMax(int amt){
        Stats.GetInstance.maxPres += amt;
        UIElements.GetInstance.pres.maxValue += amt;
    }

    static void AddBatMax(int amt){
        Stats.GetInstance.maxBat += amt;
        UIElements.GetInstance.bat.maxValue += amt;
    }

    static void AddOXMax(int amt){
        Stats.GetInstance.maxOX += amt;
        UIElements.GetInstance.ox.maxValue += amt;
    }
}
