using UnityEngine;

public class Evac : MonoBehaviour
{
    
    public AudioClip soundClip;
    
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
