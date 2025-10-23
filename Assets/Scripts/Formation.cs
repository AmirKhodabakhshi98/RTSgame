using System.Collections.Generic;
using UnityEngine;

public class Formation : MonoBehaviour
{

    public static Formation instance;
   
    
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }


    private List<GameObject> selected = new List<GameObject>();
    
    public void SetSelected(GameObject go, bool isSelected)
    {
        if (isSelected)
        {
            selected.Add(go);
        }
        else
        {
            selected.Remove(go);
        }
    }
    
    
    public float spacing = 1f;
    public bool includeCenter = true;
    public float jitter = 0f;
    
    public Transform getTarget(GameObject go, Transform target)
    {
        if (selected.Count == 1)
        {
            return target;
        }
        
            
        List<Vector2> spacedPos = GetSpacedPositions2D(target.position, selected.Count, spacing, includeCenter, jitter);
        
        int index = selected.IndexOf(go);
        
        target.position = spacedPos[index];
        return target;
    }
    
    
    public static List<Vector2> GetSpacedPositions2D(Vector2 center, int count, float spacing, bool includeCenter = false, float jitter = 0f)
    {
        var result3D = GetSpacedPositions(new Vector3(center.x, 0f, center.y), count, spacing, includeCenter, jitter);
        var result2D = new List<Vector2>(count);
        foreach (var p in result3D) result2D.Add(new Vector2(p.x, p.z));
        return result2D;
    }
    

    
    public static List<Vector3> GetSpacedPositions(Vector3 center, int count, float spacing, bool includeCenter = false, float jitter = 0f)
    {
        var result = new List<Vector3>(count);
        if (count <= 0) return result;

        // optional center slot
        if (includeCenter && count > 0)
        {
            result.Add(center);
            if (count == 1) return result;
        }

        int placed = result.Count;
        int ring = 1;

        
        while (placed < count)
        {
            float radius = ring * spacing;
            float circumference = 2f * Mathf.PI * radius;
            int slotsOnRing = Mathf.Max(6, Mathf.CeilToInt(circumference / spacing)); // at least hex ring

            // offset angle per ring to avoid straight radial lines
            float angleOffset = ring * 0.37f; // arbitrary offset (radians)

            for (int i = 0; i < slotsOnRing && placed < count; i++)
            {
                float t = (i / (float)slotsOnRing) * Mathf.PI * 2f + angleOffset;
                Vector3 pos = center + new Vector3(Mathf.Cos(t), 0f, Mathf.Sin(t)) * radius;

                if (jitter > 0f)
                {
                    float j = jitter * spacing;
                    pos += new Vector3(Random.Range(-j, j), 0f, Random.Range(-j, j));
                }

                result.Add(pos);
                placed++;
            }
            ring++;
        }

        return result;
    }
    
}
