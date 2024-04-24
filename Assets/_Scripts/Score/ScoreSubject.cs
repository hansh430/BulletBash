using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSubject : MonoBehaviour
{
    public static ScoreSubject Instance;
    private int score = 0;
    private int highScore = 0;
    private Action<int> onScoreUpdated;
    private Action<int> onHighScoreUpdated;
    private string highScoreKey = "HighScore";

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey(highScoreKey))
        {
            highScore = PlayerPrefs.GetInt(highScoreKey);
            onHighScoreUpdated?.Invoke(highScore);
        }
    }
    public void IncreaseScore(int amount)
    {
        score += amount;
        onScoreUpdated?.Invoke(score);
    }

    public void UpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(highScoreKey, highScore);
            onHighScoreUpdated?.Invoke(highScore);
        }
    }

    public void SetScoreToLeaderBoard()
    {
        PlayfabLeaderboardManager.Instance.SendLeaderboard(score);
    }
    public void SetOnScoreUpdatedAction(Action<int> onScoreUpdated)
    {
        this.onScoreUpdated += onScoreUpdated;
    }
    public void RemoveOnScoreUpdatedAction(Action<int> onScoreUpdated)
    {
        this.onScoreUpdated -= onScoreUpdated;
    }
    public void SetOnHighScoreUpdatedAction(Action<int> onHighScoreUpdated)
    {
        this.onHighScoreUpdated += onHighScoreUpdated;
    }
    public void RemoveOnHighScoreUpdatedAction(Action<int> onHighScoreUpdated)
    {
        this.onHighScoreUpdated -= onHighScoreUpdated;
    }

}
