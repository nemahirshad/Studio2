using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : Node
{
    public override NodeOutcome Execute(BehaviorTree bt)
    {
        for (int i = 0; i < childrenNodes.Count; i++)
        {
            NodeOutcome outcome = childrenNodes[i].Execute(bt);

            if (outcome == NodeOutcome.FAIL)
            {
                return NodeOutcome.FAIL;
            }

            if (outcome == NodeOutcome.RUNNING)
            {
                return NodeOutcome.RUNNING;
            }
        }
        return NodeOutcome.SUCCESS;
    }
}