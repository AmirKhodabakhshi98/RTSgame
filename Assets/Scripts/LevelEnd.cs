using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioClip soundClip;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerUnit"))
        {
            AudioSource.PlayClipAtPoint(soundClip, transform.position);
            
            Destroy(gameObject);
        } 
    }
}
