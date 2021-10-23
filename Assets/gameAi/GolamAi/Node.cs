using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int gridX;
    public int gridY;
    public int gCost;
    public int hCost;

    public bool bIsWall;
    public Vector3 worldPosition;
    public Node ParentNode;
    public int fCost { get { return gCost + hCost; } }

    public Node(bool _bIsWall, Vector3 vPos, int _gridX, int _gridY)
    {
        bIsWall = _bIsWall;
        worldPosition = vPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    
    
}
