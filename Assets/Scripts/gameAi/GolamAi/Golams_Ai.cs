using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public enum State
    {
        Idle,
        Patrol,
        Chase,
        Attack,
    }

public class Golams_Ai : MonoBehaviour
{
    public float speed;
    public GameObject[] wayPoints;
    public GameObject target;
    public Rigidbody rb;
    public GameObject player;
    public float detectionRange;
    public float chaseRange;
    public float attackRange;
    private State CurrentState; //Local variable that represents our state
    public Astar astar;
    public GridAStar grid;

    public void ChangeState(State newState)
    {
        CurrentState = newState;
    }

    private void Start()
    {
        CurrentState = State.Patrol;
    }


    void Update()
    {
        switch (CurrentState)
        {
            case State.Patrol:
                Patrol();
                if (Vector3.Distance(player.transform.position, transform.position) < detectionRange)
                {
                    ChangeState(State.Chase);
                }
                break;
            case State.Chase:
                Chasing();
                if (Vector3.Distance(player.transform.position, transform.position) > chaseRange)
                {
                    ChangeState(State.Patrol);
                }
                break;
        }

        Debug.Log(CurrentState);
    }

    void getTarget()
    {
        target = wayPoints[Random.Range(0, wayPoints.Length)];
        astar.TargetPosition = target.transform;
    }


    void Patrol()
    {
        if (!target)
        {
            getTarget();
        }

        rb.AddForce(grid.MovementCalc() * speed * Time.deltaTime, ForceMode.Impulse);

        if (distanceToTarget() <= 2)
        {
            getTarget();
        }

    }
    public float distanceToTarget()
    {
        float dist = Vector3.Distance(gameObject.transform.position, target.transform.position);
        return dist;
    }

    public void Chasing()
    {
        astar.TargetPosition = player.transform;

        rb.AddForce(grid.MovementCalc() * speed * Time.deltaTime, ForceMode.Impulse);
    }

    //private void OnDrawGizmos()
    //{
    //    if (target)
    //    {
    //        Gizmos.color = Color.blue;
    //        Gizmos.DrawSphere(target.transform.position, 1f);
    //    }

    //}

}
