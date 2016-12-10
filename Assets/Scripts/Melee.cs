using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class Melee : NetworkBehaviour
{

    public int m_Damage;
    public int m_AttackSpeed;

    [SyncVar(hook = "OnWalkChange")]
    public bool m_IsWalking = true;
    [SyncVar(hook = "OnAnimationChange")]
    public string m_CurrentAnimation;
    private Animator m_Anim;
    private float m_Cooldown = -1;

    private SyncDirection m_Direction;

    public GameObject m_Enemy;

    private Stats m_Stats;

    void Awake()
    {
        m_Direction = GetComponent<SyncDirection>();
    }

    void Start()
    {
        m_Anim = GetComponent<Animator>();
        m_Stats = GetComponent<Stats>();

        if (transform.transform.localScale.x < 0)
        {
            m_Direction.m_FacingRight = false;
        }
    }

    public void FixedUpdate()
    {
        m_Direction.Flip(m_Direction.m_FacingRight);
        if (m_IsWalking)
        {
            transform.position += new Vector3(transform.localScale.x, 0) * (m_Stats.m_SpeedUpgrade + 2 * .5f) * Time.deltaTime;
        }
    }

    void Update()
    {
        if (!hasAuthority)
        {
            return;
        }

        //If the unit has a current target, and is off cooldown, do damage to it
        if (m_Enemy != null)
        {
            if (m_Cooldown < Time.time - m_AttackSpeed || m_Cooldown == -1)
            {
                CmdHurtEnemy(m_Enemy);

                m_Cooldown = Time.time;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasAuthority)
        {
            return;
        }

        if (collision.GetComponent<Health>() != null)
        {
            Health collisionHealth = collision.GetComponent<Health>();

            if (!collisionHealth.hasAuthority)
            {
                Attack(collision.gameObject);
            }
            else
            {
                /*if (!collision.gameObject.CompareTag("Player"))
                {
                    CmdSetWalking(false);
                }*/
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (hasAuthority)
        {
            CmdSetWalking(true);
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

    [Command]
    private void CmdHurtEnemy(GameObject enemy)
    {
        float multiplier = Random.Range(0.5f, 1.5f);

        //Returns true if this was the finishing blow
        if(enemy.GetComponent<Health>().damage(m_Damage * multiplier))
        {
            CmdSetWalking(true);
        }
    }

    [Command]
    private void CmdUpdateAnimation(string animation)
    {
        m_CurrentAnimation = animation;
    }

    [Command]
    private void CmdSetWalking(bool walking)
    {
        if (walking)
        {
            m_IsWalking = true;
            m_CurrentAnimation = "";
        }
        else
        {
            m_IsWalking = false;
        }
    }

    void OnWalkChange(bool walking)
    {
        m_IsWalking = walking;
    }

    void OnAnimationChange(string animation)
    {
        m_CurrentAnimation = animation;

        if (m_CurrentAnimation == "")
        {
            m_Anim.SetBool("Slash", false);
            m_Anim.SetBool("Stab", false);
        }
        else
        {
            m_Anim.SetBool(m_CurrentAnimation, true);
        }
    }

    /*
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Melee"))
        {
            Health m = collision.gameObject.GetComponent<Health>();
            if (!m.isLocalPlayer)
            {
                if (m_IsWalking)
                {
                    m_IsWalking = false;

                    int randomNumber = (int)Random.Range(1, 100);
                    if (randomNumber >= 50)
                    {
                        m_Anim.SetBool("Slash", true);
                    }
                    else
                    {
                        m_Anim.SetBool("Stab", true);
                    }
                }
                else
                {
                    if (m_Cooldown <= Time.time - m_AttackSpeed || m_Cooldown == -1)
                    {
                        m.damage(m_Damage);

                        if(m.gameObject == null)
                        {
                            m_IsWalking = true;
                        }

                        m_Cooldown = Time.time;
                    }
                }
            }
        }
    }*/

}
