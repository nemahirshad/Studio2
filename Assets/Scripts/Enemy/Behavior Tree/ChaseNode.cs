using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseNode : Node
{
    public override NodeOutcome Execute(BehaviorTree bt)
    {
        if (Vector3.Distance(bt.target.transform.position, bt.transform.position) > ((ZombieAgent)bt).attackRange * 2)
        {
            bt.anim.SetBool("Chasing", false);
            return NodeOutcome.FAIL;
        }
        ((ZombieAgent)bt).point.position = ((ZombieAgent)bt).player.transform.position;

        if (Vector2.Distance(bt.target.transform.position, bt.transform.position) < ((ZombieAgent)bt).attackRange)
        {
            return NodeOutcome.SUCCESS;
        }
        else
        {
            ((ZombieAgent)bt).canFollow = true;
        }

        bt.anim.SetBool("Chasing", true);
        bt.anim.SetBool("Wandering", false);

        return NodeOutcome.RUNNING;
    }
}