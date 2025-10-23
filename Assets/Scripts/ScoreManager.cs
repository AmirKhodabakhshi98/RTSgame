using System;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score;
    
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
        scoreText.text = score.ToString();
    }


    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
    
}
