using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree : MonoBehaviour
{
    protected Node rootNode;

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