
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Vector3 m_leftPos;
    public Vector3 m_rightPos;

    [HideInInspector][SerializeField]
    private Rigidbody m_rigidbody;
    [HideInInspector][SerializeField]
    private Transform m_transform;

#if UNITY_EDITOR
    void OnValidate()
    {
        m_transform = transform;
        m_rigidbody = GetComponent<Rigidbody>();
    }
#endif
    
    public void SetLeft()
    {
        m_transform.position = m_leftPos;
        m_rigidbody.isKinematic = true;
    }

    public void SetRight()
    {
        m_transform.position = m_rightPos;
        m_rigidbody.isKinematic = true;
    }
    
    void OnCollisionEnter(Collision _coll)
    {
        m_rigidbody.isKinematic = false;
    }
}
