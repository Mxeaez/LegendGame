using UnityEngine;
using System.Collections;

public class Collider : MonoBehaviour {

    Unit m_Unit;

    GameObject m_Collider;

    void Awake()
    {
        m_Unit = transform.parent.GetComponent<Unit>();
    }

    void Update()
    {
        if (m_Collider == null)
        {
            m_Unit.m_IsColliding = false;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Health>() != null)
        {
            m_Unit.m_IsColliding = true;
            m_Collider = collision.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {

        m_Unit.m_IsColliding = false;
    }


}
