using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float range;
    public int damage;
    public float speed;
    
    private Vector2 startPos;
    private float travelledDistance;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize()
    {
        //range = GetComponentInParent<PlayerUnit>().attackRange;
        startPos = transform.position;
        rb.linearVelocity = transform.up * speed;
    }

    void Start()
    {
        //range = GetComponentInParent<PlayerUnit>().attackRange;
        
    }

    // Update is called once per frame
    void Update()
    {
        travelledDistance = Vector2.Distance(transform.position, startPos);
        if (travelledDistance>range)
        {
            DisableObject();
        }
        
    }

    private void DisableObject()
    {
        rb.linearVelocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "EnemyUnit":
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
        
    }
    
}


