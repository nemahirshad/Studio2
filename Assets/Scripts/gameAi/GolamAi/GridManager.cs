using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Brendan
{
    public class GridManager : MonoBehaviour
    {
        //Will be used to declare untraversable terrail physics layer.
        public LayerMask WallLayer;
        public Vector2 gridSize;
        // used to assign the the size of each individual nodes.
        public float fNodeRadius;
        public float fDistanceBetweenNode;

        //calculated based on the input variables above.
        private float fNodeDiameter;
        private int gridSizeX, gridSizeY;

        Node[,] nodeArray;

        // Assigning variables then creating grid.
        void Awake()
        {
            fNodeDiameter = fNodeRadius * 2;
            gridSizeX = Mathf.RoundToInt(gridSize.x / fNodeDiameter);
            gridSizeY = Mathf.RoundToInt(gridSize.y / fNodeDiameter);
            CreateGrid();
        }


        void CreateGrid()
        {
            //Assigning size to the node array.
            nodeArray = new Node[gridSizeX, gridSizeY];
            Vector3 bottomLeft = transform.position - Vector3.right * gridSize.x / 2 - Vector3.forward * gridSize.y / 2;

            // start creating the nodes in the grid from the bottom left and checking whether if the nodes being created are surrounded by object with a collider that
            // have the same physics layer used to declare untraversable terrain.
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    Vector3 worldLeft = bottomLeft + Vector3.right * (x * fNodeDiameter + fNodeRadius) + Vector3.forward * (y * fNodeDiameter + fNodeRadius);
                    bool wall = true;
                    // checks if there is a wall or not.
                    if (Physics.CheckSphere(worldLeft, fNodeRadius, WallLayer))
                    {
                        wall = false;
                    }
                    nodeArray[x, y] = new Node(wall, worldLeft, x, y);
                }
            }
        }
        // checks the neighbouring nodes in the order of Right neighbour, left neighbour, top neighbour and bottom neighbour and returns a list of neighbours for the node that's being checked.
        public List<Node> getNeighboringNodes(Node _NeighborNode)
        {
            List<Node> neighborList = new List<Node>();
            int checkX;
            int checkY;
            // right neighbour
            checkX = _NeighborNode.gridX + 1;
            checkY = _NeighborNode.gridY;
            if (checkX >= 0 && checkX < gridSizeX)
            {
                if (checkY >= 0 && checkY < gridSizeY)
                {
                    neighborList.Add(nodeArray[checkX, checkY]);
                }
            }
            // left neighbour
            checkX = _NeighborNode.gridX - 1;
            checkY = _NeighborNode.gridY;
            if (checkX >= 0 && checkX < gridSizeX)
            {
                if (checkY >= 0 && checkY < gridSizeY)
                {
                    neighborList.Add(nodeArray[checkX, checkY]);
                }
            }


            // top neighbour
            checkX = _NeighborNode.gridX;
            checkY = _NeighborNode.gridY + 1;
            if (checkX >= 0 && checkX < gridSizeX)
            {
                if (checkY >= 0 && checkY < gridSizeY)
                {
                    neighborList.Add(nodeArray[checkX, checkY]);
                }
            }


            // bottom neighbour
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
        // Assuming the Grid is placed at Origin (0,0,0) It will calculate and return the specific node that we want from the node array based on the world position.
        public Node nodeFromeWorldPoint(Vector3 _worldPos)
        {
            float xPos = ((_worldPos.x + gridSize.x / 2) / gridSize.x);
            float yPos = ((_worldPos.z + gridSize.y / 2) / gridSize.y);

            xPos = Mathf.Clamp01(xPos);
            yPos = Mathf.Clamp01(yPos);

            int _x = Mathf.RoundToInt((gridSizeX - 1) * xPos);
            int _y = Mathf.RoundToInt((gridSizeY - 1) * yPos);

            // returns the index of the node from the array
            return nodeArray[_x, _y];
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, 1, gridSize.y));

            if (nodeArray != null)
            {
                foreach (Node n in nodeArray)
                {
                    if (n.bIsWall)
                    {
                        Gizmos.color = Color.white;
                    }
                    else
                    {
                        Gizmos.color = Color.black;
                    }

                  
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (fNodeDiameter - fDistanceBetweenNode));
                }
            }
        }
    }
}
