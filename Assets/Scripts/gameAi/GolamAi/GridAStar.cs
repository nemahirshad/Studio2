using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridAStar : MonoBehaviour
{
    public Transform StartPositon;

    public LayerMask WallLayer;
    public Vector2 gridSize;
    public float fNodeRadius;
    public float fDistanceBetweenNode;
    private float fNodeDiameter;
    private int gridSizeX, gridSizeY;

    NodeAStar[,] nodeArray;
    public List<NodeAStar> shortestPath;


    void Update()
    {
        fNodeDiameter = fNodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridSize.x / fNodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridSize.y / fNodeDiameter);
        createGrid();
    }

    public Vector3 MovementCalc()
    {
        if(shortestPath != null)
        {
            Vector3 dir = shortestPath[0].worldPosition - StartPositon.position + new Vector3(0, 0.5f, 0);
            dir.Normalize();
            return dir;
        }
        return Vector3.zero;
    }

    void createGrid()
    {
        nodeArray = new NodeAStar[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridSize.x / 2 - Vector3.forward * gridSize.y / 2;
        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldLeft = bottomLeft + Vector3.right * (x * fNodeDiameter + fNodeRadius) + Vector3.forward * (y * fNodeDiameter + fNodeRadius);
                bool wall = true;

                if (Physics.CheckSphere(worldLeft, fNodeRadius, WallLayer))
                {
                    wall = false;
                }
                nodeArray[x, y] = new NodeAStar(wall, worldLeft, x, y);
            }
        }
    }

    public List<NodeAStar> getNeighboringNodes(NodeAStar _NeighborNode)
    {
        List<NodeAStar> neighborList = new List<NodeAStar>();
        int checkX;
        int checkY;

        checkX = _NeighborNode.gridX + 1;
        checkY = _NeighborNode.gridY;
        if(checkX >= 0 && checkX < gridSizeX)
        {
            if(checkY >= 0 && checkY < gridSizeY)
            {
                neighborList.Add(nodeArray[checkX, checkY]);
            }
        }

        checkX = _NeighborNode.gridX - 1;
        checkY = _NeighborNode.gridY;
        if (checkX >= 0 && checkX < gridSizeX)
        {
            if (checkY >= 0 && checkY < gridSizeY)
            {
                neighborList.Add(nodeArray[checkX, checkY]);
            }
        }

        checkX = _NeighborNode.gridX;
        checkY = _NeighborNode.gridY + 1;
        if (checkX >= 0 && checkX < gridSizeX)
        {
            if (checkY >= 0 && checkY < gridSizeY)
            {
                neighborList.Add(nodeArray[checkX, checkY]);
            }
        }

        checkX = _NeighborNode.gridX;
        checkY = _NeighborNode.gridY - 1;
        if (checkX >= 0 && checkX < gridSizeX)
        {
            if (checkY >= 0 && checkY < gridSizeY)
            {
                neighborList.Add(nodeArray[checkX, checkY]);
            }
        }

    return neighborList;
    }

    public NodeAStar nodeFromeWorldPoint(Vector3 _worldPos)
    {
        float xPos = ((_worldPos.x + gridSize.x / 2) / gridSize.x);
        float yPos = ((_worldPos.z + gridSize.y / 2) / gridSize.y);

        xPos = Mathf.Clamp01(xPos);
        yPos = Mathf.Clamp01(yPos);

        int _x = Mathf.RoundToInt((gridSizeX - 1) * xPos);
        int _y = Mathf.RoundToInt((gridSizeY - 1) * yPos);

        return nodeArray[_x, _y];
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, 1, gridSize.y));

        if (nodeArray != null)
        {
            foreach (NodeAStar n in nodeArray)
            {
                if (n.bIsWall)
                {
                    Gizmos.color = Color.white;
                }
                else
                {
                    Gizmos.color = Color.black;
                }

                if (shortestPath != null)
                {
                    if (shortestPath.Contains(n))
                    {
                        Gizmos.color = Color.green;
                    }
                }
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (fNodeDiameter - fDistanceBetweenNode));
            }
        }
    }
}
