using UnityEngine;
using TMPro;

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
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        score += points;

        Debug.Log("Score: " + score);

        UpdateScoreText();
    }

    public void LoseLife()
    {
        lives--;

        Debug.Log("Vidas restantes: " + lives);

        UpdateLivesText();

        if (lives <= 0)
        {
            GameOver();
        }
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

    void GameOver()
    {
        Debug.Log("GAME OVER");
        Time.timeScale = 0f;
    }
}