using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviours/Cohesion")]
public class CohesionBehaviour : FlockBehavior
{
    public override Vector2 CalculateMovement(FlockingAgent agentFlock, List<Transform> context, FlockScript flock)
    {
        //if no neighbors return, no adjustment necessary.
        if (context.Count == 0)
            return Vector3.zero;

        //Add all points together and take an average.
        Vector2 cohesionMove = Vector2.zero;

        foreach (Transform item in context)
        {
            cohesionMove += (Vector2)item.position;
        }

        cohesionMove /= context.Count;

        //create offset from Agent Pos
        cohesionMove -= (Vector2)agentFlock.transform.position;
        return cohesionMove;
    }
}