using Pathfinding;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float health = 100;
    public float maxHealth = 100;
    private AIDestinationSetter ads;
    private Transform target;
    private SpriteRenderer sr;
    private bool selected = false;
    public float attackRange = 5f;
    public float fireRate = 5f;
    public float turretRotationSpeed = 5f ;
    public int damage = 10;
    public float bulletSpeed = 5f;
    
    [SerializeField] private GameObject SelectedEffect;
    [SerializeField] private GameObject RangeEffect;

    private string myTag;
    private string enemyTag;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        ads = GetComponent<AIDestinationSetter>();
        
        if (gameObject.CompareTag("EnemyUnit"))
        {
            myTag = gameObject.tag;
            enemyTag = "PlayerUnit";
        }else if (gameObject.CompareTag("PlayerUnit"))
        {
            myTag = gameObject.tag;
            enemyTag = "EnemyUnit";
        }
        
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if units start moving weirdly on spawn look here XD //

        if(gameObject.CompareTag("PlayerUnit"))
        {
            target = new GameObject("ClickMarker").transform;
            RangeEffect.transform.localScale = new Vector3(attackRange * 2, attackRange * 2, 1);
        }


    }

    public string getMyTag()
    {
        return myTag;
    }

    public string getEnemyTag()
    {
        return enemyTag;
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPos.z = 0f;
                target.position = mouseWorldPos;
                ads.target = target;
            }
        }
        
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




    public void SetSelected(bool isSelected)
    {
        selected = isSelected;
        SelectedEffect.SetActive(selected);
        RangeEffect.SetActive(selected);
        
    }
}
