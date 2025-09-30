using Pathfinding;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    
    private AIDestinationSetter ads;
    private Transform target;
    private SpriteRenderer sr;
    private bool selected = false;
    public float attackRange = 5f;
    
    [SerializeField] private GameObject SelectedEffect;
    [SerializeField] private GameObject RangeEffect;

    
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        ads = GetComponent<AIDestinationSetter>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if units start moving weirdly on spawn look here XD //
        
        target = new GameObject("ClickMarker").transform;
        RangeEffect.transform.localScale = new Vector3(attackRange * 2, attackRange * 2, 1);

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
    

    




    public void SetSelected(bool isSelected)
    {
        selected = isSelected;
        SelectedEffect.SetActive(selected);
        RangeEffect.SetActive(selected);
        
    }
}
