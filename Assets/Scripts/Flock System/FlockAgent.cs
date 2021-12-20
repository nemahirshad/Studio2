using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockAgent : MonoBehaviour
{
    FlockManager flockcontroller;

    List<GameObject> flockAgents;
    public List<GameObject> neighborAgents;

    Transform flockParent;
    float flockRadius;

    Vector3 velocity;

    Vector3 alignment;
    Vector3 cohesion;
    Vector3 avoidance;
    Vector3 stayInFlock;

    Vector3 goalPos;

    Vector3 avgGizmoPos;

    Vector3 currentCohesionVel;

    float speed;
    int framesPassed;

    public float cohesionWeight;
    public float avoidanceWeight;
    public float alignmentWeight;
    public float maintainFlockWeight;

    float followWeight;

    public float agentSmoothTime = 0.5f;

    public bool lockYAxis;


    void Start()
    {
        framesPassed = 0;

        neighborAgents = new List<GameObject>();

        flockParent = flockcontroller.transform;
        speed = flockcontroller.speed;
        flockRadius = flockcontroller.spawnradius;

        //velocity = new Vector3(Random.Range(0, 5), 0,Random.Range(0,5));

        velocity.x = Random.Range(0, 5);
        velocity.z = Random.Range(0, 5);

        if (lockYAxis)
            velocity.y = flockParent.position.y;

        else
            velocity.y = Random.Range(0, 5);

    }

    public void Initialize(List<GameObject> entities, FlockManager flockManager,
                            float cohesionWeight, float avoidanceWeight,
                            float alignmentWeight, float maintainFlockWeight, bool lockYAxis)
    {
        flockAgents = entities;
        flockcontroller = flockManager;

        this.alignmentWeight = alignmentWeight;
        this.cohesionWeight = cohesionWeight;
        this.avoidanceWeight = avoidanceWeight;
        this.maintainFlockWeight = maintainFlockWeight;

        this.lockYAxis = lockYAxis;
    }

    Vector3 Alignment()
    {
        Vector3 alignmentVelocity = Vector3.zero;

        if (flockAgents.Count <= 10)
        {
            for (int i = 0; i < flockAgents.Count; i++)
            {
                if (flockAgents[i] != gameObject)
                {
                    float distance = Vector3.Distance(transform.position, flockAgents[i].transform.position);

                    if (distance < 4)
                        alignmentVelocity += flockAgents[i].GetComponent<FlockAgent>().velocity;
                }
            }

            if (flockAgents.Count > 0)
            {
                alignmentVelocity /= flockAgents.Count;
                alignmentVelocity.Normalize();
            }
        }

        else if (flockAgents.Count > 10)
        {
            for (int i = 0; i < neighborAgents.Count; i++)
            {
                if (neighborAgents[i] != gameObject)
                {
                    float distance = Vector3.Distance(transform.position, neighborAgents[i].transform.position);

                    if (distance < 4)
                        alignmentVelocity += neighborAgents[i].GetComponent<FlockAgent>().velocity;
                }
            }

            if (neighborAgents.Count > 0)
            {
                alignmentVelocity /= neighborAgents.Count;
                alignmentVelocity.Normalize();
            }
        }

        return alignmentVelocity;
    }

    Vector3 Cohesion()
    {
        Vector3 avgAgentPos = Vector3.zero;

        if (flockAgents.Count > 10)
        {
            for (int i = 0; i < neighborAgents.Count; i++)
                avgAgentPos += neighborAgents[i].transform.position;

            avgAgentPos /= neighborAgents.Count;
        }

        else if (flockAgents.Count <= 10)
        {
            for (int i = 0; i < flockAgents.Count; i++)
                avgAgentPos += flockAgents[i].transform.position;

            avgAgentPos /= flockAgents.Count;
        }

        //For Testing Only
        avgGizmoPos = avgAgentPos;

        Vector3 cohesionDir = avgAgentPos - transform.position;
        cohesionDir = Vector3.SmoothDamp(transform.position, cohesionDir, ref currentCohesionVel, agentSmoothTime);
        cohesionDir.Normalize();

        return cohesionDir;
    }

    Vector3 Avoidance()
    {
        Vector3 avgAvoidanceDir = Vector3.zero;

        if (flockAgents.Count <= 10)
        {
            for (int i = 0; i < flockAgents.Count; i++)
            {
                if (flockAgents[i] != gameObject)
                {
                    float distance = Vector3.Distance(transform.position, flockAgents[i].transform.position);

                    if (distance < flockcontroller.avoidanceradius)
                        avgAvoidanceDir += transform.position - flockAgents[i].transform.position;
                }
            }

            if (flockAgents.Count > 0)
            {
                avgAvoidanceDir /= flockAgents.Count;
                avgAvoidanceDir.Normalize();
            }
        }

        else if (flockAgents.Count > 10)
        {
            for (int i = 0; i < neighborAgents.Count; i++)
            {
                if (neighborAgents[i] != gameObject)
                {
                    float distance = Vector3.Distance(transform.position, neighborAgents[i].transform.position);

                    if (distance < flockcontroller.avoidanceradius)
                        avgAvoidanceDir += transform.position - neighborAgents[i].transform.position;
                }
            }

            if (neighborAgents.Count > 0)
            {
                avgAvoidanceDir /= neighborAgents.Count;
                avgAvoidanceDir.Normalize();
            }
        }

        return avgAvoidanceDir;
    }

    Vector3 SeekTarget(Vector3 target)
    {
        return target - transform.position;
    }

    void FindNeighborAgents()
    {
        neighborAgents.Clear();

        for (int i = 0; i < flockAgents.Count; i++)
        {
            if (Vector3.Distance(flockAgents[i].transform.position, transform.position) < flockcontroller.neighborRadius)
                if (flockAgents[i] != gameObject)
                    neighborAgents.Add(flockAgents[i]);
        }
    }

    //Requires work
    Vector3 MaintainFlock()
    {
        Vector3 centerOffset = flockParent.position - transform.position;
        float t = centerOffset.magnitude / flockRadius;

        if (t < 0.9)
            return Vector3.zero;

        return centerOffset * t * t;
    }

    void FlockMovement()
    {
        cohesion = Cohesion() * cohesionWeight;
        avoidance = Avoidance() * avoidanceWeight;
        alignment = Alignment() * alignmentWeight;

        stayInFlock = MaintainFlock() * maintainFlockWeight;

        velocity += (alignment + cohesion + avoidance + stayInFlock).normalized;
        velocity.Normalize();

        //transform.LookAt(velocity);
        transform.position += velocity * speed * Time.deltaTime;
    }

    void Update()
    {
        if (flockAgents.Count > 10)
        {
            framesPassed++;

            if (framesPassed > 10)
            {
                FindNeighborAgents();
                framesPassed = 0;
            }
        }

        FlockMovement();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        //Gizmos.DrawRay(transform.position, velocity);


        Gizmos.color = Color.red;
        //Gizmos.DrawRay(transform.position, avoidance);
        //Gizmos.DrawWireSphere(transform.position, flockcontroller.avoidanceradius);

        Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(transform.position, flockcontroller.neighborRadius);
        Gizmos.DrawSphere(transform.position, 2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, alignment);


        //Gizmos.color = Color.green;
        //Gizmos.DrawRay(transform.position, goalPos);

        //Gizmos.DrawCube(avgGizmoPos, Vector3.one / 2);
    }
}