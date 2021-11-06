using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Brendan;

namespace Brendan
{
    public class Astar : MonoBehaviour
    {
        Grid gridReference;
        private Transform StartPosition;
        private Transform TargetPosition;
        private void Awake()
        {
            gridReference = GameObject.FindObjectOfType<Grid>();
        }

        // Update is called once per frame
        void Update()
        {
            //FindPath(StartPosition.position, TargetPosition.position);
        }

        public void FindPath(Vector3 StartPos, Vector3 targetPos, GameObject AI)
        {
            Node startNode = gridReference.nodeFromeWorldPoint(StartPos);
            Node targetNode = gridReference.nodeFromeWorldPoint(targetPos);

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
