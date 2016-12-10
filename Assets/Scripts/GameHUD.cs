using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameHUD : MonoBehaviour {

    [HideInInspector]
    public Player m_Player;

    private Text m_UnitUpgradeNotEnoughGold;

    void Start()
    {
        m_UnitUpgradeNotEnoughGold = GameObject.Find("Game_HUD/UnitUpgrade/NotEnoughGold").GetComponent<Text>();
    }

    public void spawnWorker()
    {
        if (m_Player.m_Gold >= Prices.m_WorkerPrice)
        {
            m_Player.GetComponent<Player>().CmdSpawnWorker();
        }
        else
        {
            m_Player.DisplayNotEnoughGold();
        }
    }

    public void spawnMelee()
    {
        if (m_Player.m_Gold >= Prices.m_MeleePrice)
        {
            m_Player.GetComponent<Player>().CmdSpawnMelee();
        }
        else
        {
            m_Player.DisplayNotEnoughGold();
        }
    }

    public void spawnRanged()
    {
        if (m_Player.m_Gold >= Prices.m_RangedPrice)
        {
            m_Player.GetComponent<Player>().CmdSpawnRanged();
        }
        else
        {
            m_Player.DisplayNotEnoughGold();
        }
    }

    public void upgradeWorkerGold()
    {
        if (m_Player.m_Upgrades.workerGold < 5)
        {
            m_Player.GetComponent<Player>().CmdUpgradeWorkerGold();
        }
        else
        {
            DisplayNotEnoughGoldUpgrade();
        }
    }

    public void upgradeWorkerSpeed()
    {
        if (m_Player.m_Upgrades.workerSpeed < 5)
        {
            m_Player.GetComponent<Player>().CmdUpgradeWorkerSpeed();
        }
        else
        {
            DisplayNotEnoughGoldUpgrade();
        }
    }

    public void upgradeMeleeArmour()
    {
        if (m_Player.m_Upgrades.meleeArmour < 5)
        {
            m_Player.GetComponent<Player>().CmdUpgradeMeleeArmour();
        }
        else
        {
            DisplayNotEnoughGoldUpgrade();
        }
    }

    public void upgradeMeleeSpeed()
    {
        if (m_Player.m_Upgrades.meleeSpeed < 5)
        {
            m_Player.GetComponent<Player>().CmdUpgradeMeleeSpeed();
        }
        else
        {
            DisplayNotEnoughGoldUpgrade();
        }
    }

    public void upgradeRangedArmour()
    {
        if (m_Player.m_Upgrades.rangedArmour < 5)
        {
            m_Player.GetComponent<Player>().CmdUpgradeRangedArmour();
        }
        else
        {
            DisplayNotEnoughGoldUpgrade();
        }
    }

    public void upgradeRangedSpeed()
    {
        if (m_Player.m_Upgrades.rangedSpeed < 5)
        {
            m_Player.GetComponent<Player>().CmdUpgradeRangedSpeed();
        }
        else
        {
            DisplayNotEnoughGoldUpgrade();
        }
    }

    public void upgradeAirArmour()
    {
        if (m_Player.m_Upgrades.airArmour < 5)
        {
            m_Player.GetComponent<Player>().CmdUpgradeAirArmour();
        }
        else
        {
            DisplayNotEnoughGoldUpgrade();
        }
    }

    public void upgradeAirSpeed()
    {
        if (m_Player.m_Upgrades.airSpeed < 5)
        {
            m_Player.GetComponent<Player>().CmdUpgradeAirSpeed();
        }
        else
        {
            DisplayNotEnoughGoldUpgrade();
        }
    }

    public void DisplayNotEnoughGoldUpgrade()
    {
        StopCoroutine(NotEnoughGoldTimer());
        StartCoroutine(NotEnoughGoldTimer());
    }

    IEnumerator NotEnoughGoldTimer()
    {
        m_UnitUpgradeNotEnoughGold.enabled = true;
        yield return new WaitForSeconds(3);
        m_UnitUpgradeNotEnoughGold.enabled = false;
    }


    void Update()
    {
        updateUnitText();
        updateUnitUpgradeText();
    }

    private void updateUnitText()
    {
        if (transform.FindChild("Unit") != null)
        {
            if (transform.FindChild("Unit").FindChild("Info") != null)
            {
                Text hudText = transform.FindChild("Unit").FindChild("Info").GetComponent<Text>();
                if (m_Player != null)
                {
                    hudText.text = "Gold: " + m_Player.m_Gold + "\nWorkers: " + m_Player.m_WorkerCount;
                }
            }
            else
            {
                Debug.LogWarning("Could not find Game_HUD/Unit/Info");
            }
        }
        else
        {
            Debug.LogWarning("Could not find Game_HUD/Unit");
        }
    }

    private void updateUnitUpgradeText()
    {
        //gold
        if (transform.FindChild("UnitUpgrade") != null)
        {
            if (transform.FindChild("UnitUpgrade").FindChild("Info") != null)
            {
                Text hudText = transform.FindChild("UnitUpgrade").FindChild("Info").GetComponent<Text>();
                if (m_Player != null)
                {
                    hudText.text = "Gold: " + m_Player.m_Gold;
                }
            }
            else
            {
                Debug.LogWarning("Could not find Game_HUD/Unit/Info");
            }
            if (m_Player != null)
            {
                transform.FindChild("UnitUpgrade/Worker/Gold/UpgradeCount").GetComponent<Text>().text = m_Player.m_Upgrades.workerGold + "/5";
                transform.FindChild("UnitUpgrade/Worker/Movement/UpgradeCount").GetComponent<Text>().text = m_Player.m_Upgrades.workerSpeed + "/5";

                transform.FindChild("UnitUpgrade/Melee/Armour/UpgradeCount").GetComponent<Text>().text = m_Player.m_Upgrades.meleeArmour + "/5";
                transform.FindChild("UnitUpgrade/Melee/Movement/UpgradeCount").GetComponent<Text>().text = m_Player.m_Upgrades.meleeSpeed + "/5";

                transform.FindChild("UnitUpgrade/Ranged/Armour/UpgradeCount").GetComponent<Text>().text = m_Player.m_Upgrades.rangedArmour + "/5";
                transform.FindChild("UnitUpgrade/Ranged/Movement/UpgradeCount").GetComponent<Text>().text = m_Player.m_Upgrades.rangedSpeed + "/5";

                transform.FindChild("UnitUpgrade/Air/Armour/UpgradeCount").GetComponent<Text>().text = m_Player.m_Upgrades.airArmour + "/5";
                transform.FindChild("UnitUpgrade/Air/Movement/UpgradeCount").GetComponent<Text>().text = m_Player.m_Upgrades.airSpeed + "/5";
            }
        }
        else
        {
            Debug.LogWarning("Could not find Game_HUD/Unit");
        }
    }
}
