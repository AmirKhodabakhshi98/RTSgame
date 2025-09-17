using UnityEngine;
using System.Collections.Generic;

public class UnitSelector : MonoBehaviour

{
    [SerializeField] private RectTransform selectionBox;

    private Vector2 startPos;
    private Vector2 endPos;

    private void Start()
    {
        selectionBox.gameObject.SetActive(false);
    }
//klickar nån annanstans d som ny drag så den fattar att den ska unselecta
    private void Update()
    {
        // Start drag
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            selectionBox.gameObject.SetActive(true);
        }

        // Update drag
        if (Input.GetMouseButton(0))
        {
            endPos = Input.mousePosition;
            DrawSelection();
        }

        // Release
        if (Input.GetMouseButtonUp(0))
        {
            SelectUnits();
            selectionBox.gameObject.SetActive(false);
        }
    }

    private void DrawSelection()
    {
        Vector2 boxStart = startPos;
        Vector2 boxEnd = endPos;
        Vector2 center = (boxStart + boxEnd) / 2;

        selectionBox.position = center;

        float sizeX = Mathf.Abs(boxStart.x - boxEnd.x);
        float sizeY = Mathf.Abs(boxStart.y - boxEnd.y);

        selectionBox.sizeDelta = new Vector2(sizeX, sizeY);
    }

    private void SelectUnits()
    {
        Vector2 min = Vector2.Min(startPos, endPos);
        Vector2 max = Vector2.Max(startPos, endPos);

        foreach (var unit in GameObject.FindGameObjectsWithTag("PlayerUnit"))
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(unit.transform.position);
            if (screenPos.x > min.x && screenPos.x < max.x &&
                screenPos.y > min.y && screenPos.y < max.y)
            {
                unit.GetComponent<PlayerUnit>().SetSelected(true);
            }
            else
            {
                unit.GetComponent<PlayerUnit>().SetSelected(false);
            }
        }
        
    }
}