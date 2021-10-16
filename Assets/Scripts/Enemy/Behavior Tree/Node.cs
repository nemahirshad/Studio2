using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public enum NodeOutcome
    {
        SUCCESS,
        RUNNING,
        FAIL
    }

    public List<Node> childrenNodes;

    public virtual NodeOutcome Execute(BehaviorTree bt)
    {
        return NodeOutcome.FAIL;
    }
}