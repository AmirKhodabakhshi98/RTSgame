using Pathfinding;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    
    public float health;
    private AIDestinationSetter ads;
    private Transform target;
    public float attackRange = 100f;
    public float maxHealth = 100;

    private void Awake()
    {
        
        ads = GetComponent<AIDestinationSetter>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void changeHealth(float amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
            //do sth for healing
        }

        if (health <= 0)
        {
            Destroy(gameObject);

        }
    }

}
