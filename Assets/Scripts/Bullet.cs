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
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Obstacle"))
        {
            Instantiate(obstacleExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }else
        { 
            Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        
        
    }
    
}


