using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Ranged : Unit
{

    public int m_Damage;
    public int m_AttackSpeed;

    public Player m_Player;

    public GameObject m_Arrow;
    public GameObject m_ShootSpot;

    [SyncVar(hook = "OnWalkChange")]
    public bool m_IsWalking = true;
    [SyncVar(hook = "OnAnimationChange")]
    public string m_CurrentAnimation;
    private Animator m_Anim;
    private float m_Cooldown = -1;

    private SyncDirection m_Direction;

    //public GameObject m_Enemy;

    private Stats m_Stats;

    private Rigidbody2D m_Rigidbody;

    private float m_ClosestEnemyPos = -1;

    void Awake()
    {
        m_Direction = GetComponent<SyncDirection>();
    }

    void Start()
    {

        foreach(GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            if(p.GetComponent<Player>().isLocalPlayer)
            {
                m_Player = p.GetComponent<Player>();
                Debug.Log("Player set for ranged");
                break;
            }
        }
        m_Anim = GetComponent<Animator>();
        m_Stats = GetComponent<Stats>();
        m_Rigidbody = GetComponent<Rigidbody2D>();

        if (transform.transform.localScale.x < 0)
        {
            m_Direction.m_FacingRight = false;
        }
        Invoke("EnableCanCheckAuthority", .1f);
    }

    public void FixedUpdate()
    {

        m_Direction.Flip(m_Direction.m_FacingRight);
        if (!m_IsColliding)
        {
            //transform.position += new Vector3(transform.localScale.x, 0) * (m_Stats.m_SpeedUpgrade + 2 * .5f) * Time.deltaTime;
            if (m_Direction.m_FacingRight)
            {
                m_Rigidbody.velocity = (new Vector3(transform.position.x + 1, transform.position.y) - transform.position).normalized * (float)(1.2 + (m_Stats.m_SpeedUpgrade / 5f));
            }
            else
            {
                m_Rigidbody.velocity = (new Vector3(transform.position.x - 1, transform.position.y) - transform.position).normalized * (float)(1.2 + (m_Stats.m_SpeedUpgrade / 5f));
            }
        }
    }

    void Update()
    {
        if (!hasAuthority)
        {
            return;
        }

        if(m_Enemy == null)
        {
            m_ClosestEnemyPos = -1;
        }

        foreach(Collider2D collider in Physics2D.OverlapCircleAll(transform.position, 4f))
        {
            if(collider.GetComponent<Health>() != null)
            {
                Health enemyHealth = collider.GetComponent<Health>();

                if (enemyHealth.GetComponent<Unit>() != null)
                {

                    if (!enemyHealth.hasAuthority && enemyHealth.GetComponent<Unit>().m_CanCheckAuthority)
                    {
                        if (Mathf.Abs(collider.transform.position.x - transform.position.x) < m_ClosestEnemyPos || m_ClosestEnemyPos == -1)
                        {
                            m_ClosestEnemyPos = Mathf.Abs(collider.transform.position.x - transform.position.x);
                            m_Enemy = collider.gameObject;
                        }
                    }
                }
                else
                {
                    if (!enemyHealth.hasAuthority)
                    {
                        if (Mathf.Abs(collider.transform.position.x - transform.position.x) < m_ClosestEnemyPos || m_ClosestEnemyPos == -1)
                        {
                            m_ClosestEnemyPos = Mathf.Abs(collider.transform.position.x - transform.position.x);
                            m_Enemy = collider.gameObject;
                        }
                    }
                }
            }
        }

        //If the unit has a current target, and is off cooldown, do damage to it
        if (m_Enemy != null)
        {
            if (m_Cooldown < Time.time - m_AttackSpeed || m_Cooldown == -1)
            {
                CmdHurtEnemy(m_Enemy);
                CmdUpdateAnimation("isAttackingTrue");
                Debug.Log("Fire");

                m_Cooldown = Time.time;
            }
        }
    }

    private void Attack(GameObject enemy)
    {
        m_Enemy = enemy;

        if (m_IsWalking)
        {
            CmdSetWalking(false);

            int randomNumber = Random.Range(1, 100);
            if (randomNumber >= 50)
            {
                CmdUpdateAnimation("Slash");
            }
            else
            {
                CmdUpdateAnimation("Stab");
            }
        }
    }


    //Shoot arrow
    [Command]
    private void CmdHurtEnemy(GameObject enemy)
    {
        GameObject arrowClone = Instantiate(m_Arrow, m_ShootSpot.transform.position, m_ShootSpot.transform.rotation) as GameObject;
        Rigidbody2D arrowRigidbody = arrowClone.GetComponent<Rigidbody2D>();
        arrowRigidbody.AddForce((enemy.transform.position - m_ShootSpot.transform.position).normalized * 50f);

        NetworkServer.SpawnWithClientAuthority(arrowClone, m_Player.gameObject);

        float multiplier = Random.Range(0.5f, 1.5f);

        //Returns true if this was the finishing blow
        if (enemy.GetComponent<Stats>() == null)
        {
            //Returns true if this was the finishing blow
            enemy.GetComponent<Health>().damage(m_Damage * multiplier);
        }
        else
        {
            if (enemy.GetComponent<Health>().damage(m_Damage * multiplier - enemy.GetComponent<Stats>().m_ArmourUpgrade))
            {
                CmdSetWalking(true);
            }

        }
        /*GameObject arrowClone = Instantiate(m_Arrow, m_ShootSpot.transform.position, m_ShootSpot.transform.rotation) as GameObject;
          Damage m_ArrowDamage = arrowClone.GetComponent<Damage>();
          m_ArrowDamage.m_Damage = m_Damage;
          m_ArrowDamage.x = enemy.transform.position.x;
          m_ArrowDamage.y = enemy.transform.position.y;

          NetworkServer.SpawnWithClientAuthority(arrowClone, m_Player.gameObject);

          /*float multiplier = Random.Range(0.5f, 1.5f);

          //Returns true if this was the finishing blow
          if (enemy.GetComponent<Health>().damage(m_Damage * multiplier))
          {
              CmdSetWalking(true);
          }*/
    }

    [Command]
    public void CmdUpdateAnimation(string animation)
    {
        m_CurrentAnimation = animation;
    }

    [Command]
    private void CmdSetWalking(bool walking)
    {
        if (walking)
        {
            m_IsWalking = true;
            m_CurrentAnimation = "isWalkingTrue";
        }
        else
        {
            m_IsWalking = false;
            m_CurrentAnimation = "isWalkingFalse";
        }
    }

    void OnWalkChange(bool walking)
    {
        m_IsWalking = walking;
    }

    void OnAnimationChange(string animation)
    {
        m_CurrentAnimation = animation;

        Debug.Log("animation changed");
        if (animation == "isWalkingTrue")
        {
            m_Anim.SetBool("isWalking", true);
        }
        else if (animation == "isWalkingFalse")
        {
            m_Anim.SetBool("isWalking", false);
        }
        if (animation == "isAttackingTrue")
        {
            m_Anim.SetBool("isAttacking", true);
        }
        else if (animation == "isAttackingFalse")
        {
            m_Anim.SetBool("isAttacking", false);
        }
    }
}
