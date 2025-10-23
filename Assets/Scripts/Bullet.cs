using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float range;
    private int damage;
    private float speed;
    public GameObject explosion;
    public GameObject obstacleExplosion;

    public AudioClip shootSound;
    public AudioClip explosionSoundMiss;
    public AudioClip explosionSoundHit;
    private Vector2 startPos;
    private float travelledDistance;
    private Rigidbody2D rb;
    private string myTag;
    private string enemyTag;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    public void Initialize(float r, string mt, string et, int d, float s)
    {
        range = r;
        myTag = mt;
        enemyTag = et;
        damage = d;
        speed = s;
        
        startPos = transform.position;
        rb.linearVelocity = transform.up * speed;
        AudioSource.PlayClipAtPoint(shootSound, transform.position);
    }



    // Update is called once per frame
    void Update()
    {
        travelledDistance = Vector2.Distance(transform.position, startPos);
        if (travelledDistance>range)
        {
            Destroy(gameObject);
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(enemyTag))
        {
            collision.GetComponent<Unit>().changeHealth(-damage);
            Instantiate(explosion,  transform.position, Quaternion.identity);
            //Instantiate(explosionSoundHit, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(explosionSoundHit, transform.position);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Obstacle"))
        {
            Instantiate(obstacleExplosion, transform.position, Quaternion.identity);
           // Instantiate(explosionSoundMiss, transform.position, Quaternion.identity);
          
           // AudioSource.PlayClipAtPoint(explosionSoundMiss, transform.position);
            Destroy(gameObject);
        }else
        { 
            Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        
        
    }
    
}


