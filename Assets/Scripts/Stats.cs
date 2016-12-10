using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Stats : NetworkBehaviour {

    [SyncVar]
    public int m_ArmourUpgrade;
    [SyncVar]
    public int m_SpeedUpgrade;
    [SyncVar]
    public int m_GoldUpgrade;
}
