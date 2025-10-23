using System;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score;
    private int totalScore;
    
    public static ScoreManager instance;
    [SerializeField] private TMP_Text scoreText;
    
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void OnGUI()
    {
        
        scoreText.text = score.ToString() + " / " + totalScore.ToString();
    }

    public void AddTotal(int scoreToAdd)
    {
        totalScore += scoreToAdd;
    }


    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
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
