using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private Stats stats;
    private SubmarineController sub;
    private GameObject ship;
    private ShipController scontroller;

    private void Start()
    {
        stats = Stats.GetInstance;
        sub = GameObject.Find("Submarine").GetComponent<SubmarineController>();
        ship = GameObject.Find("ShipyMcShipFace");
        scontroller = ship.GetComponent<ShipController>();
    }

    public void SaveGame()
    {
        //Position
        PlayerPrefs.SetFloat("Sub_X", sub.gameObject.transform.position.x);
        PlayerPrefs.SetFloat("Sub_Y", sub.gameObject.transform.position.y);

        PlayerPrefs.SetFloat("Ship_X", ship.transform.position.x);
        PlayerPrefs.SetFloat("Ship_Y", ship.transform.position.y);
        PlayerPrefs.SetInt("subDocked", scontroller.subDocked? 1 : 0);

        //Score
        PlayerPrefs.SetInt("Score", stats.Score);

        //Supplies, Pressure is calculated at runtime
        PlayerPrefs.SetFloat("OX", sub.OX);
        PlayerPrefs.SetFloat("Bat", sub.Bat);

        //Upgrades
        bool[] upgrades = Upgrade.GetInstance.UpgradeAcquired;
        for(int i = 0; i < upgrades.Length; i++)
        {
            PlayerPrefs.SetInt("Upgrade_" + i.ToString(), upgrades[i] ? 1 : 0);
        }

        //TODO: Save shark pos
        //TODO: Grabing, save before?
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        //Use local copy of upgrade?
        int count = Upgrade.GetInstance.UpgradeCount;
        for(int i = 0; i < count; i++)
        {
            if(PlayerPrefs.GetInt("Upgrade_" + i.ToString()) == 1)
            {
                Upgrade.GetInstance.ApplyUpgrade(i);
            }
        }
        Upgrade.GetInstance.RebuildUpgradeUI();
        //TODO: Level defaults
        scontroller.subDocked = (PlayerPrefs.GetInt("subDocked") == 1);//True if docked else false

        if (scontroller.subDocked == false)
        {
            scontroller.ReleaseSub(false);
            sub.gameObject.transform.position = new Vector3(PlayerPrefs.GetFloat("Sub_X"), PlayerPrefs.GetFloat("Sub_Y"), 0);
        }
        ship.transform.position = new Vector3(PlayerPrefs.GetFloat("Ship_X"), PlayerPrefs.GetFloat("Ship_Y"), 0);
        stats.Score = PlayerPrefs.GetInt("Score");
        sub.OX = PlayerPrefs.GetFloat("OX");
        sub.Bat = PlayerPrefs.GetFloat("Bat");

    }
}
