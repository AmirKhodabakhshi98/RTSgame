using System;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    private int totalScore;
    public int evacScore = 1;
    
    public static ScoreManager instance;
    
    private TextMeshProUGUI scoreText;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void Start()
    {
      //  scoreText = Evac.instance.GetComponentInChildren<TextMeshProUGUI>();
      
      
      updateScoreText();
    }

    private void updateScoreText()
    {
        Evac.instance.updateText(evacScore - score);

    }
    

    public void AddTotal(int scoreToAdd)
    {
        totalScore += scoreToAdd;
    }


    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        updateScoreText();
    }

    public int getScore()
    {
        return score;
    }

    public int getTotalScore()
    {
        return totalScore;
    }
    
    
}
