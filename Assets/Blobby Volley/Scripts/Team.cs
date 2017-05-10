
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Team
{
    Red = 0,
    Blue,
    Green,
    Yellow,
}

[System.Serializable]
public class TeamHolder
{
    public const int c_maxPlayers = 2;
    public const int c_maxTeams = 2;

    public Team m_team;
    public int m_teamIndex;
    public List<BlobbyController> m_teamMembers = new List<BlobbyController>(c_maxPlayers / c_maxTeams);

    public Text m_scoreText;

    public const string c_scoreBaseText = "Team XX : ";
    public int m_score;
    
    [HideInInspector][SerializeField]
    private string m_scorePlayerText;

#if UNITY_EDITOR
    public void Validate()
    {
        m_teamIndex = (int)m_team + 1;
        m_scorePlayerText = c_scoreBaseText.Replace("XX", m_teamIndex.ToString());
        for(int i = 0; i < m_teamMembers.Count; i++)
        {
            m_teamMembers[i].m_team = m_team;
            m_teamMembers[i].m_teamIndex = m_teamIndex;
        }
    }
#endif

    public void ResetScore()
    {
        m_score = 0;
        m_scoreText.text = m_scorePlayerText + m_score;
    }

    public void Score()
    {
        m_scoreText.text = m_scorePlayerText + ++m_score;

        if (m_score >= GameManager.Instance.m_maxScore)
        {
            GameManager.Instance.TeamWins(m_team, m_teamIndex);
        }
    }
}
