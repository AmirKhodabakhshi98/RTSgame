using UnityEngine;
using UnityEngine.UIElements;

public class UnitSelector : MonoBehaviour
{
    [SerializeField] private RectTransform selectionBox;

    private Vector2 startPos;
    private Vector2 endPos;
    

    [SerializeField] private float clickThreshold = 6f; // pixels to decide click vs drag

    private void Start()
    {
        selectionBox.gameObject.SetActive(false);
    }

    private int index = 0;
    private GameObject lastSelected;
    private void Update()
    {
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log(index);
            if (lastSelected != null)
            {
                lastSelected.GetComponent<Unit>().SetSelected(false);
            }
            Debug.Log(index);
            index++;
            index = index % playerUnits.Length;
            Debug.Log(index);
            playerUnits[index].GetComponent<Unit>().SetSelected(true);
            lastSelected = playerUnits[index];
        }
        
        
        if (Input.GetMouseButtonDown(0)) 
        {
            startPos = Input.mousePosition;
            endPos = startPos;
            selectionBox.gameObject.SetActive(true);
            DrawSelection();
        }

        if (Input.GetMouseButton(0))
        {
            endPos = Input.mousePosition;
            DrawSelection();
        }

        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;

            if (Vector2.Distance(startPos, endPos) < clickThreshold)
                SelectByClick();
            else
                SelectByBox();

            selectionBox.gameObject.SetActive(false);
        }


    }

    private void DrawSelection()
    {
        Vector2 center = (startPos + endPos) * 0.5f;
        selectionBox.position = center;
        selectionBox.sizeDelta = new Vector2(Mathf.Abs(startPos.x - endPos.x), Mathf.Abs(startPos.y - endPos.y));
    }

    private void SelectByClick()
    {
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mouseWorld, Vector2.zero);
        GameObject hitRoot = null;

        //if (hit.collider != null)
        if (hit.collider != null && !hit.collider.isTrigger)
        {
            var unit = hit.collider.GetComponentInParent<Unit>();
            if (unit != null) hitRoot = unit.gameObject;
        }

        bool selectedAny = false;
        foreach (var go in GameObject.FindGameObjectsWithTag("PlayerUnit"))
        {
            var unit = go.GetComponent<Unit>();
            if (!unit) continue;

            bool selected = (hitRoot != null && hitRoot == go);
            unit.SetSelected(selected);
            if (selected) selectedAny = true;
        }

        if (!selectedAny)             // Clicked empty space -> clear selection

       //if (Input.GetMouseButtonDown(1) || !selectedAny)  // rioght click deseclt
           {
                foreach (var go in GameObject.FindGameObjectsWithTag("PlayerUnit"))
                {
                    var unit = go.GetComponent<Unit>();
                    if (unit) unit.SetSelected(false);
                }
            }
    }

    private void SelectByBox()
    {
        // Selection rect in screen space
        Vector2 selMin = Vector2.Min(startPos, endPos);
        Vector2 selMax = Vector2.Max(startPos, endPos);

        foreach (var go in GameObject.FindGameObjectsWithTag("PlayerUnit"))
        {
            var unit = go.GetComponent<Unit>();
            if (!unit) continue;

            var col = go.GetComponentInChildren<Collider2D>();
            if (!col) { unit.SetSelected(false); continue; }

            Bounds b = col.bounds;

            // 4 world-space corners of collider bounds
            Vector3[] corners =
            {
                new Vector3(b.min.x, b.min.y, 0),
                new Vector3(b.min.x, b.max.y, 0),
                new Vector3(b.max.x, b.min.y, 0),
                new Vector3(b.max.x, b.max.y, 0),
            };

            bool anyInFront = false;
            float cMinX = float.PositiveInfinity, cMinY = float.PositiveInfinity;
            float cMaxX = float.NegativeInfinity, cMaxY = float.NegativeInfinity;

            for (int i = 0; i < corners.Length; i++)
            {
                Vector3 sp = Camera.main.WorldToScreenPoint(corners[i]);
                if (sp.z <= 0f) continue; // behind camera
                anyInFront = true;
                if (sp.x < cMinX) cMinX = sp.x;
                if (sp.y < cMinY) cMinY = sp.y;
                if (sp.x > cMaxX) cMaxX = sp.x;
                if (sp.y > cMaxY) cMaxY = sp.y;
            }

            if (!anyInFront)
            {
                unit.SetSelected(false);
                continue;
            }

            // Collider's screen-space AABB
            bool overlap =
                !(selMax.x < cMinX || selMin.x > cMaxX || selMax.y < cMinY || selMin.y > cMaxY);

            unit.SetSelected(overlap);
        }
    }
}
