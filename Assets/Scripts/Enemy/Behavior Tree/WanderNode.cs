using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderNode : Node
{
    public override NodeOutcome Execute(BehaviorTree bt)
    {
        Vector3 desiredVelocity = ((ZombieAgent)bt).GetWanderForce();
        desiredVelocity = desiredVelocity.normalized * ((ZombieAgent)bt).maxSpeed;

        Vector3 steeringForce = desiredVelocity - ((ZombieAgent)bt).velocity;
        steeringForce = Vector3.ClampMagnitude(steeringForce, ((ZombieAgent)bt).maxForce);
        steeringForce /= ((ZombieAgent)bt).mass;

        ((ZombieAgent)bt).velocity = Vector3.ClampMagnitude(((ZombieAgent)bt).velocity + steeringForce, ((ZombieAgent)bt).maxSpeed);
        ((ZombieAgent)bt).transform.position += ((ZombieAgent)bt).velocity * Time.deltaTime;
        ((ZombieAgent)bt).transform.forward = ((ZombieAgent)bt).velocity.normalized;

        bt.anim.SetBool("Wandering", true);
        
        return NodeOutcome.SUCCESS;
    }
}
