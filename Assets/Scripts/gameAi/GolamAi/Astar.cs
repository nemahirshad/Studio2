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
        //FindPath(StartPosition.position, TargetPosition.position);
    }

    public void FindPath(Vector3 StartPos, Vector3 targetPos,GameObject AI)
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
                getFinalPath(startNode, targetNode,AI);
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

<<<<<<< HEAD:Assets/Scripts/gameAi/GolamAi/Astar.cs
<<<<<<< HEAD:Assets/Scripts/gameAi/GolamAi/Astar.cs
    void getFinalPath(NodeAStar startingNode, NodeAStar endNode)
=======
    void getFinalPath(Node startingNode, Node endNode, GameObject AI)
>>>>>>> 7d48e6f7538afef6f46ca19b2d41d13f6ba80f15:Assets/gameAi/GolamAi/Astar.cs
=======
    void getFinalPath(Node startingNode, Node endNode, GameObject AI)
>>>>>>> 7d48e6f7538afef6f46ca19b2d41d13f6ba80f15:Assets/gameAi/GolamAi/Astar.cs
    {
        List<NodeAStar> finalPath = new List<NodeAStar>();
        NodeAStar currentNode = endNode;

        while(currentNode != startingNode)
        {
            finalPath.Add(currentNode);
            currentNode = currentNode.ParentNode;
        }

        finalPath.Reverse();
        //gridReference.shortestPath = finalPath;
        AI.GetComponent<AIPathHolder>().shortestPath = finalPath;
        //ShortestPath = finalPath;
    }

    int getManhattenDistance(NodeAStar nodeA, NodeAStar nodeB)
    {
        int x = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int y = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        return x + y;
    }
}
