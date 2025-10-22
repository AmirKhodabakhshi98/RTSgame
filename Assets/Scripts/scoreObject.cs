using UnityEngine;

public class scoreObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int score = 1;
    public AudioClip soundClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerUnit"))
        {
            //incrs score +=score
            //AudioSource.PlayClipAtPoint(soundClip, transform.position);
            Destroy(gameObject);
        } 
    }

}
