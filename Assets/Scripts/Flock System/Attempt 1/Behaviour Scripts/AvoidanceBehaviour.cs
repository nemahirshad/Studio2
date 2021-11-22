using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviours/Avoidance")]
public class AvoidanceBehaviour : FlockBehavior
{
    public override Vector2 CalculateMovement(FlockingAgent agentFlock, List<Transform> context, FlockScript flock)
    {
        //if no neighbors return, no adjustment necessary.
        if (context.Count == 0)
            return Vector3.zero;

        //Add all points together and take an average.
        Vector2 avoidanceMove = Vector2.zero;

        int numToAvoid = 0;

        foreach (Transform item in context)
        {
            if(Vector2.SqrMagnitude(item.position - agentFlock.transform.position) < flock.squareAvoidanceRadius)
            {
                numToAvoid++;
                avoidanceMove += (Vector2)(agentFlock.transform.position - item.position);
            }
        }

        if (numToAvoid > 0)
            avoidanceMove /= numToAvoid;

        return avoidanceMove;
    }
}