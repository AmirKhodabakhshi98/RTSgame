using System;
using UnityEngine;

public class scoreObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int score = 1;
    public AudioClip soundClip;
    
    private ScoreManager scoreManager;
    private Evac evac;
    
    private void Start()
    {
        scoreManager = ScoreManager.instance;
        scoreManager.AddTotal(score);
        evac = Evac.instance;
    }
    [SerializeField] private AudioClip getToEvacSound;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerUnit"))
        {
            scoreManager.AddScore(score); //for ui
            if (evac.shouldPlayEvacSound())
            {
                AudioSource.PlayClipAtPoint(getToEvacSound, transform.position);
            }
            else
            {
                AudioSource.PlayClipAtPoint(soundClip, transform.position);
            }
            
            
            //LevelEnd.instance.ScoreTotalAdd(); //for post level screen totals
            Destroy(gameObject);
        } 
    }

}
