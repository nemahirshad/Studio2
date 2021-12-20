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

    //----------Public Variables----------

    [Range(1f, 10f)]
    public float spawnradius = 5;

    [Range(0f, 10f)]
    public float neighborRadius = 1.5f;

    [Range(0f, 10f)]
    public float avoidanceradius = 0.5f;

    [Range(1f, 100f)]
    public float speed = 5;

    public float cohesionWeight;
    public float avoidanceWeight;
    public float alignmentWeight;
    public float maintainFlockWeight;

    public bool lockYAxis;

    void Start()
    {
        flockAgents = new List<GameObject>();

        SpawnFlock();
    }

    public void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnFlock();
            Debug.Log("Flock Spawned");
        }*/
    }
    public void SpawnFlock()
    {
        for (int i = 0; i < initialAgentCount; i++)
        {
            Vector2 randomIntCircle = Random.insideUnitCircle;
            flockAgents.Add(Instantiate(agentPrefab,
                            new Vector3(transform.position.x + randomIntCircle.x * spawnradius, gameObject.transform.position.y, transform.position.z + randomIntCircle.y * spawnradius),
                            Quaternion.identity,
                            transform
                            ));

            flockAgents[i].name = agentPrefab.name + " " + i;
            flockAgents[i].GetComponent<FlockAgent>().Initialize(flockAgents, this, cohesionWeight,
                                                                avoidanceWeight, alignmentWeight, maintainFlockWeight, lockYAxis);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 1, 0.25f);
        Gizmos.DrawSphere(transform.position, spawnradius);
    }
}