
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public const int c_maxPlayers = 2;
    public const int c_maxTeams = 2;

    public BallController m_ball;
    public BlobbyController[] m_players;

    public int m_maxScore;
    public Text m_winText;

    const string c_winText = "Team XX wins ! Team YY sucks !";

    public TeamHolder[] m_teams = new TeamHolder[c_maxTeams];

    public static GameManager Instance;

#if UNITY_EDITOR
    void OnValidate()
    {
        m_players = FindObjectsOfType<BlobbyController>();

        /*m_teams = new TeamHolder[c_maxTeams];
        for (int i = 0; i < c_maxTeams; i++)
        {
            m_teams[i] = new TeamHolder();
            m_teams[i].m_team = (Team)i;
        }*/

        for (int i = 0; i < c_maxTeams; i++)
            m_teams[i].m_teamMembers.Clear();

        for (int i = 0; i< m_players.Length; i++)
        {
            if (m_players[i].transform.position.x < 0)
                m_teams[0].m_teamMembers.Add(m_players[i]);
            else
                m_teams[1].m_teamMembers.Add(m_players[i]);
        }

        for (int i = 0; i < c_maxTeams; i++)
        {
            m_teams[i].Validate();
        }
    }
#endif

    void Awake()
    {
        Instance = this;

        for (int i = 0; i < c_maxTeams; i++)
            m_teams[i].ResetScore();
    }

    public void Fail(Team _failTeam)
    {
        for (int i = 0; i < c_maxTeams; i++)
        {
            if ((Team)i == _failTeam)
                continue;

            m_teams[i].Score();
        }

        if ((int)_failTeam < 1)
            m_ball.SetRight();
        else
            m_ball.SetLeft();
    }

    public void TeamWins(Team _winTeam, int _teamIndex)
    {
        int looseTeam = 0;
        StartCoroutine(EndGameCoroutine(_teamIndex, looseTeam));
    }

    IEnumerator EndGameCoroutine(int _winTeam, int _looseTeam)
    {
        m_winText.text = c_winText.Replace("XX", _winTeam.ToString()).Replace("YY", _looseTeam.ToString());
        m_winText.enabled = true;
        m_ball.gameObject.SetActive(false);
        yield return new WaitForSeconds(5);
        m_winText.enabled = false;
        m_ball.gameObject.SetActive(true);
        ResetPlayers();
    }

    void ResetPlayers()
    {
        for (int i = 0; i < m_teams.Length; i++)
        {
            m_teams[i].ResetScore();
        }
        for (int i = 0; i < m_players.Length; i++)
        {
            m_players[i].ResetPlayer();
        }
    }
}
