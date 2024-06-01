using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Variables

    // Timer
    [SerializeField] private float m_TimeLimit;
    private float m_StartTime;

    // UI
    [SerializeField] private Text m_TimerText;
    [SerializeField] private Text m_Score;
    #region NameInputPanel

    [SerializeField] private GameObject m_NameInputPanel;
    [SerializeField] private InputField m_NameInput;

    #endregion
    #region GameOverPanel

    [SerializeField] private GameObject m_GameOverPanel;
    [SerializeField] private Text m_YourScoreText;
    [SerializeField] private Text m_HighScoresText;

    #endregion

    // References
    private Player m_Player;

    private bool m_GameOver;

    #endregion

    private void Start()
    {
        // Timer
        m_StartTime = Time.time;

        // References
        m_Player = GameObject.Find("Player").GetComponent<Player>();

        m_GameOver = false;
    }

    private void Update()
    {
        UpdateScore();

        if (Time.time - m_StartTime >= m_TimeLimit && !m_GameOver)
        {
            GameOver();
        }
        else if (!m_GameOver)
        {
            float timeLeft = m_TimeLimit - (Time.time - m_StartTime);
            m_TimerText.text = timeLeft.ToString("F2");
        }
    }

    private void UpdateScore()
    {
        m_Score.text = "Score: " + m_Player.CoinsCollected.ToString();
    }

    public void GameOver()
    {
        m_GameOver = true;

        Time.timeScale = 0;

        m_NameInputPanel.SetActive(true);
    }

    public void InputName()
    {
        if (m_NameInput.text != "")
        {
            PlayerData.PlayerName = m_NameInput.text;
        }
        else
        {
            PlayerData.PlayerName = "Anon";
        }

        m_GameOverPanel.SetActive(true);

        m_YourScoreText.text = "Score: " + m_Player.CoinsCollected.ToString();
        HighScoreData highScoreData = GetHighScoreData();
        m_HighScoresText.text = "High Scores\n\n" +
                                "1. " + highScoreData.names[2] + ": " + highScoreData.highScores[2] + "\n" +
                                "2. " + highScoreData.names[1] + ": " + highScoreData.highScores[1] + "\n" +
                                "3. " + highScoreData.names[0] + ": " + highScoreData.highScores[0];

        SaveData.Save(highScoreData);
    }

    private HighScoreData GetHighScoreData()
    {
        HighScoreData highScoreData = SaveData.Load();
        bool isNewHighScore = false;
        int newHighScoreIndex = 0;

        for (int i = 0; i < highScoreData.highScores.Length; i++)
        {
            if (m_Player.CoinsCollected > highScoreData.highScores[i])
            {
                newHighScoreIndex = i;
                isNewHighScore = true;
            }
        }

        if (isNewHighScore)
        {
            for (int i = 0; i <= newHighScoreIndex; i++)
            {
                if (i != newHighScoreIndex)
                {
                    highScoreData.highScores[i] = highScoreData.highScores[i + 1];
                    highScoreData.names[i] = highScoreData.names[i + 1];
                }
                else
                {
                    highScoreData.highScores[i] = m_Player.CoinsCollected;
                    highScoreData.names[i] = PlayerData.PlayerName;
                }
            }
        }

        return highScoreData;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
