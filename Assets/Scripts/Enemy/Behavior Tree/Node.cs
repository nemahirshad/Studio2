using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public enum NodeOutcome
    {
        SUCCESS,
        FAIL,
        RUNNING
    }

    public List<Node> childrenNodes;

    public Node()
    {
        childrenNodes = new List<Node>();
    }

    public virtual NodeOutcome Execute(BehaviorTree bt)
    {
        return NodeOutcome.FAIL;
    }
}