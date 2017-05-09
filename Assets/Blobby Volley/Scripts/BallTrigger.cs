using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrigger : MonoBehaviour
{
    public GameManager m_gameManager;
    public Team m_team;

#if UNITY_EDITOR
    void OnValidate()
    {
        m_gameManager = FindObjectOfType<GameManager>();

        if (transform.position.x < 0)
            m_team = (Team)0;
        else
            m_team = (Team)1;
    }
#endif

    void OnTriggerEnter2D(Collider2D other)
    {
        m_gameManager.Fail(m_team);
    }
}
