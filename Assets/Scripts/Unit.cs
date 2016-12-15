using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Unit : NetworkBehaviour {

    public GameObject m_Enemy;
    public bool m_CanCheckAuthority;

    [SyncVar(hook = "OnCollidingChange")]
    public bool m_IsColliding;

    void OnCollidingChange(bool colliding)
    {
        m_IsColliding = colliding;
    }

    public void EnableCanCheckAuthority()
    {
        m_CanCheckAuthority = true;
    }
}
