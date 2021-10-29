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
<<<<<<< HEAD:Assets/Scripts/gameAi/GolamAi/Golams_Ai.cs
    public GridAStar grid;
=======
    public Grid grid;
    AIPathHolder aiPath;

>>>>>>> 7d48e6f7538afef6f46ca19b2d41d13f6ba80f15:Assets/gameAi/GolamAi/Golams_Ai.cs

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
<<<<<<< HEAD:Assets/Scripts/gameAi/GolamAi/Golams_Ai.cs
=======

>>>>>>> 7d48e6f7538afef6f46ca19b2d41d13f6ba80f15:Assets/gameAi/GolamAi/Golams_Ai.cs
    }

    void getTarget()
    {
<<<<<<< HEAD:Assets/Scripts/gameAi/GolamAi/Golams_Ai.cs
        target = wayPoints[Random.Range(0, wayPoints.Length)];
        astar.TargetPosition = target.transform;
=======
        target = wayPoints[Random.Range(0,wayPoints.Length)];
>>>>>>> 7d48e6f7538afef6f46ca19b2d41d13f6ba80f15:Assets/gameAi/GolamAi/Golams_Ai.cs
    }


    void Patrol()
    {
        if (!target)
        {
            getTarget();
        }
        astar.FindPath(transform.position, target.transform.position, gameObject);

        rb.AddForce(grid.MovementCalculator(gameObject) * speed * Time.deltaTime, ForceMode.Impulse);

        if (distanceToTarget() <= 3)
        {
            getTarget();
        }
    }
    public float distanceToTarget()
    {
        float dist = Vector3.Distance(gameObject.transform.position, target.transform.position);
        return dist;
    }

<<<<<<< HEAD:Assets/Scripts/gameAi/GolamAi/Golams_Ai.cs
    public void Chasing()
    {
        astar.TargetPosition = player.transform;

        rb.AddForce(grid.MovementCalc() * speed * Time.deltaTime, ForceMode.Impulse);
    }
=======
        public void Chasing()
        {
        //astar.TargetPosition = player.transform;
            astar.FindPath(transform.position, player.transform.position, gameObject);
            rb.AddForce(grid.MovementCalculator(gameObject) * speed * Time.deltaTime, ForceMode.Impulse);
            Debug.Log(grid.MovementCalculator(gameObject));
        }
>>>>>>> 7d48e6f7538afef6f46ca19b2d41d13f6ba80f15:Assets/gameAi/GolamAi/Golams_Ai.cs

    //private void OnDrawGizmos()
    //{
    //    if (target)
    //    {
    //        Gizmos.color = Color.blue;
    //        Gizmos.DrawSphere(target.transform.position, 1f);
    //    }

    //}

}
