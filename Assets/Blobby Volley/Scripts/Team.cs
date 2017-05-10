
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
    public int m_teamNumber;
    public List<BlobbyController> m_teamMembers = new List<BlobbyController>(c_maxPlayers / c_maxTeams);

    public Text m_scoreText;

    public const string c_scoreBaseText = "Team XX : ";
    public int m_score;
    
    [HideInInspector][SerializeField]
    private string m_scorePlayerText;
    
    public void Validate()
    {
        m_teamNumber = (int)m_team + 1;
        m_scorePlayerText = c_scoreBaseText.Replace("XX", m_teamNumber.ToString());
    }

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
            GameManager.Instance.TeamWins(m_team, m_teamNumber);
        }
    }
}
