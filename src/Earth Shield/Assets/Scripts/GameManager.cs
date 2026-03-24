using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public GameObject gameOverPanel;

    private int score = 0;
    private int lives = 3;
    private bool isGameOver = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateLivesUI();
    }

    public void AddScore(int amount)
    {
        if (isGameOver) return;

        score += amount;
        scoreText.text = "Score: " + score;
    }
    
    public void LoseLife()
    {
        if (isGameOver) return;

        lives--;
        UpdateLivesUI();

        if (lives <= 0)
        {
            GameOver();
        }

        Debug.Log("Life lost! Current lives: " + lives);
    }

    void UpdateLivesUI()
    {
        livesText.text = "Lives: " + lives;
    }

    public void GameOver()
    {
        isGameOver = true;

        Time.timeScale = 0f;

        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
