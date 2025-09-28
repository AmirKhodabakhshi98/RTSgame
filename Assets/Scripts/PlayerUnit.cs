using Pathfinding;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    
    private AIDestinationSetter ads;
    private Transform target;
    private SpriteRenderer sr;
    private bool selected = false;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        ads = GetComponent<AIDestinationSetter>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if units start moving weirdly on spawn look here XD
        target = new GameObject("ClickMarker").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPos.z = 0f;
                target.position = mouseWorldPos;
                ads.target = target;

            }
        }
        
    }
    




    [SerializeField] private GameObject SelectedEffect;

    public void SetSelected(bool isSelected)
    {
        selected = isSelected;
        SelectedEffect.SetActive(selected);
    }
}
