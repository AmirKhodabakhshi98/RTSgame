using UnityEngine;

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

    private void Update()
    {
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        GameObject hitRoot = null;
        if (Physics.Raycast(ray, out hit))
        {
            // If collider is on a child, climb to the PlayerUnit root
            var unit = hit.collider.GetComponentInParent<PlayerUnit>();
            if (unit != null) hitRoot = unit.gameObject;
        }

        bool selectedAny = false;
        foreach (var go in GameObject.FindGameObjectsWithTag("PlayerUnit"))
        {
            var unit = go.GetComponent<PlayerUnit>();
            if (!unit) continue;

            bool selected = (hitRoot != null && hitRoot == go);
            unit.SetSelected(selected);
            if (selected) selectedAny = true;
        }

        if (!selectedAny)
        {
            // Clicked empty space -> clear selection
            foreach (var go in GameObject.FindGameObjectsWithTag("PlayerUnit"))
            {
                var unit = go.GetComponent<PlayerUnit>();
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
            var unit = go.GetComponent<PlayerUnit>();
            if (!unit) continue;

            // Use Collider bounds (works for Box/Sphere/Capsule/Mesh colliders)
            var col = go.GetComponentInChildren<Collider>();
            if (!col) { unit.SetSelected(false); continue; }

            Bounds b = col.bounds;

            // Project the 8 world-space corners of the bounds to screen space
            Vector3[] corners =
            {
                new Vector3(b.min.x, b.min.y, b.min.z),
                new Vector3(b.min.x, b.min.y, b.max.z),
                new Vector3(b.min.x, b.max.y, b.min.z),
                new Vector3(b.min.x, b.max.y, b.max.z),
                new Vector3(b.max.x, b.min.y, b.min.z),
                new Vector3(b.max.x, b.min.y, b.max.z),
                new Vector3(b.max.x, b.max.y, b.min.z),
                new Vector3(b.max.x, b.max.y, b.max.z),
            };

            bool anyInFront = false;
            float cMinX = float.PositiveInfinity, cMinY = float.PositiveInfinity;
            float cMaxX = float.NegativeInfinity, cMaxY = float.NegativeInfinity;

            for (int i = 0; i < corners.Length; i++)
            {
                Vector3 sp = Camera.main.WorldToScreenPoint(corners[i]);
                // Only consider points in front of the camera
                if (sp.z <= 0f) continue;
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

            // Now we have the collider's screen-space AABB: [cMinX..cMaxX] x [cMinY..cMaxY]

            // INTERSECTION test (select if rectangles overlap at all)
            bool overlap =
                !(selMax.x < cMinX || selMin.x > cMaxX || selMax.y < cMinY || selMin.y > cMaxY);

            // If you prefer "fully inside" selection, use this instead:
            // bool fullyInside = selMin.x <= cMinX && selMax.x >= cMaxX &&
            //                    selMin.y <= cMinY && selMax.y >= cMaxY;

            unit.SetSelected(overlap);
        }
    }
}
