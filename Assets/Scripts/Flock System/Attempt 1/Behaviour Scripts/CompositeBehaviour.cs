using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviours/Composite")]
public class CompositeBehaviour : FlockBehavior
{
    public FlockBehavior[] flockBehaviours;
    public float[] weights;

    public override Vector2 CalculateMovement(FlockingAgent agentFlock, List<Transform> context, FlockScript flock)
    {
        //If the two array lengths are not equal, stop moving.
        if (weights.Length != flockBehaviours.Length)
        {
            Debug.LogError("Array count is not equal in " + name, this);
            return Vector2.zero;
        }

        //setting up movement
        Vector2 agentMove = Vector2.zero;

        //iterate through all behaviours
        for (int i = 0; i < flockBehaviours.Length; i++)
        {
            Vector2 partialMove = flockBehaviours[i].CalculateMovement(agentFlock, context, flock) * weights[i];

            if (partialMove != Vector2.zero)
            {
                if (partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }
                
                agentMove += partialMove;
            }
        }

        return agentMove;
    }
}