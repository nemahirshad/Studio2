using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseNode : Node
{
    public override NodeOutcome Execute(BehaviorTree bt)
    {
        ((ZombieAgent)bt).chasing = true;

        if (Vector3.Distance(bt.target.transform.position, bt.transform.position) > ((ZombieAgent)bt).attackRange)
        {
            ((ZombieAgent)bt).chasing = false;
            bt.anim.SetBool("Chasing", false);
            return NodeOutcome.FAIL;
        }

        bt.transform.position = Vector2.MoveTowards(bt.transform.position, bt.target.transform.position, ((ZombieAgent)bt).speed * Time.deltaTime);

        if (Vector2.Distance(bt.target.transform.position, bt.transform.position) < ((ZombieAgent)bt).attackRange)
        {
            return NodeOutcome.SUCCESS;
        }

        bt.anim.SetBool("Chasing", true);
        bt.anim.SetBool("Wandering", false);

        return NodeOutcome.RUNNING;
    }
}