
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public const int c_maxPlayers = 2;
    public const int c_maxTeams = 2;

    public BallController m_ball;
    public List<BlobbyController> m_players = new List<BlobbyController>(4);

    public int m_maxScore;
    public Text m_winText;

    const string c_winText = "Team XX wins ! Team YY sucks !";

    public TeamHolder[] m_teams = new TeamHolder[c_maxTeams];

    public static GameManager Instance;

#if UNITY_EDITOR
    void OnValidate()
    {
        for (int i = 0; i < c_maxTeams; i++)
        {
            m_teams[i].Validate();
        }
    }
#endif

    void Awake()
    {
        Application.runInBackground = true;
        Instance = this;

        for (int i = 0; i < c_maxTeams; i++)
            m_teams[i].ResetScore();
    }

    public void RegisterPlayer(BlobbyController _newPlayer, int _teamIndex)
    {
        m_players.Add(_newPlayer);
        m_teams[_teamIndex].m_teamMembers.Add(_newPlayer);
        
        _newPlayer.m_team = m_teams[_teamIndex].m_team;
        _newPlayer.m_teamNumber = m_teams[_teamIndex].m_teamNumber;
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

    public void TeamWins(Team _winTeam, int _teamNumber)
    {
        int looseTeam = GetOtherTeam(_teamNumber);
        StartCoroutine(EndGameCoroutine(_teamNumber, looseTeam));
    }

    int GetOtherTeam(int _teamNumber)
    {
        for (int i = 0; i < c_maxTeams; i++)
        {
            if (m_teams[i].m_teamNumber != _teamNumber)
                return m_teams[i].m_teamNumber;
        }
        return 0;
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
        for (int i = 0; i < m_players.Count; i++)
        {
            m_players[i].ResetPlayer();
        }
    }
}
