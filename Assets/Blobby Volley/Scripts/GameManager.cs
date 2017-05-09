using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    Red = 0,
    Blue,
    Green,
    Yellow,
}

public class TeamHolder
{
    public const int c_maxPlayers = 2;
    public const int c_maxTeams = 2;

    public Team m_team;
    public List<BlobbyController> m_teamMembers = new List<BlobbyController>(c_maxPlayers / c_maxTeams);
}

public class GameManager : MonoBehaviour {

    public const int c_maxPlayers = 2;
    public const int c_maxTeams = 2;

    public BallController m_ball;
    public BlobbyController[] m_players;

    public TeamHolder[] m_teams = new TeamHolder[c_maxTeams];

#if UNITY_EDITOR
    void OnValidate()
    {
        m_players = FindObjectsOfType<BlobbyController>();

        m_teams = new TeamHolder[c_maxTeams];
        for (int i = 0; i < c_maxTeams; i++)
        {
            m_teams[i] = new TeamHolder();
            m_teams[i].m_team = (Team)i;
        }

        for (int i = 0; i< m_players.Length; i++)
        {
            if (m_players[i].transform.position.x < 0)
                m_teams[0].m_teamMembers.Add(m_players[i]);
            else
                m_teams[1].m_teamMembers.Add(m_players[i]);
        }
    }
#endif

    public void Fail(Team _failTeam)
    {
        for (int i = 0; i < c_maxTeams; i++)
        {
            if ((Team)i == _failTeam)
                continue;

            for (int j = 0; j < m_teams[i].m_teamMembers.Count; j++)
            {
                m_teams[i].m_teamMembers[j].Score();
            }
        }

        if ((int)_failTeam < 1)
            m_ball.SetRight();
        else
            m_ball.SetLeft();
    }

    public void ResetGame()
    {
        for (int i = 0; i < m_players.Length; i++)
        {
            m_players[i].ResetPlayer();
        }
    }
}
