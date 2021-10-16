using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNode : Node
{
    public override NodeOutcome Execute(BehaviorTree bt)
    {
        if (Vector2.Distance(bt.transform.position, bt.player.transform.position) < ((ZombieAgent)bt).attackRange && ((ZombieAgent)bt).attackCountdown <= 0)
        {
            //Deal Damage
            //Reset Cooldown

            bt.anim.SetTrigger("Attack");

            return NodeOutcome.SUCCESS;
        }
        else
        {
            return NodeOutcome.FAIL;
        }
    }
}