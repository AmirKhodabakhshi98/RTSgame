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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerUnit"))
        {
            AudioSource.PlayClipAtPoint(soundClip, transform.position);
            scoreManager.AddScore(score);
            Destroy(gameObject);
        } 
    }

}
