using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum aiStates
{
    
    patrolling,
    Flee,
   
}

public class BatAi : MonoBehaviour
{
    private aiStates currentState;
    public float speed;
    public GameObject[] wayPoints;
    public GameObject target;
    public Rigidbody rb;
    public GameObject player;
    public float fleeRange; 
   
    // Start is called before the first frame update
    void Start()
    {
        ChangeState(aiStates.patrolling);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {

            case aiStates.patrolling:
                Patrolling();
               
                Debug.Log("patrol");
                break;
            case aiStates.Flee:
                Debug.Log("Flee");
                Flee();
                break;
               


        }
    }

    public void ChangeState(aiStates newState)
    {
        currentState = newState;
    }

    private void Patrolling()
    {
        if (!target)
        {
            getTarget();

        }
        // calculate direction towards the target
        Vector3 dir = target.transform.position - gameObject.transform.position;
        // we are normalizing the direction vector, in order to get a unit vector out of the direction where the magnitude is always = 1
        rb.AddForce(dir.normalized * speed * Time.deltaTime, ForceMode.Impulse);

        if (distanceToTarget() <= 1.5f)
        {
            getTarget();
        }

        if(Vector3.Distance(gameObject.transform.position,player.transform.position) < fleeRange)
        {
            ChangeState(aiStates.Flee);
        }

    }

    public float distanceToTarget()
    {
        float dist = Vector3.Distance(gameObject.transform.position, target.transform.position);
        return dist;
    }

    // sets the target to be a random waypoint from the array of waypoints
    public void getTarget()
    {
        target = wayPoints[Random.Range(0, wayPoints.Length)];
    }

    public void Flee()
    {

        Vector3 dir = gameObject.transform.position - player.transform.position;
        // we are normalizing the direction vector, in order to get a unit vector out of the direction where the magnitude is always = 1
        rb.AddForce(dir.normalized * speed * Time.deltaTime, ForceMode.Impulse);

        if (Vector3.Distance(gameObject.transform.position, player.transform.position) > fleeRange)
        {
            ChangeState(aiStates.patrolling);
        }
    }


}
