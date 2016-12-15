using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Arrow : NetworkBehaviour {

    private SyncDirection m_Direction;

    void Awake()
    {
        m_Direction = GetComponent<SyncDirection>();
    }

    void Start()
    {
        if (transform.localScale.x < 0)
        {
            m_Direction.m_FacingRight = false;
        }
        if (gameObject != null)
        {
            Destroy(gameObject, 3f);
        }
    }

    void FixedUpdate()
    {
        m_Direction.Flip(m_Direction.m_FacingRight);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasAuthority)
        {
            return;
        }
        if (collision.GetComponent<Health>() != null)
        {
            Health enemyHealth = collision.GetComponent<Health>();
            if (!enemyHealth.hasAuthority)
            {
                Destroy(gameObject);
                Debug.Log("Sniped");
            }
        }
    }

    /*
    Damage m_Damage;

    Rigidbody2D m_Rigidbody;

    private SyncDirection m_Direction;

    void Awake()
    {
        m_Direction = GetComponent<SyncDirection>();
    }

    void Start()
    {
        m_Damage = GetComponent<Damage>();
        m_Rigidbody = GetComponent<Rigidbody2D>();

        if (transform.localScale.x < 0)
        {
            m_Direction.m_FacingRight = false;
        }
    }

    void FixedUpdate()
    {
        m_Direction.Flip(m_Direction.m_FacingRight);
        if (!hasAuthority)
        {
            return;
        }
        m_Rigidbody.velocity = (new Vector3(m_Damage.x, m_Damage.y) - transform.position).normalized * 5f;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(!hasAuthority)
        {
            return;
        }
        if(collision.GetComponent<Health>() != null)
        {
            Health enemyHealth = collision.GetComponent<Health>();
            if (!enemyHealth.hasAuthority)
            {
                CmdHurtEnemy(collision.gameObject);
                Destroy(gameObject);
                Debug.Log("Sniped");
            }
        }
    }

    [Command]
    private void CmdHurtEnemy(GameObject enemy)
    {
        float multiplier = Random.Range(0.5f, 1.5f);

        enemy.GetComponent<Health>().damage(m_Damage.m_Damage * multiplier);
    }*/
}
