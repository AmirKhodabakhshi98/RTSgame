using Pathfinding;
using UnityEngine;
using UnityEngine.UI;

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
    public AudioClip deathClip;

    
    [SerializeField] private GameObject SelectedEffect;
    [SerializeField] private GameObject RangeEffect;
    [SerializeField] private GameObject RangeIndicator;
    private string myTag;
    private string enemyTag;
    private Slider healthBar;
    private CanvasGroup group;

    private Formation formation;
    
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
            SelectedEffect.SetActive(selected);
            RangeIndicator.GetComponent<RangeIndicator>().setSelected(selected);
        }
        healthBar = GetComponentInChildren<Slider>();
        group = healthBar.GetComponent<CanvasGroup>();
        if (!group) group = healthBar.gameObject.AddComponent<CanvasGroup>();
        
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if units start moving weirdly on spawn look here XD //
        

        if(gameObject.CompareTag("PlayerUnit"))
        {
            
            target = new GameObject("ClickMarker").transform;
            RangeEffect.transform.localScale = new Vector3(attackRange * 2, attackRange * 2, 1);
            LevelEnd.instance.Register();
            group.alpha = 0f;
            formation = Formation.instance;
        }
        healthBar.value = health/maxHealth;

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
                target = formation.getTarget(gameObject,target);
                ads.target = target;
            }
        }
        
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(myTag))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);
        }
    }

    public void changeHealth(float amount)
    {
        
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(deathClip, transform.position);
            
            if(myTag == "PlayerUnit"){
                LevelEnd.instance.Unregister();   
            }
            Destroy(gameObject);
        }
        
        healthBar.value = health/maxHealth;
        
    }




    public void SetSelected(bool isSelected)
    {
        selected = isSelected;
        formation.SetSelected(gameObject, selected);
        SelectedEffect.SetActive(selected);
        RangeIndicator.GetComponent<RangeIndicator>().setSelected(selected);
        group.alpha = selected ? 1f : 0f;
        //RangeEffect.SetActive(selected);
        
    }
    
    
    
    public string getMyTag()
    {
        return myTag;
    }

    public string getEnemyTag()
    {
        return enemyTag;
    }
}
