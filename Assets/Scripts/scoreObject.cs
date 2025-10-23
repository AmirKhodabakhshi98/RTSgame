using System;
using UnityEngine;

public class scoreObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int score = 1;
    public AudioClip soundClip;
    
    private ScoreManager scoreManager;
    
    private void Start()
    {
        scoreManager = ScoreManager.instance;
        scoreManager.AddTotal(score);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerUnit"))
        {
            AudioSource.PlayClipAtPoint(soundClip, transform.position);
            scoreManager.AddScore(score); //for ui
            //LevelEnd.instance.ScoreTotalAdd(); //for post level screen totals
            Destroy(gameObject);
        } 
    }

}
