using UnityEngine;

public class RangeIndicator : MonoBehaviour
{
    private float range = 5f;
    public LayerMask obstacleMask;
    public int segments = 128;        // Number of points in the ring
    public float updateInterval = 0.1f;
    public float lineWidth = 0.05f;
    private LineRenderer lr;
    private float updateTimer;
    public Color lineColor;
    private bool selected = false;
    private Vector3[] currentPoints;
    private Vector3[] targetPoints;
    
    [Range(0.01f, 1f)] public float smoothing = 0.2f;
    
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.startColor =  lineColor;
        lr.endColor = lineColor;
        lr.loop = true;
        lr.useWorldSpace = true;
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;
        lr.positionCount = segments;
        
        currentPoints = new Vector3[segments];
        targetPoints = new Vector3[segments];
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        range = GetComponentInParent<Unit>().attackRange;
        lastPos = transform.position;
    }

    private Vector3 lastPos;
    void Update()
    {
        if (selected )//|| transform.position != lastPos)
        {
            /*
            updateTimer -= Time.deltaTime;
            if (updateTimer <= 0f)
            {
                updateTimer = updateInterval;
                DrawRangeRing();
                lastPos = transform.position;
            }
            */
            UpdateTargetPoints();
            SmoothUpdateLine();
        }

    }

    void UpdateTargetPoints()
    {
        Vector2 origin = transform.position;
        float angleStep = 360f / segments;

        for (int i = 0; i < segments; i++)
        {
            float angle = angleStep * i * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            RaycastHit2D hit = Physics2D.Raycast(origin, dir, range, obstacleMask);
            float dist = hit ? hit.distance : range;

            // keep slightly in front of sprites to avoid z-fighting
            targetPoints[i] = origin + dir * dist + Vector2.up * 0.0001f;
        }
    }

    void SmoothUpdateLine()
    {
        for (int i = 0; i < segments; i++)
        {
            // blend toward new position for smooth movement
            currentPoints[i] = Vector3.Lerp(currentPoints[i], targetPoints[i], smoothing);
            lr.SetPosition(i, currentPoints[i]);
        }
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (lr == null) lr = GetComponent<LineRenderer>();
        lr.positionCount = Mathf.Max(12, segments);
        lr.startWidth = lr.endWidth = lineWidth;
        lr.startColor = lr.endColor = lineColor;
    }
#endif
    public void setSelected(bool selected)
    {
        this.selected = selected;
    }
    
    void DrawRangeRing()
    {
        Vector2 origin = transform.position;
        float angleStep = 360f / segments;

        for (int i = 0; i < segments; i++)
        {
            float angle = angleStep * i * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            // Raycast outwards — stop at obstacles
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, range, obstacleMask);

            float dist = hit ? hit.distance : range;
            Vector3 point = origin + direction * dist;

            lr.SetPosition(i, point);
        }
    }
    
}
