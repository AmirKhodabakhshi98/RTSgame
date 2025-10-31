using System.Collections.Generic;
using Pathfinding;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    private ScoreManager scoreManager = null;

    public GameObject smokePlosion;
    public GameObject deathPlosion;
    
    [SerializeField] private GameObject SelectedEffect;
    [SerializeField] private GameObject RangeEffect;
    [SerializeField] private GameObject RangeIndicator;
    [SerializeField] private GameObject LinePath;
    private string myTag;
    private string enemyTag;
    
    private Slider healthBar;
    private CanvasGroup group;
    private Image groupFill;
    private Color groupFillOriginalColor;
    
    [SerializeField] private SpriteRenderer TankBaseRenderer;
    [SerializeField] private SpriteRenderer TankTurretRenderer;
    [SerializeField] private SpriteRenderer SelectedEffectRenderer;
    [SerializeField] private SpriteRenderer WarningRenderer;
    private GameObject WarningObject;
    private Color warningStart ;//= //new Color(Color.white.r, Color.white.g, Color.white.b, 1f);
    private Color warningEnd;// = //new Color(Color.white.r, Color.white.g, Color.white.b, 0f);
    
    private Formation formation;
    private List<int> controlGroups;
    private Color baseColor ;
    private Color turretColor;
    
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        ads = GetComponent<AIDestinationSetter>();
        
        
        if (gameObject.CompareTag("EnemyUnit"))
        {
            myTag = gameObject.tag;
            enemyTag = "PlayerUnit";
            flashColor1 = Color.red;
        }else if (gameObject.CompareTag("PlayerUnit"))
        {
            
            myTag = gameObject.tag;
            enemyTag = "EnemyUnit";
            SelectedEffect.SetActive(selected);
            RangeIndicator.GetComponent<RangeIndicator>().setSelected(selected);
            LinePath.GetComponent<LinePath>().setSelected(selected);
            warningStart = new Color(WarningRenderer.color.r, WarningRenderer.color.g, WarningRenderer.color.b, 1f);
            warningEnd = new Color(WarningRenderer.color.r, WarningRenderer.color.g, WarningRenderer.color.b, 0f);
            WarningObject = WarningRenderer.gameObject;
            flashColor1 = Color.blue;
        }
        healthBar = GetComponentInChildren<Slider>();
        group = healthBar.GetComponent<CanvasGroup>();
        if (!group) group = healthBar.gameObject.AddComponent<CanvasGroup>();
        groupFill = group.GetComponent<Slider>().fillRect.GetComponent<Image>();
        groupFillOriginalColor = groupFill.color;
        

      baseColor = TankBaseRenderer.color;
      turretColor = TankTurretRenderer.color;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if units start moving weirdly on spawn look here XD //
        
        
        if(gameObject.CompareTag("PlayerUnit"))
        {
            scoreManager = ScoreManager.instance;;
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


    [SerializeField] private int warningSoundThreshold = 20;
    [SerializeField] private AudioClip warningSound;

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
            Instantiate(smokePlosion, transform.position, Quaternion.identity);
            Instantiate(deathPlosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
        healthBar.value = health/maxHealth;
        if (myTag == "PlayerUnit")
        {
            if (health < warningSoundThreshold)
            {
                 AudioSource.PlayClipAtPoint(warningSound, transform.position);
                 StartWarningFade();
            }
         
        }


        StartFadeOut();
    }
    
    private Tween fadeTween;
    public Ease easeType = Ease.InOutSine;
    private Color flashColor = Color.white;


    public void StartWarningFade()
    {
        WarningRenderer.DOKill();
        WarningObject.transform.position = transform.position;
        WarningObject.transform.rotation = Quaternion.identity;
        
        WarningRenderer.color = warningStart;
        WarningRenderer.DOFade(0.5f, 0.05f)
            .SetLoops(20, LoopType.Yoyo)
            //  .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                WarningRenderer.color = warningEnd;
            });
        
    }


    
    private Color flashColor1;
    
    public void StartFadeOut()
    {        
        TankBaseRenderer.color = baseColor;
        TankTurretRenderer.color = turretColor;
    
        float prevAlpha = group.alpha;
        // Kill any existing fade tween before starting a new one
        if (fadeTween != null && fadeTween.IsActive())
        {
            fadeTween.Kill();
        }





        group.DOKill();
        group.alpha = 1f;
        
        groupFill.DOColor(flashColor, 0.1f)
            .SetLoops(2 * 2, LoopType.Yoyo)
            //.SetEase(Ease.Linear)
            .SetEase(Ease.OutSine)
            .OnComplete(() =>
            {
                groupFill.color = groupFillOriginalColor;
                group.alpha = prevAlpha;
            });



        TankBaseRenderer.DOKill();
        TankTurretRenderer.DOKill();
        


        TankBaseRenderer.DOColor(flashColor1, 0.05f)
            .SetLoops(2, LoopType.Yoyo)
            //.SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                TankBaseRenderer.color = baseColor;
            });

        
        TankTurretRenderer.DOColor(flashColor1, 0.05f)
            .SetLoops(2, LoopType.Yoyo)
            //.SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                TankTurretRenderer.color = turretColor;
            });

       // TankBaseRenderer.color = baseColor;
     //   TankTurretRenderer.color = turretColor;
    }

    public void SetSelected(bool isSelected)
    {
      //  Debug.Log(isSelected);
        selected = isSelected;
        formation.SetSelected(gameObject, selected);
        SelectedEffect.SetActive(selected);
        RangeIndicator.GetComponent<RangeIndicator>().setSelected(selected);
        group.alpha = selected ? 1f : 0f;
        LinePath.GetComponent<LinePath>().setSelected(selected);
        //RangeEffect.SetActive(selected);
        
    }

    public bool GetSelected()
    {
        return selected;
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
