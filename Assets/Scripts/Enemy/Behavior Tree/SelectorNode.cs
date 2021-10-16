using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : Node
{
    public override NodeOutcome Execute(BehaviorTree bt)
    {
        for (int i = 0; i < childrenNodes.Count; i++)
        {
            NodeOutcome outcome = childrenNodes[i].Execute(bt);

            if (outcome == NodeOutcome.SUCCESS)
            {
                return NodeOutcome.SUCCESS;
            }

            if (outcome == NodeOutcome.RUNNING)
            {
                return NodeOutcome.RUNNING;
            }
        }
        return NodeOutcome.FAIL;
    }
}