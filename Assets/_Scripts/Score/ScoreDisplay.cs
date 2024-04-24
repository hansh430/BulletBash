using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private ScoreSubject scoreSubject;
    private void OnEnable()
    {
        scoreSubject.SetOnScoreUpdatedAction(UpdateScore);
        scoreSubject.SetOnHighScoreUpdatedAction(UpdateHighScore);
    }
    private void OnDisable()
    {
        scoreSubject.RemoveOnScoreUpdatedAction(UpdateScore);
        scoreSubject.RemoveOnHighScoreUpdatedAction(UpdateHighScore);
    }

    private void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
    private void UpdateHighScore(int highScore)
    {
        highScoreText.text = highScore.ToString();
    }
}
