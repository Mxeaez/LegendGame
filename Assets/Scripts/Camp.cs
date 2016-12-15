using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Camp : MonoBehaviour {

    private Player m_Player;

    //UnitUpgrade Animations
    private Animator m_Anim;

    //Unit Animations
    private Animator m_UnitAnim;

    private Text m_NotEnoughGold;

    // Use this for initialization
    void Start () {
        m_Player = transform.parent.GetComponent<Player>();
        m_Anim = GameObject.Find("Game_HUD").transform.FindChild("UnitUpgrade").GetComponent<Animator>();
        m_UnitAnim = GameObject.Find("Game_HUD").transform.FindChild("Unit").GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnMouseDown()
    {
        if (!m_Player.isLocalPlayer)
        {
            return;
        }

        if (m_Anim.GetBool("isClicked"))
        {
            m_Anim.SetBool("isClicked", false);
        }
        else
        {
            m_Anim.SetBool("isClicked", true);
            m_UnitAnim.SetBool("isClicked", false);
        }
    }
}
