using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlockBehavior : ScriptableObject
{
    //The List context refers to objects the Agent would need to take into consideration, like neighbors and walls when moving across the map.
    public abstract Vector2 CalculateMovement(FlockingAgent agentFlock, List<Transform> context, FlockScript flock);
}