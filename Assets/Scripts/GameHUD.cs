using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameHUD : MonoBehaviour {

    [HideInInspector]
    public Player m_Player;

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

    void Update()
    {
        if(transform.FindChild("Panel") != null)
        {
            if(transform.FindChild("Panel").FindChild("Info") != null)
            {
                Text hudText = transform.FindChild("Panel").FindChild("Info").GetComponent<Text>();
                if (m_Player != null)
                {
                    hudText.text = "Gold: " + m_Player.m_Gold + "\nWorkers: " + m_Player.m_WorkerCount;
                }
            }
            else
            {
                Debug.LogWarning("Could not find Game_HUD/Panel/Info");
            }
        }
        else
        {
            Debug.LogWarning("Could not find Game_HUD/Panel");
        }
    }
}
