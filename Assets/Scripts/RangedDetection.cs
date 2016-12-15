using UnityEngine;
using System.Collections;

public class RangedDetection : MonoBehaviour
{
/*
    Ranged m_Unit;

    void Awake()
    {
        m_Unit = transform.parent.GetComponent<Ranged>();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (m_Unit.m_Enemy == null)
        {
            if (collision.GetComponent<Health>() != null)
            {
                Health enemyHealth = collision.GetComponent<Health>();

                if (!enemyHealth.hasAuthority)
                {
                    m_Unit.m_Enemy = collision.gameObject;
                    Debug.Log("Enemy Acquired");
                }
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(!m_Unit.hasAuthority)
        {
            return;
        }
        if (m_Unit.m_Enemy != null)
        {
            if (collision.gameObject == m_Unit.m_Enemy)
            {
                m_Unit.m_Enemy = null;
                m_Unit.CmdUpdateAnimation("isAttackingFalse");
            }
        }
    }
    */
}
