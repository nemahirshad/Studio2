using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    //----------Private Variables----------

    [SerializeField] GameObject agentPrefab;

    public List<GameObject> flockAgents;

    [SerializeField, Range(4, 50)]
    int initialAgentCount = 10;

    

    //[SerializeField, Range(0f, 1f)]
    //float movementMultiplier = 0.5f;

    bool needsLeader = false;

    //----------Public Variables----------

    [Range(1f, 10f)]
    public float spawnradius = 5;

    [Range(0f, 10f)]
    public float neighborRadius = 1.5f;

    [Range(0f, 10f)]
    public float avoidanceradius = 0.5f;

    [Range(1f, 100f)]
    public float speed = 5;

    public GameObject leaderPrefab;
    
    public float cohesionWeight;
    public float avoidanceWeight;
    public float alignmentWeight;
    public float maintainFlockWeight;

    float followWeight;

    public bool lockYAxis;

    void Start()
    {
        flockAgents = new List<GameObject>();

        //SpawnFlock();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnFlock();
            Debug.Log("Flock Spawned");
        }
    }
    public void SpawnFlock()
    {
        /*if (lockYAxis)
        {
            if (needsLeader)
            {
                GameObject leader = Instantiate(leaderPrefab, new Vector3(transform.position.x, 0, transform.position.z + spawnradius), Quaternion.identity, transform);
                leader.name = gameObject.name + " Leader";


            }


            Vector2 randomIntCircle = Random.insideUnitCircle;

            for (int i = 0; i < initialAgentCount; i++)
            {
                flockAgents.Add(Instantiate(agentPrefab,
                                //new Vector3(randomIntCircle.x * spawnradius, 0, randomIntCircle.y * spawnradius),
                                new Vector3(Random.insideUnitSphere.x * spawnradius, 0, Random.insideUnitSphere.z * spawnradius),
                                Quaternion.identity,
                                transform
                                ));

                flockAgents[i].name = agentPrefab.name + " " + i;
                flockAgents[i].GetComponent<FlockAgent>().Initialize(flockAgents, this, /*transform.position,
                                                                     leaderPrefab, speed, spawnradius,
                                                                     cohesionWeight, avoidanceWeight,
                                                                     alignmentWeight, followWeight,
                                                                     maintainFlockWeight, true);
            }
        }*/

        if (!needsLeader)
        {
            Vector2 randomIntCircle = Random.insideUnitCircle;

            for (int i = 0; i < initialAgentCount; i++)
            {
                flockAgents.Add(Instantiate(agentPrefab,
                                new Vector3(transform.position.x + randomIntCircle.x * spawnradius, 0, transform.position.z + randomIntCircle.y * spawnradius),
                                Quaternion.identity,
                                transform
                                ));

                flockAgents[i].name = agentPrefab.name + " " + i;
                flockAgents[i].GetComponent<FlockAgent>().Initialize(flockAgents, this, /*transform.position,
                                                                     leaderPrefab, speed, spawnradius,*/
                                                                     cohesionWeight, avoidanceWeight,
                                                                     alignmentWeight, followWeight,
                                                                     maintainFlockWeight, false);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 1, 0.25f);
        Gizmos.DrawSphere(transform.position, spawnradius);
    }
}