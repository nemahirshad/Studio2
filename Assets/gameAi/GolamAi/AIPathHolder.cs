using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Brendan
{
    public class AIPathHolder : MonoBehaviour
    {
        public List<Node> shortestPath;

        //private void OnDrawGizmos()
        //{
        //    if (shortestPath != null)
        //    {
        //        foreach (Node n in shortestPath)
        //        { 
        //            Gizmos.color = Color.green;
        //            Gizmos.DrawCube(n.worldPosition, new Vector3(2, 2, 2));
        //        }
        //    }
        //}
    }
}