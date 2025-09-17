using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    [SerializeField] private GameObject SelectedEffect;

    public void SetSelected(bool isSelected)
    {
        SelectedEffect.SetActive(isSelected);
    }
}
