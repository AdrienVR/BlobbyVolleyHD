using System.Collections;
using UnityEngine;

public class BlobbyController : MonoBehaviour {
    
    [Range(1, 12)]
    public int m_playerID;

    public Team m_team;
    public int m_teamIndex;

    public KeyCode m_left;
    public KeyCode m_right;
    public KeyCode m_up;

    public float m_horizontalSpeed;
    public float m_jumpAmplitude;
    public float m_gravity;


    public const float c_ground = -2f;

    public AnimationCurve m_speedInertia;

    [HideInInspector][SerializeField]
    private string m_scorePlayerText;
    [HideInInspector][SerializeField]
    private Rigidbody2D m_rigidbody;
    [HideInInspector][SerializeField]
    private Transform m_transform;

    private Vector3 m_initPos;
    private bool m_flying;

#if UNITY_EDITOR
    void OnValidate()
    {
        m_transform = transform;
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    [ContextMenu("SetControlZQS")]
    void SetControlZQS()
    {
        m_left = KeyCode.Q;
        m_right = KeyCode.S;
        m_up = KeyCode.Z;
    }

    [ContextMenu("SetControlHUK")]
    void SetControlHUK()
    {
        m_left = KeyCode.H;
        m_right = KeyCode.K;
        m_up = KeyCode.U;
    }
#endif

    void Start()
    {
        m_initPos = m_transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(m_up) && !m_flying)
        {
            m_rigidbody.AddForce(Vector3.up * m_jumpAmplitude, ForceMode2D.Impulse);
            m_flying = true;
            StartCoroutine(CheckFlying());
        }
    }

    float m_leftKeyTimer;
    float m_rightKeyTimer;

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (Input.GetKey(m_left))
        {
            m_transform.position += m_horizontalSpeed * Time.deltaTime * Vector3.left * m_speedInertia.Evaluate(m_leftKeyTimer);
            m_leftKeyTimer += Time.deltaTime;
        }
        else
        {
            m_leftKeyTimer = 0;
        }
        if (Input.GetKey(m_right))
        {
            m_transform.position += m_horizontalSpeed * Time.deltaTime * Vector3.right * m_speedInertia.Evaluate(m_rightKeyTimer);
            m_rightKeyTimer += Time.deltaTime;
        }
        else
        {
            m_rightKeyTimer = 0;
        }
    }

    IEnumerator CheckFlying()
    {
        yield return null;
        yield return null;
        while (m_flying)
        {
            yield return null;
            if (m_transform.position.y < c_ground)
                m_flying = false;
        }
    }

    void OnCollisionEnter2D(Collision2D _coll)
    {
        if (_coll.gameObject.layer == 0 && m_flying)
        {
            m_flying = false;
        }
    }

    public void ResetPlayer()
    {
        m_transform.position = m_initPos;
    }
}
