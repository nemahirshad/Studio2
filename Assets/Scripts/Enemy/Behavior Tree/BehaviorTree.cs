using Brendan;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree : MonoBehaviour
{
    protected Node rootNode;

    protected Rigidbody rb;

    public Astar astar;

    public Brendan.Grid grid;

    public Animator anim;

    public GameObject target;
    public GameObject player;

    public float range;

    // Update is called once per frame
    public virtual void Update()
    {
        rootNode.Execute(this);
    }
}