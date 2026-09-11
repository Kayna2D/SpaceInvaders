using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Pontuação")]
    public int score = 0;
    public TextMeshProUGUI scoreText;

    [Header("Vidas")]
    public int lives = 3;
    public TextMeshProUGUI livesText;


    void Start()
    {
        Time.timeScale = 1f;

        int phase = SceneManager.GetActiveScene().name ==
                    CampaignManager.PhaseTwoSceneName ? 2 : 1;

        CampaignManager.Instance.EnsureCampaignStarted(phase);

        score = CampaignManager.Instance.Score;
        lives = CampaignManager.Instance.Lives;

        UpdateScoreText();
        UpdateLivesText();
    }

    public void AddScore(int points)
    {
        CampaignManager.Instance.AddScore(points);
        score = CampaignManager.Instance.Score;

        Debug.Log("Score: " + score);

        UpdateScoreText();
    }

    public void LoseLife()
    {
        CampaignManager.Instance.LoseLife();
        lives = CampaignManager.Instance.Lives;

        Debug.Log("Vidas restantes: " + lives);

        UpdateLivesText();

    }

    public void CompletePhase()
    {
        CampaignManager.Instance.CompletePhase();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Pontos: " + score;
        }
    }

    void UpdateLivesText()
    {
        if (livesText != null)
        {
            livesText.text = "Vidas: " + lives;
        }
    }

}
