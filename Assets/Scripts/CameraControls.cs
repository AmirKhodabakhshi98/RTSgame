using UnityEngine;

public class CameraControls : MonoBehaviour
{
    
    
    public float moveSpeed = 5f;

    [Header("Zoom Settings")]
    public float zoomSpeed = 5f;
    public float minZoom = 3f;
    public float maxZoom = 15f;
    
    private Camera cam;
    private Vector3 dragOrigin;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponent<Camera>();
    }


    // Update is called once per frame
    void Update()
    {
        cameraMove();
        cameraZoom();
        cameraMmb();
    }

    //TODO: bounds so u cant go outside map. loooow prio
    void cameraMmb()
    {
        if (Input.GetMouseButtonDown(2)) // mmb press get orig for movbe
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(2)) // mmb drag
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            transform.position += difference;
        }
    }
    
    void cameraMove()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // AD 
        float moveY = Input.GetAxisRaw("Vertical");   // WS

        Vector3 move = new Vector3(moveX, moveY, 0).normalized;
        transform.position +=  moveSpeed * Time.deltaTime * move;
    }

    void cameraZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel"); // forward = positive, back = negative

        if (scroll != 0f)
        {
            float newSize = cam.orthographicSize - scroll * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
        }
    }
}
