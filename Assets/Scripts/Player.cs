using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class Player : NetworkBehaviour
{
    [System.Serializable]
    public struct Upgrades
    {
        [SyncVar]
        public int workerGold;
        [SyncVar]
        public int workerSpeed;
        [SyncVar]
        public int meleeArmour;
        [SyncVar]
        public int meleeSpeed;
        [SyncVar]
        public int rangedArmour;
        [SyncVar]
        public int rangedSpeed;
        [SyncVar]
        public int airArmour;
        [SyncVar]
        public int airSpeed;
    }

    //Player base stats
    [SyncVar]
    public int m_Gold;
    [SyncVar]
    public int m_Health;
    [SyncVar]
    public int m_Armour;

    //Player Workercount
    [SyncVar]
    public int m_WorkerCount;

    //Unit Prefabs
    public GameObject m_Worker;
    public GameObject m_Melee;
    public GameObject m_Ranged;
    public GameObject m_Air;

    //Unit Animations
    private Animator m_Anim;

    //Not enough gold text
    private Text m_NotEnoughGold;

    //Unit Spawnpoint Prefabs
    public GameObject m_WorkerSpawn;
    public GameObject m_UnitSpawn;

    public Upgrades m_Upgrades;

    //Variable to sync the direction of the base
    SyncDirection m_Direction;

    void Awake()
    {
        m_Direction = GetComponent<SyncDirection>();
        m_NotEnoughGold = GameObject.Find("Game_HUD/Unit/NotEnoughGold").GetComponent<Text>();
        m_Anim = GameObject.Find("Game_HUD").transform.FindChild("Unit").GetComponent<Animator>();

        m_Upgrades.meleeSpeed = 0;
        m_Upgrades.meleeArmour = 0;
    }

    void Start()
    {
        //If player is not the current player playing, do nothing
        if (!isLocalPlayer)
        {
            return;
        }

        //Set this player to the player variable in the GameHUD (Used for running functions when this player presses a button in game)
        GameObject.Find("Game_HUD").GetComponent<GameHUD>().m_Player = this;

        //For determine which direction player 1 and 2 face
        m_Direction.m_FacingRight = true;
        if (GameObject.Find("SpawnLocationP2") != null)
        {
            if (GameObject.Find("SpawnLocationP2").gameObject.transform.position.x == transform.position.x)
            {
                //Camera.main.gameObject.transform.position = new Vector3(GameObject.Find("SpawnLocationP2").gameObject.transform.position.x, GameObject.Find("SpawnLocationP2").gameObject.transform.position.y, transform.position.z);
                m_Direction.m_FacingRight = false;
                Camera.main.GetComponent<MoveCamera>().setMaxRight();
            }
        }
        else
        {
            Camera.main.GetComponent<MoveCamera>().setMaxLeft();
        }
    }

    void Update()
    {
        m_Direction.Flip(m_Direction.m_FacingRight);
    }

    //Click the base
    public void OnMouseDown()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (m_Anim.GetBool("isClicked"))
        {
            m_Anim.SetBool("isClicked", false);
        }
        else
        {
            m_Anim.SetBool("isClicked", true);
        }
    }

    public void DisplayNotEnoughGold()
    {
        StopCoroutine(NotEnoughGoldTimer());
        StartCoroutine(NotEnoughGoldTimer());
    }

    IEnumerator NotEnoughGoldTimer()
    {
        m_NotEnoughGold.enabled = true;
        yield return new WaitForSeconds(3);
        m_NotEnoughGold.enabled = false;
    }

    IEnumerator SpawnWorkersAfterDelay()
    {
        yield return new WaitForSeconds(1);
        CmdSpawnWorker();
        CmdSpawnWorker();
        CmdSpawnWorker();
    }

    [Command]
    public void CmdSpawnWorker()
    {
        GameObject worker = Instantiate(m_Worker, m_WorkerSpawn.transform.position, m_Worker.transform.rotation) as GameObject;

        worker.GetComponent<Stats>().m_GoldUpgrade = m_Upgrades.workerGold;
        worker.GetComponent<Stats>().m_SpeedUpgrade = m_Upgrades.workerSpeed;

        NetworkServer.SpawnWithClientAuthority(worker, this.gameObject);

        m_Gold -= Prices.m_WorkerPrice;
        ++m_WorkerCount;
    }

    [Command]
    public void CmdSpawnMelee()
    {
        RaycastHit2D raycast = Physics2D.Raycast(m_UnitSpawn.transform.position, new Vector2(0, -1));

        if (raycast.collider != null)
        {
            if (raycast.collider.gameObject.CompareTag("Ground"))
            {
                GameObject melee = Instantiate(m_Melee, new Vector3(raycast.point.x, raycast.point.y + m_Melee.GetComponent<SpriteRenderer>().bounds.size.y / 2), m_Melee.transform.rotation) as GameObject;

                if (!m_Direction.m_FacingRight)
                {
                    Vector3 originalScale = melee.transform.localScale;
                    originalScale.x *= -1;
                    melee.transform.localScale = originalScale;
                }

                melee.GetComponent<Stats>().m_ArmourUpgrade = m_Upgrades.meleeArmour;
                melee.GetComponent<Stats>().m_SpeedUpgrade = m_Upgrades.meleeSpeed;

                NetworkServer.SpawnWithClientAuthority(melee, this.gameObject);

                m_Gold -= Prices.m_MeleePrice;
            }
            else
            {
                Debug.Log("Must wait to spawn");
            }
        }
    }

    [Command]
    public void CmdSpawnRanged()
    {
        RaycastHit2D raycast = Physics2D.Raycast(m_UnitSpawn.transform.position, new Vector2(0, -1));

        if (raycast.collider != null)
        {
            if (raycast.collider.gameObject.CompareTag("Ground"))
            {
                GameObject ranged = Instantiate(m_Ranged, new Vector3(raycast.point.x, raycast.point.y + m_Ranged.GetComponent<SpriteRenderer>().bounds.size.y / 2), m_Melee.transform.rotation) as GameObject;

                if (!m_Direction.m_FacingRight)
                {
                    Vector3 originalScale = ranged.transform.localScale;
                    originalScale.x *= -1;
                    ranged.transform.localScale = originalScale;
                }

                NetworkServer.SpawnWithClientAuthority(ranged, this.gameObject);

                m_Gold -= Prices.m_RangedPrice;
            }
            else
            {
                Debug.Log("Must wait to spawn");
            }
        }
    }


    ////////
    ///Upgrades
    ////////

    [Command]
    public void CmdUpgradeWorkerGold()
    {
         m_Upgrades.workerGold++;
    }

    [Command]
    public void CmdUpgradeWorkerSpeed()
    {

            m_Upgrades.workerSpeed++;

    }

    [Command]
    public void CmdUpgradeMeleeArmour()
    {

            m_Upgrades.meleeArmour++;
    }

    [Command]
    public void CmdUpgradeMeleeSpeed()
    {
            m_Upgrades.meleeSpeed++;
    }

    [Command]
    public void CmdUpgradeRangedArmour()
    {
            m_Upgrades.rangedArmour++;
    }

    [Command]
    public void CmdUpgradeRangedSpeed()
    {
            m_Upgrades.rangedSpeed++;
    }

    [Command]
    public void CmdUpgradeAirArmour()
    {
            m_Upgrades.airArmour++;
    }

    [Command]
    public void CmdUpgradeAirSpeed()
    {
            m_Upgrades.airSpeed++;
    }


    /* void FixedUpdate()
 {
     if (!isLocalPlayer)
     {
         return;
     }
     transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime);
     if (Input.GetKeyDown(KeyCode.Space))
     {
         CmdFire();
     }
 }*/

    // Use this for initialization

    /*
    public override void OnStartLocalPlayer()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    [Command]
    void CmdFire()
    {
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50, 0));

        NetworkServer.Spawn(bullet);

    }*/
}
