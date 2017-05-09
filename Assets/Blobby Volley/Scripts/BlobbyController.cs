using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlobbyController : MonoBehaviour {
    
    [Range(1, 12)]
    public int m_playerID;

    public KeyCode m_left;
    public KeyCode m_right;
    public KeyCode m_up;

    public int m_score;

    public float m_horizontalSpeed;
    public float m_jumpAmplitude;
    public float m_gravity;

    public Text m_scoreText;

    public const string c_scoreBaseText = "Player XX : ";

    public const float c_ground = -2f;

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
        m_scorePlayerText = c_scoreBaseText.Replace("XX", m_playerID.ToString());
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

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (Input.GetKey(m_left))
        {
            m_transform.position += m_horizontalSpeed * Time.deltaTime * Vector3.left;
        }
        if (Input.GetKey(m_right))
        {
            m_transform.position += m_horizontalSpeed * Time.deltaTime * Vector3.right;
        }

        if (Input.GetKeyDown(m_up) && !m_flying)
        {
            m_rigidbody.AddForce(Vector3.up * m_jumpAmplitude, ForceMode2D.Impulse);
            m_flying = true;
            StartCoroutine(CheckFlying());
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
        m_score = 0;
        m_scoreText.text = m_scorePlayerText + m_score;
        m_transform.position = m_initPos;
    }

    public void Score()
    {
        m_scoreText.text = m_scorePlayerText + ++m_score;
    }
}
