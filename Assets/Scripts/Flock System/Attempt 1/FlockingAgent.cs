using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockingAgent : MonoBehaviour
{
    public Collider agentCollider;

    void Start()
    {
        agentCollider = GetComponent<Collider>();
    }

    public void MoveAgent(Vector2 agentVelocity)
    {
        transform.forward = agentVelocity;
        transform.position += new Vector3(agentVelocity.x * Time.deltaTime, 0, agentVelocity.y * Time.deltaTime);
    }
}