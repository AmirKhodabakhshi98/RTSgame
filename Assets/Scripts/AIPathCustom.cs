




//Create Class that inherits from another class

using System;
using UnityEngine;

public class AIPathCustom :MonoBehaviour //: Pathfinding.AIPath
{
    public Pathfinding.AIPath path;

    private void Awake()
    {
        path = GetComponentInParent<Pathfinding.AIPath>();
    }

    public int GetPathLength()
    {
        return path.path.vectorPath.Count;
    }

    public Vector3 GetPathPosition(int theIndex)
    {
        return path.path.vectorPath[theIndex];
    }
}