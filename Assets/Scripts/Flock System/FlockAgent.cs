using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockAgent : MonoBehaviour
{
    FlockManager flockcontroller;

    List<GameObject> flockAgents;
    public List<GameObject> neighborAgents;

    GameObject flockLeader;

    Vector3 flockCenter;
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

    public bool hasLeader;


    void Start()
    {
        framesPassed = 0;

        neighborAgents = new List<GameObject>();
        
        flockLeader = flockcontroller.leaderPrefab;
        speed = flockcontroller.speed;
        flockRadius = flockcontroller.spawnradius;

        velocity = new Vector3(Random.Range(0, 5), 0,Random.Range(0,5));
    }

    public void Initialize(List<GameObject> entities, 
                            FlockManager flockManager, /*Vector3 flockCenter, 
                            GameObject flockLeader, float speed, float flockRadius,*/
                            float cohesionWeight, float avoidanceWeight, 
                            float alignmentWeight, float followWeight, float maintainFlockWeight, bool hasLeader)
    {
        flockAgents = entities;
        flockcontroller = flockManager;
        //this.flockLeader = flockLeader;

        //this.speed = speed;

        this.alignmentWeight = alignmentWeight;
        this.cohesionWeight = cohesionWeight;
        this.avoidanceWeight = avoidanceWeight;
        this.followWeight = followWeight;
        this.maintainFlockWeight = maintainFlockWeight;

        //this.flockCenter = flockCenter;
        //this.flockRadius = flockRadius;

        this.hasLeader = hasLeader;
    }

    Vector3 Alignment()
    {
        // Play here
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

                    /*if (hasLeader)
                    {
                        float distFromLeader = Vector3.Distance(transform.position, flockLeader.transform.position);

                        if (distFromLeader < flockcontroller.avoidanceradius)
                            alignmentVelocity += flockLeader.transform.forward;
                    }*/
                }
            }

            if (flockAgents.Count > 0)
            {
                /*if (hasLeader)
                    alignmentVelocity /= flockAgents.Count + 1;

                else*/
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

                    /*if (hasLeader)
                    {
                        float distFromLeader = Vector3.Distance(transform.position, flockLeader.transform.position);

                        if (distFromLeader < flockcontroller.avoidanceradius)
                            alignmentVelocity += flockLeader.transform.forward;
                    }*/
                }
            }

            if (neighborAgents.Count > 0)
            {
                /*if (hasLeader)
                    alignmentVelocity /= neighborAgents.Count + 1;

                else*/
                    alignmentVelocity /= neighborAgents.Count;

                alignmentVelocity.Normalize();
            }
        }

        return alignmentVelocity;
    }

    Vector3 Cohesion()
    {
        // Play Here
        Vector3 avgAgentPos = Vector3.zero;


        if (flockAgents.Count > 10)
        {
            for (int i = 0; i < neighborAgents.Count; i++)
                avgAgentPos += neighborAgents[i].transform.position;

            avgAgentPos /= neighborAgents.Count;

            /*if (hasLeader)
                avgAgentPos += transform.position - flockLeader.transform.position;*/
        }

        else if(flockAgents.Count <= 10)
        {
            for(int i = 0; i < flockAgents.Count; i++)
                avgAgentPos += flockAgents[i].transform.position;

            avgAgentPos /= flockAgents.Count;

            /*if (hasLeader)
                avgAgentPos += avgAgentPos / flockAgents.Count + (transform.position - flockLeader.transform.position);*/
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

                    /*if (hasLeader)
                    {
                        float distFromLeader = Vector3.Distance(transform.position, flockLeader.transform.position);

                        if (distFromLeader < flockcontroller.avoidanceradius)
                            avgAvoidanceDir += transform.position - flockLeader.transform.position;
                    }*/
                }
            }

            if (flockAgents.Count > 0)
            {
                /*if (hasLeader)
                    avgAvoidanceDir /= flockAgents.Count + 1;

                else*/
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

                    /*if (hasLeader)
                    {
                        float distFromLeader = Vector3.Distance(transform.position, flockLeader.transform.position);

                        if (distFromLeader < flockcontroller.avoidanceradius)
                            avgAvoidanceDir += transform.position - flockLeader.transform.position;
                    }*/
                }
            }

            if (neighborAgents.Count > 0)
            {
                /*if (hasLeader)
                    avgAvoidanceDir /= neighborAgents.Count + 1;

                else*/
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
                if(flockAgents[i]!=gameObject)
                    neighborAgents.Add(flockAgents[i]);
        }
    }

    //Requires work
    Vector3 MaintainFlock()
    {
        Vector3 centerOffset = flockLeader.transform.position - transform.position;
        float t = centerOffset.magnitude / flockRadius;

        if (t < 0.9)
            return Vector3.zero;

        return centerOffset * t * t;
    }

    void FlockMovement()
    {
        //if (Random.Range(0, 5) <= 1)
        //{
            cohesion = Cohesion() * cohesionWeight;
            avoidance = Avoidance() * avoidanceWeight;
            alignment = Alignment() * alignmentWeight;

            stayInFlock = MaintainFlock() * maintainFlockWeight;

            /*if (hasLeader)
            {
                goalPos = SeekTarget(flockLeader.transform.position) * followWeight;
                velocity += (alignment + cohesion + avoidance /*+ stayInFlock + goalPos).normalized;
            }

            else*/
                velocity += (alignment + cohesion + avoidance + stayInFlock).normalized;

            velocity.Normalize();

        /*var agentRotation = Quaternion.LookRotation(velocity - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, agentRotation, speed * 0.5f * Time.deltaTime);*/


            transform.LookAt(velocity);
            transform.position += velocity * speed * Time.deltaTime;
        //}
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