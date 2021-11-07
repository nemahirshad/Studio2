using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Brendan;

public class BehaviorTree : MonoBehaviour
{
    protected Node rootNode;

    public Animator anim;

    public Rigidbody rb;

    public GameObject target;
    public GameObject player;

    public Brendan.Grid grid;

    public Astar astar;

    public AIPathHolder path;

    public float range;

    // Update is called once per frame
    public virtual void Update()
    {
        rootNode.Execute(this);
    }
}