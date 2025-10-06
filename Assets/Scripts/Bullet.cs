using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float range = 10;
    public int damage = 10;
    public float speed = 5;
    
    private Vector2 startPos;
    private float travelledDistance;
    private Rigidbody2D rb;
    private string myTag;
    private string enemyTag;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(float r, string mt, string et)
    {
        range = r;
        myTag = mt;
        enemyTag = et;
        
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
  /*
        switch (collision.tag)
        {
            case enemyTag:
                //damage
                collision.GetComponent<EnemyUnit>().changeHealth(-damage);
                //DEBUG here if we get weird behaviour where we collide once enemy is dead or something 
                Destroy(gameObject);
                break;
            
            case "Obstacle": 
                //DisableObject();
                Destroy(gameObject);
                break;
            default:
                Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                break;
        }
*/
        if (collision.CompareTag(enemyTag))
        {
            collision.GetComponent<Unit>().changeHealth(-damage);
        }
        else if (collision.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }else
        { 
            Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        
        
    }
    
}


