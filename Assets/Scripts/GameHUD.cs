using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameHUD : MonoBehaviour {

    [HideInInspector]
    public Player m_Player;

    private Text m_UnitUpgradeNotEnoughGold;

    private LineRenderer m_LineRenderer;

    Text workerCost;
    Text workerGoldUpgradeAmount;
    Text workerSpeedUpgradeAmount;
    Text workerGoldUpgradeCost;
    Text workerSpeedUpgradeCost;

    Text meleeCost;
    Text meleeArmourUpgradeAmount;
    Text meleeSpeedUpgradeAmount;
    Text meleeArmourUpgradeCost;
    Text meleeSpeedUpgradeCost;

    Text rangedCost;
    Text rangedArmourUpgradeAmount;
    Text rangedSpeedUpgradeAmount;
    Text rangedArmourUpgradeCost;
    Text rangedSpeedUpgradeCost;

    Text airCost;
    Text airArmourUpgradeAmount;
    Text airSpeedUpgradeAmount;
    Text airArmourUpgradeCost;
    Text airSpeedUpgradeCost;

    void Update()
    {
        updateUnitText();
        updateUnitUpgradeText();

        if(m_LineRenderer.enabled == true)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 0;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, -Vector2.up);

            if(hit.collider != null)
            {
                Vector3[] rendererPositions = new Vector3[2];
                rendererPositions[0] = new Vector3(mousePosition.x, mousePosition.y, -1);
                rendererPositions[1] = new Vector3(hit.point.x, hit.point.y, -1);

                m_LineRenderer.SetPositions(rendererPositions);


                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (m_Player.m_Gold >= Prices.m_AirPrice)
                    {
                        m_Player.CmdSpawnAir(rendererPositions[0], rendererPositions[1]);
                    }
                    else
                    {
                        m_LineRenderer.enabled = false;
                    }
                    m_LineRenderer.enabled = false;
                }
            }
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                m_LineRenderer.enabled = false;
            }
        }
    }

    void Start()
    {
        m_UnitUpgradeNotEnoughGold = GameObject.Find("Game_HUD/UnitUpgrade/NotEnoughGold").GetComponent<Text>();
        m_LineRenderer = GetComponent<LineRenderer>();
        m_LineRenderer.material = new Material(Shader.Find("Unlit/Texture"));
        m_LineRenderer.enabled = false;

        workerCost = transform.FindChild("Unit/WorkerCost").GetComponent<Text>();
        workerGoldUpgradeAmount = transform.FindChild("UnitUpgrade/Worker/GoldUpgradeCount").GetComponent<Text>();
        workerSpeedUpgradeAmount = transform.FindChild("UnitUpgrade/Worker/MovementUpgradeCount").GetComponent<Text>();
        workerGoldUpgradeCost = transform.FindChild("UnitUpgrade/Worker/Gold/Cost").GetComponent<Text>();
        workerSpeedUpgradeCost = transform.FindChild("UnitUpgrade/Worker/Movement/Cost").GetComponent<Text>();

        meleeCost = transform.FindChild("Unit/MeleeCost").GetComponent<Text>();
        meleeArmourUpgradeAmount = transform.FindChild("UnitUpgrade/Melee/ArmourUpgradeCount").GetComponent<Text>();
        meleeSpeedUpgradeAmount = transform.FindChild("UnitUpgrade/Melee/MovementUpgradeCount").GetComponent<Text>();
        meleeArmourUpgradeCost = transform.FindChild("UnitUpgrade/Melee/Armour/Cost").GetComponent<Text>();
        meleeSpeedUpgradeCost = transform.FindChild("UnitUpgrade/Melee/Movement/Cost").GetComponent<Text>();

        rangedCost = transform.FindChild("Unit/RangedCost").GetComponent<Text>();
        rangedArmourUpgradeAmount = transform.FindChild("UnitUpgrade/Ranged/ArmourUpgradeCount").GetComponent<Text>();
        rangedSpeedUpgradeAmount = transform.FindChild("UnitUpgrade/Ranged/MovementUpgradeCount").GetComponent<Text>();
        rangedArmourUpgradeCost = transform.FindChild("UnitUpgrade/Ranged/Armour/Cost").GetComponent<Text>();
        rangedSpeedUpgradeCost = transform.FindChild("UnitUpgrade/Ranged/Movement/Cost").GetComponent<Text>();

        airCost = transform.FindChild("Unit/AirCost").GetComponent<Text>();
        airArmourUpgradeAmount = transform.FindChild("UnitUpgrade/Air/ArmourUpgradeCount").GetComponent<Text>();
        airSpeedUpgradeAmount = transform.FindChild("UnitUpgrade/Air/MovementUpgradeCount").GetComponent<Text>();
        airArmourUpgradeCost = transform.FindChild("UnitUpgrade/Air/Armour/Cost").GetComponent<Text>();
        airSpeedUpgradeCost = transform.FindChild("UnitUpgrade/Air/Movement/Cost").GetComponent<Text>();
    }

    private void OnDestroy()
    {
        Destroy(m_LineRenderer.material);
    }

    public void spawnWorker()
    {
        if (m_Player.m_Gold >= Prices.m_WorkerPrice)
        {
            m_Player.CmdSpawnWorker();
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
            m_Player.CmdSpawnMelee();
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
            m_Player.CmdSpawnRanged();
        }
        else
        {
            m_Player.DisplayNotEnoughGold();
        }
    }

    public void spawnAir()
    {
        if(m_Player.m_Gold >= Prices.m_AirPrice)
        {
            m_LineRenderer.enabled = true;
        }
    }

    public void upgradeWorkerGold()
    {
        if (m_Player.m_WorkerGold < 5 && m_Player.m_Gold >= Prices.m_WorkerGold)
        {
            m_Player.CmdUpgradeWorkerGold();
        }
        else
        {
            DisplayNotEnoughGoldUpgrade();
        }
    }

    public void upgradeWorkerSpeed()
    {
        if (m_Player.m_WorkerSpeed < 5 && m_Player.m_Gold >= Prices.m_WorkerSpeed)
        {
            m_Player.CmdUpgradeWorkerSpeed();
        }
        else
        {
            DisplayNotEnoughGoldUpgrade();
        }
    }

    public void upgradeMeleeArmour()
    {
        if (m_Player.m_MeleeArmour < 5 && m_Player.m_Gold >= Prices.m_MeleeArmour)
        {
            m_Player.CmdUpgradeMeleeArmour();
        }
        else
        {
            DisplayNotEnoughGoldUpgrade();
        }
    }

    public void upgradeMeleeSpeed()
    {
        if (m_Player.m_MeleeSpeed < 5 && m_Player.m_Gold >= Prices.m_MeleeSpeed)
        {
            m_Player.CmdUpgradeMeleeSpeed();
        }
        else
        {
            DisplayNotEnoughGoldUpgrade();
        }
    }

    public void upgradeRangedArmour()
    {
        if (m_Player.m_RangedArmour < 5 && m_Player.m_Gold >= Prices.m_RangedArmour)
        {
            m_Player.CmdUpgradeRangedArmour();
        }
        else
        {
            DisplayNotEnoughGoldUpgrade();
        }
    }

    public void upgradeRangedSpeed()
    {
        if (m_Player.m_RangedSpeed < 5 && m_Player.m_Gold >= Prices.m_RangedSpeed)
        {
            m_Player.CmdUpgradeRangedSpeed();
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
            m_Player.CmdUpgradeAirArmour();
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
            m_Player.CmdUpgradeAirSpeed();
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

    private void updateUnitText()
    {
        if (transform.FindChild("Unit") != null)
        {
            if (m_Player != null)
            {
                if (m_Player.m_Gold < Prices.m_WorkerPrice)
                    workerCost.color = Color.red;
                else
                    workerCost.color = Color.green;
                if (m_Player.m_Gold < Prices.m_MeleePrice)
                    meleeCost.color = Color.red;
                else
                    meleeCost.color = Color.green;
                if (m_Player.m_Gold < Prices.m_RangedPrice)
                    rangedCost.color = Color.red;
                else
                    rangedCost.color = Color.green;
                if (m_Player.m_Gold < Prices.m_AirPrice)
                    airCost.color = Color.red;
                else
                    airCost.color = Color.green;
            }
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

                workerGoldUpgradeAmount.text = m_Player.m_WorkerGold + "/5";
                workerSpeedUpgradeAmount.text = m_Player.m_WorkerSpeed + "/5";
                workerGoldUpgradeCost.text = Prices.m_WorkerGold.ToString();
                workerSpeedUpgradeCost.text = Prices.m_WorkerSpeed.ToString();
                if(m_Player.m_Gold < Prices.m_WorkerGold)
                    workerGoldUpgradeCost.color = Color.red;
                else
                    workerGoldUpgradeCost.color = Color.green;

                if (m_Player.m_Gold < Prices.m_WorkerSpeed)
                    workerSpeedUpgradeCost.color = Color.red;
                else
                    workerSpeedUpgradeCost.color = Color.green;

                meleeArmourUpgradeAmount.text = m_Player.m_MeleeArmour + "/5";
                meleeSpeedUpgradeAmount.text = m_Player.m_MeleeSpeed + "/5";
                meleeArmourUpgradeCost.text = Prices.m_MeleeArmour.ToString();
                meleeSpeedUpgradeCost.text = Prices.m_MeleeSpeed.ToString();
                if (m_Player.m_Gold < Prices.m_MeleeArmour)
                    meleeArmourUpgradeCost.color = Color.red;
                else
                    meleeArmourUpgradeCost.color = Color.green;

                if (m_Player.m_Gold < Prices.m_MeleeSpeed)
                    meleeSpeedUpgradeCost.color = Color.red;
                else
                    meleeSpeedUpgradeCost.color = Color.green;

                rangedArmourUpgradeAmount.text = m_Player.m_RangedArmour + "/5";
                rangedSpeedUpgradeAmount.text = m_Player.m_RangedSpeed + "/5";
                rangedArmourUpgradeCost.text = Prices.m_RangedArmour.ToString();
                rangedSpeedUpgradeCost.text = Prices.m_RangedSpeed.ToString();
                if (m_Player.m_Gold < Prices.m_RangedArmour)
                    rangedArmourUpgradeCost.color = Color.red;
                else
                    rangedArmourUpgradeCost.color = Color.green;

                if (m_Player.m_Gold < Prices.m_RangedSpeed)
                    rangedSpeedUpgradeCost.color = Color.red;
                else
                    rangedSpeedUpgradeCost.color = Color.green;

                airArmourUpgradeAmount.text = m_Player.m_AirArmour + "/5";
                airSpeedUpgradeAmount.text = m_Player.m_AirSpeed + "/5";
                airArmourUpgradeCost.text = Prices.m_AirArmour.ToString();
                airSpeedUpgradeCost.text = Prices.m_AirSpeed.ToString();
                if (m_Player.m_Gold < Prices.m_AirArmour)
                    airArmourUpgradeCost.color = Color.red;
                else
                    airArmourUpgradeCost.color = Color.green;

                if (m_Player.m_Gold < Prices.m_AirSpeed)
                    airSpeedUpgradeCost.color = Color.red;
                else
                    airSpeedUpgradeCost.color = Color.green;
            }
        }
        else
        {
            Debug.LogWarning("Could not find Game_HUD/Unit");
        }
    }
}
