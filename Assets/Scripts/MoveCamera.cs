using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{


    public int m_Offset;
    public int m_MoveSpeed = 5;

    public GameObject m_Background;

    private int screenWidth;
    private int screenHeight;

    private float cameraHeight;
    private float cameraWidth;

    // Use this for initialization
    void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        cameraHeight = 2 * Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition.x > screenWidth - m_Offset)
        {
            //Debug.Log(m_Background.GetComponent<SpriteRenderer>().bounds.size.x / 2 - cameraWidth);
            if (transform.position.x < m_Background.GetComponent<SpriteRenderer>().bounds.size.x / 2 - (cameraWidth / 2))
            {
                transform.position = new Vector3(transform.position.x + m_MoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
        }
        else if (Input.mousePosition.x < m_Offset)
        {
            if (transform.position.x > m_Background.GetComponent<SpriteRenderer>().bounds.size.x / -2 + (cameraWidth / 2))
            {
                transform.position = new Vector3(transform.position.x - m_MoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
        }
    }

    public void setMaxRight()
    {
        Camera.main.gameObject.transform.position = new Vector3(m_Background.GetComponent<SpriteRenderer>().bounds.size.x / 2 - (cameraWidth / 2), Camera.main.transform.position.y, Camera.main.transform.position.z);
    }

    public void setMaxLeft()
    {
        Camera.main.gameObject.transform.position = new Vector3(m_Background.GetComponent<SpriteRenderer>().bounds.size.x / -2 + (cameraWidth / 2), Camera.main.transform.position.y, Camera.main.transform.position.z);
    }
}
