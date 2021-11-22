using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockScript : MonoBehaviour
{
    public FlockingAgent prefabAgent;
    public FlockBehavior flockBehavior;

    List<FlockingAgent> agents = new List<FlockingAgent>();

    [Range(4, 100)]
    public int initialAgentCount = 50;
    const float agentDensity = 0.5f;

    [Range(1f, 100f)]
    public float movementMultiplier = 10f;

    [Range(1f, 100f)]
    public float maxSpeed = 5;

    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;

    [Range(0f, 1f)]
    public float avoidanceMultiplier = 0.5f;

    public float squareMaxSpeed;
    public float squareNeighborRadius;
    public float squareAvoidanceRadius;

    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceMultiplier * avoidanceMultiplier;

        for (int i = 0; i < initialAgentCount; i++)
        {
            FlockingAgent newAgent = Instantiate(
                prefabAgent,
                Random.insideUnitCircle * initialAgentCount * agentDensity, 
                Quaternion.Euler(Vector3.forward * Random.Range(0, 360)), 
                transform);

            newAgent.name = "Agent " + i;
            agents.Add(newAgent);
        }
    }

    void Update()
    {
        foreach (FlockingAgent agent in agents)
        {
            List<Transform> agentContext = GetNearbyObjects(agent);

            Vector2 agentMove = flockBehavior.CalculateMovement(agent, agentContext, this);
            agentMove *= movementMultiplier;

            if (agentMove.sqrMagnitude > squareMaxSpeed)
            {
                agentMove = agentMove.normalized * maxSpeed;
            }
            agent.MoveAgent(agentMove);
        }
    }

    public List<Transform> GetNearbyObjects(FlockingAgent agent)
    {
        List<Transform> objContext = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);

        foreach (Collider objCol in contextColliders)
        {
            if (objCol != agent.agentCollider)
            {
                objContext.Add(objCol.transform);
            }
        }

        return objContext;
    }
}