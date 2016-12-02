using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Worker : NetworkBehaviour
{

    public int m_GoldMined;

    private SyncDirection m_Direction;

    void Awake()
    {
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
        transform.position += new Vector3(transform.localScale.x, 0) * Time.deltaTime;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Ground") && !collision.gameObject.CompareTag("Worker"))
        {
            m_Direction.m_FacingRight = !m_Direction.m_FacingRight;

            if(collision.gameObject.GetComponent<Player>() != null)
            {
                Player player = collision.gameObject.GetComponent<Player>();
                player.m_Gold += m_GoldMined;
            }
        }
    }
}
