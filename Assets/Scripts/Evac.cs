using TMPro;
using UnityEngine;

public class Evac : MonoBehaviour
{
    
    public AudioClip soundClip;
    private TextMeshProUGUI text;
    public GameObject plane;
    public GameObject scoreCanvas;
    public static Evac instance;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    

    private bool done = false;
    public void updateText(int scoreLeft)
    {
        if (scoreLeft <= 0)
        {
            if (!done)
            {
                done = true;
                scoreCanvas.SetActive(false);
                plane.SetActive(true);
            }
        }
        else
        {
            text.text = scoreLeft.ToString();
        }
        
        
    }
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerUnit") && LevelEnd.instance.canEvac())
        {
            AudioSource.PlayClipAtPoint(soundClip, transform.position);
            LevelEnd.instance.Evacuated();
            Destroy(gameObject);
        } 
    }
}
