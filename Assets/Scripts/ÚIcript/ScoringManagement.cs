using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoringManagement : MonoBehaviour
{
    // public GameOver gameOverScreen;
    public static ScoringManagement instance;
    public int score = 0;
    public Text scoreText;  // Assign this through the inspector to display the score
    bool isPaused;
    
    void Start()
    {
        instance = this;
        UpdateScoreText();
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene load event
        isPaused = false;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from scene load event
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == SceneManager.GetActiveScene().name) // Replace with the actual scene name
        {
            // Reset score when the main game scene is loaded
            score = 0;
            UpdateScoreText();
            isPaused = false;
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        // gameOverScreen.Gameover(score);
    }


    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Coins: " + score;
        }
    }

}
