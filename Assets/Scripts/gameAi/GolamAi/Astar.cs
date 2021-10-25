using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
    GridAStar gridReference;
    public Transform StartPosition;
    public Transform TargetPosition;
    private void Awake()
    {
        gridReference = GameObject.FindObjectOfType<GridAStar>().GetComponent<GridAStar>();
    }

    // Update is called once per frame
    void Update()
    {
        FindPath(StartPosition.position, TargetPosition.position);
    }

    void FindPath(Vector3 StartPos, Vector3 targetPos)
    {
        NodeAStar startNode = gridReference.nodeFromeWorldPoint(StartPos);
        NodeAStar targetNode = gridReference.nodeFromeWorldPoint(targetPos);

        List<NodeAStar> openList = new List<NodeAStar>();
        HashSet<NodeAStar> closedList = new HashSet<NodeAStar>();

        openList.Add(startNode);
        while(openList.Count > 0)
        {
            NodeAStar currentNode = openList[0];
            for(int i = 1; i < openList.Count; i++)
            {
                if (openList[i].fCost < currentNode.fCost || openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost)
                {
                    currentNode = openList[i];
                }
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if(currentNode == targetNode)
            {
                getFinalPath(startNode, targetNode);
            }

            foreach(NodeAStar neighborNode in gridReference.getNeighboringNodes(currentNode))
            {
                if(!neighborNode.bIsWall || closedList.Contains(neighborNode))
                {
                    continue;
                }
                int moveCost = currentNode.gCost + getManhattenDistance(currentNode, neighborNode);

                if(moveCost < neighborNode.gCost || !openList.Contains(neighborNode))
                {
                    neighborNode.gCost = moveCost;
                    neighborNode.hCost = getManhattenDistance(neighborNode, targetNode);
                    neighborNode.ParentNode = currentNode;

                    if (!openList.Contains(neighborNode))
                    {
                        openList.Add(neighborNode);
                    }
                }
            }
        }

    }

    void getFinalPath(NodeAStar startingNode, NodeAStar endNode)
    {
        List<NodeAStar> finalPath = new List<NodeAStar>();
        NodeAStar currentNode = endNode;

        while(currentNode != startingNode)
        {
            finalPath.Add(currentNode);
            currentNode = currentNode.ParentNode;
        }

        finalPath.Reverse();
        gridReference.shortestPath = finalPath;
    }

    int getManhattenDistance(NodeAStar nodeA, NodeAStar nodeB)
    {
        int x = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int y = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        return x + y;
    }
}
