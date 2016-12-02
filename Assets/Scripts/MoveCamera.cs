using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{


    public int m_Offset;
    public int m_MoveSpeed = 5;

    public GameObject m_Background;

    private int screenWidth;
    private int screenHeight;

    // Use this for initialization
    void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition.x > screenWidth - m_Offset)
        {
            if (transform.position.x < 8.6f)
            {
                transform.position = new Vector3(transform.position.x + m_MoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
        }
        else if (Input.mousePosition.x < m_Offset)
        {
            if (transform.position.x > -8.6f)
            {
                transform.position = new Vector3(transform.position.x - m_MoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
        }
    }
}
