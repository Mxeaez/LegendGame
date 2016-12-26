using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class Air : Unit
{

    public int m_Damage;

    public GameObject m_ExplosionEffect;

    public Vector3 m_StartPosition;
    public Vector3 m_EndPosition;

    private bool m_Attacking;

    private Stats m_Stats;
    private Rigidbody2D m_Rigidbody;
    private LineRenderer m_LineRenderer;

    void Start()
    {
        m_Stats = GetComponent<Stats>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_LineRenderer = GetComponent<LineRenderer>();

        Invoke("EnableCanCheckAuthority", .1f);
    }

    void Update()
    {
        if (!hasAuthority)
        {
            return;
        }

        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime * 20);

        if (m_Attacking == false)
        {
            if (m_StartPosition != null && m_EndPosition != null)
            {
                if (transform.position.x > m_StartPosition.x)
                {
                    m_Rigidbody.velocity = (new Vector3(transform.position.x - 1, transform.position.y) - transform.position).normalized * (float)(8.2 + (m_Stats.m_SpeedUpgrade / 5f));
                }
                else
                {
                    m_Rigidbody.velocity = (new Vector3(transform.position.x + 1, transform.position.y) - transform.position).normalized * (float)(8.2 + (m_Stats.m_SpeedUpgrade / 5f));
                }
                if ((int)transform.position.x == (int)m_StartPosition.x)
                {
                    m_Attacking = true;
                }
            }
        }
        else
        {
            m_Rigidbody.velocity = (new Vector3(transform.position.x, transform.position.y - 1) - transform.position).normalized * (float)(8.2 + (m_Stats.m_SpeedUpgrade / 5f) * 10);
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasAuthority)
        {
            return;
        }

        Debug.Log("Hey");

        foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, 4f))
        {
            if (collider.GetComponent<Health>() != null)
            {
                Health enemyHealth = collider.GetComponent<Health>();

                //if (!enemyHealth.hasAuthority)
                //{
                    CmdHurtEnemy(collider.gameObject);
                //}
            }
        }
        GameObject explosionEffect = Instantiate(m_ExplosionEffect, new Vector3(transform.position.x, transform.position.y + 2, -1), transform.rotation);
        Destroy(explosionEffect, 5f);
        Destroy(gameObject);
    }

    [Command]
    private void CmdHurtEnemy(GameObject enemy)
    {
        float multiplier = Random.Range(0.5f, 1.5f);

        if (enemy.GetComponent<Stats>() == null)
        {
            //Returns true if this was the finishing blow to base
            enemy.GetComponent<Health>().damage(m_Damage * multiplier);
        }
        else
        {
            enemy.GetComponent<Health>().damage(m_Damage * multiplier - enemy.GetComponent<Stats>().m_ArmourUpgrade);
        }
    }

}
