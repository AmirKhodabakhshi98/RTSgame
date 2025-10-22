using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
    
}
