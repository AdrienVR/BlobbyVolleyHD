﻿
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Vector3 m_leftPos;
    public Vector3 m_rightPos;

    public float m_heightInvisible = 5;

    public MeshRenderer m_indicRend;
    public Transform m_indicator;

    public float m_maxSpeed = 2;

    [HideInInspector][SerializeField]
    private Rigidbody2D m_rigidbody;
    [HideInInspector][SerializeField]
    private Transform m_transform;

#if UNITY_EDITOR
    void OnValidate()
    {
        m_transform = transform;
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
#endif

    void Update()
    {
        if (m_transform.position.y > m_heightInvisible)
        {
            m_indicRend.enabled = true;
            Vector3 pos = m_transform.position;
            pos.y = m_indicator.position.y;
            m_indicator.position = pos;
        }
        else if (m_indicRend.enabled)
        {
            m_indicRend.enabled = false;
        }
        m_rigidbody.velocity = Vector3.ClampMagnitude(m_rigidbody.velocity, m_maxSpeed);
    }

    void ResetBall()
    {
        m_rigidbody.gravityScale = 0;
        m_rigidbody.velocity = new Vector2();
    }
    
    public void SetLeft()
    {
        ResetBall();
        m_transform.position = m_leftPos;
    }

    public void SetRight()
    {
        ResetBall();
        m_transform.position = m_rightPos;
    }
    
    void OnCollisionEnter2D(Collision2D _coll)
    {
        m_rigidbody.gravityScale = 1;
    }
}
