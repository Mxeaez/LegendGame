using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SyncDirection : NetworkBehaviour {

    [SyncVar]
    public bool m_FacingRight = true;

    public void Flip(bool Direction)
    {
        m_FacingRight = Direction;
        if (m_FacingRight)
        {
            if(gameObject.CompareTag("Melee") || gameObject.CompareTag("Worker") || gameObject.CompareTag("Ranged") || gameObject.CompareTag("Air"))
            {
                Vector3 originalScale = transform.localScale;
                originalScale.x = 1;
                transform.localScale = originalScale;
            }
            else
            {
                Vector3 originalScale = transform.localScale;
                originalScale.x = 4;
                transform.localScale = originalScale;
            }
        }
        else
        {
            if (gameObject.CompareTag("Melee") || gameObject.CompareTag("Worker") || gameObject.CompareTag("Ranged") || gameObject.CompareTag("Air"))
            {
                Vector3 originalScale = transform.localScale;
                originalScale.x = -1;
                transform.localScale = originalScale;
            }
            else
            {
                Vector3 originalScale = transform.localScale;
                originalScale.x = -4;
                transform.localScale = originalScale;
            }
        }
    }
}
