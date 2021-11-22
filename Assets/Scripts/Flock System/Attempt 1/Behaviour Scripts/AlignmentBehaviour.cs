using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviours/Alignment")]
public class AlignmentBehaviour : FlockBehavior
{
    public override Vector2 CalculateMovement(FlockingAgent agentFlock, List<Transform> context, FlockScript flock)
    {
        //if no neighbors return, maintain current alignment.
        if (context.Count == 0)
            return agentFlock.transform.forward;

        //Add all points together and take an average.
        Vector2 alignmentMove = Vector2.zero;

        foreach (Transform item in context)
        {
            alignmentMove += (Vector2)item.transform.forward;
        }
        alignmentMove /= context.Count;
        
        return alignmentMove;
    }
}