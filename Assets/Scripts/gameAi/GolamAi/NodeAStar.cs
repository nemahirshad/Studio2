using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Brendan
{
    public class Node
    {
        // Holds X and Y position relative to the grid.
        public int gridX;
        public int gridY;
        //Holds the g and h costs
        public int gCost;
        public int hCost;
        //holds a bool which indicates whether there is a wall blocking this node or not
        public bool bIsWall;
        // holds the world position of the node
        public Vector3 worldPosition;
        public Node ParentNode;
        // calculates and returns fcost
        public int fCost { get { return gCost + hCost; } }

        // used to assign values for each node instance from grid manager
        public Node(bool _bIsWall, Vector3 vPos, int _gridX, int _gridY)
        {
            bIsWall = _bIsWall;
            worldPosition = vPos;
            gridX = _gridX;
            gridY = _gridY;
        }



    }
}
