using System;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource healthSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("PlayerUnit"))
        {
            collision.GetComponent<Unit>().changeHealth(100);
            healthSound.Play();
            //Destroy(gameObject);
        }
        
    }

}
