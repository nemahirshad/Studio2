using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
    Grid gridReference;
    public Transform StartPosition;
    public Transform TargetPosition;
    private void Awake()
    {
        gridReference = GetComponent<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        FindPath(StartPosition.position, TargetPosition.position);
    }

    void FindPath(Vector3 StartPos, Vector3 targetPos)
    {
        Node startNode = gridReference.nodeFromeWorldPoint(StartPos);
        Node targetNode = gridReference.nodeFromeWorldPoint(targetPos);

        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        openList.Add(startNode);
        while(openList.Count > 0)
        {
            Node currentNode = openList[0];
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

            foreach(Node neighborNode in gridReference.getNeighboringNodes(currentNode))
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

    void getFinalPath(Node startingNode, Node endNode)
    {
        List<Node> finalPath = new List<Node>();
        Node currentNode = endNode;

        while(currentNode != startingNode)
        {
            finalPath.Add(currentNode);
            currentNode = currentNode.ParentNode;
        }

        finalPath.Reverse();
        gridReference.shotestPath = finalPath;
    }

    int getManhattenDistance(Node nodeA, Node nodeB)
    {
        int x = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int y = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        return x + y;
    }
}
