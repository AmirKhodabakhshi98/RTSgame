using UnityEngine;

public class Range : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
   

    void Start()
    {
        GetComponent<CircleCollider2D>().radius = GetComponentInParent<PlayerUnit>().attackRange;
        

    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyUnit"))
        {
            //fire
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("EnemyUnit"))
        {
            //hold fire
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
