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
        if (m_Player.m_WorkerGold < 5)
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
        if (m_Player.m_WorkerSpeed < 5)
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
        if (m_Player.m_MeleeArmour < 5)
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
        if (m_Player.m_MeleeSpeed < 5)
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
        if (m_Player.m_RangedArmour < 5)
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
        if (m_Player.m_RangedSpeed < 5)
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
        if (m_Player.m_AirArmour < 5)
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
        if (m_Player.m_AirSpeed < 5)
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
                transform.FindChild("UnitUpgrade/Worker/GoldUpgradeCount").GetComponent<Text>().text = m_Player.m_WorkerGold + "/5";
                transform.FindChild("UnitUpgrade/Worker/MovementUpgradeCount").GetComponent<Text>().text = m_Player.m_WorkerSpeed + "/5";

                transform.FindChild("UnitUpgrade/Melee/ArmourUpgradeCount").GetComponent<Text>().text = m_Player.m_MeleeArmour + "/5";
                transform.FindChild("UnitUpgrade/Melee/MovementUpgradeCount").GetComponent<Text>().text = m_Player.m_MeleeSpeed + "/5";

                transform.FindChild("UnitUpgrade/Ranged/ArmourUpgradeCount").GetComponent<Text>().text = m_Player.m_RangedArmour + "/5";
                transform.FindChild("UnitUpgrade/Ranged/MovementUpgradeCount").GetComponent<Text>().text = m_Player.m_RangedSpeed + "/5";

                transform.FindChild("UnitUpgrade/Air/ArmourUpgradeCount").GetComponent<Text>().text = m_Player.m_AirArmour + "/5";
                transform.FindChild("UnitUpgrade/Air/MovementUpgradeCount").GetComponent<Text>().text = m_Player.m_AirSpeed + "/5";
            }
        }
        else
        {
            Debug.LogWarning("Could not find Game_HUD/Unit");
        }
    }
}
