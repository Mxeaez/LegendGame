using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Worker : NetworkBehaviour
{

    public int m_GoldMined;

    private Stats m_Stats;

    private SyncDirection m_Direction;

    public Player m_Player;

    void Awake()
    {
        m_Stats = GetComponent<Stats>();
        m_Direction = GetComponent<SyncDirection>();
    }

    void Start()
    {
        if (transform.transform.localScale.x < 0)
        {
            m_Direction.m_FacingRight = false;
        }
    }

    public void FixedUpdate()
    {
        m_Direction.Flip(m_Direction.m_FacingRight);
        if (m_Player != null)
        {
            transform.position += new Vector3(transform.localScale.x, 0) * (float)(1.2 + (m_Player.m_WorkerSpeed / 5f)) * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(transform.localScale.x, 0) * Time.deltaTime;
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground") && !collision.gameObject.CompareTag("Worker"))
        {
            m_Direction.m_FacingRight = !m_Direction.m_FacingRight;

            if(collision.gameObject.GetComponent<Player>() != null)
            {
                m_Player = collision.gameObject.GetComponent<Player>();
                m_Player.m_Gold += m_GoldMined + m_Player.m_WorkerGold;
            }
        }
    }
}
