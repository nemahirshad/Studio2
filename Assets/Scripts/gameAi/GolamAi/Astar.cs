using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Brendan;

namespace Brendan
{
    public class Astar : MonoBehaviour
    {
        GridManager gridReference;

        private void Start()
        {
            gridReference = GameObject.FindObjectOfType<GridManager>();
        }

        // Update is called once per frame
        void Update()
        {
            //FindPath(StartPosition.position, TargetPosition.position);
        }

        public void FindPath(Vector3 StartPos, Vector3 targetPos, GameObject AI)
        {
            //start should be -51, 0, -18
            //end should be -53, 0, -32
            Node startNode = gridReference.nodeFromeWorldPoint(StartPos); //worldPosition = "(-85.0, 0.0, -28.0)"
            Node targetNode = gridReference.nodeFromeWorldPoint(targetPos); //worldPosition = "(-85.0, 0.0, -42.0)"

            List<Node> openList = new List<Node>();
            HashSet<Node> closedList = new HashSet<Node>();

            openList.Add(startNode);
            while (openList.Count > 0)
            {
                Node currentNode = openList[0];
                for (int i = 1; i < openList.Count; i++)
                {
                    if (openList[i].fCost < currentNode.fCost || openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost)
                    {
                        currentNode = openList[i];
                    }
                    
                }
                openList.Remove(currentNode);
                closedList.Add(currentNode);

                if (currentNode == targetNode)
                {
                    getFinalPath(startNode, targetNode, AI);
                }

                foreach (Node neighborNode in gridReference.getNeighboringNodes(currentNode))
                {
                    if (!neighborNode.bIsWall || closedList.Contains(neighborNode))
                    {
                        continue;
                    }
                    int moveCost = currentNode.gCost + getManhattenDistance(currentNode, neighborNode);

                    if (moveCost < neighborNode.gCost || !openList.Contains(neighborNode))
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

        void getFinalPath(Node startingNode, Node endNode, GameObject AI)
        {
            List<Node> finalPath = new List<Node>();
            Node currentNode = endNode;

            while (currentNode != startingNode)
            {
                finalPath.Add(currentNode);
                currentNode = currentNode.ParentNode;
            }

            finalPath.Reverse();
            //gridReference.shortestPath = finalPath;
            AI.GetComponent<AIPathHolder>().shortestPath = finalPath;
            //ShortestPath = finalPath;
        }

        int getManhattenDistance(Node nodeA, Node nodeB)
        {
            int x = Mathf.Abs(nodeA.gridX - nodeB.gridX);
            int y = Mathf.Abs(nodeA.gridY - nodeB.gridY);

            return x + y;
        }
    }
}
