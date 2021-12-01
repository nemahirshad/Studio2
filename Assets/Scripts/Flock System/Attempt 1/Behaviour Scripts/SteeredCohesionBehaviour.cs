using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviours/SteeredCohesion")]
public class SteeredCohesionBehaviour : FlockBehavior
{
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;

    public override Vector2 CalculateMovement(FlockingAgent agentFlock, List<Transform> context, FlockScript flock)
    {
        //if no neighbors return, no adjustment necessary.
        if (context.Count == 0)
            return Vector2.zero;

        //Add all points together and take an average.
        Vector2 cohesionMove = Vector2.zero;

        foreach (Transform item in context)
        {
            cohesionMove += (Vector2)item.position;
        }

        cohesionMove /= context.Count;

        //create offset from Agent Pos
        cohesionMove -= (Vector2)agentFlock.transform.position;
        cohesionMove = Vector2.SmoothDamp(agentFlock.transform.forward, cohesionMove, ref currentVelocity, agentSmoothTime);
        return cohesionMove;
    }
}