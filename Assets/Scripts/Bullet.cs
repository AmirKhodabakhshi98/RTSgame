using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float range;
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
        range = GetComponentInParent<PlayerUnit>().attackRange;
        startPos = transform.position;
        rb.linearVelocity = transform.up * speed;
    }

    void Start()
    {
        range = GetComponentInParent<PlayerUnit>().attackRange;
        
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
        
        if (collision.CompareTag("EnemyUnit") || collision.CompareTag("Obstacle") )
        {
            DisableObject();    
        }

        switch (collision.tag)
        {
            case "EnemyUnit":
                //damage
                DisableObject();
                break;
            
            case "Obstacle":
                DisableObject();;
                break;
        }
        
    }
    
}


