using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour {

    public float m_MaxHealth = 100f;
    [SyncVar(hook = "OnDamaged")]
    public float m_CurrentHealth;

    public Image m_HealthBar;

    void Start()
    {
        m_CurrentHealth = m_MaxHealth;
    }

    public bool damage(float amount)
    {
        //Only run this method on the server
        if (!isServer)
        {
            return false;
        }

        m_CurrentHealth -= amount;


        if (m_CurrentHealth <= 0)
        {
            Destroy(gameObject);
            return true;
        }

        return false;
    }

    void OnDamaged(float health)
    {
        //Sync current health by setting it to the health
        m_CurrentHealth = health;

        //Fill health bar to new amount
        float healthPercent = m_CurrentHealth / m_MaxHealth;
        m_HealthBar.fillAmount = healthPercent;
    }
}
