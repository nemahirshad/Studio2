using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Brendan;

namespace Brendan
{
    public enum State
    {
        Idle,
        Patrol,
        Chase,
        Attack,
    }

    public class Golams_Ai : MonoBehaviour
    {
        public GameObject attackCol;
        public Animator anim;
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
        public GridManager grid;
        AIPathHolder aiPath;
        WaypointAI waypointAI;

        public void ChangeState(State newState)
        {
            CurrentState = newState;
        }

        private void Start()
        {
            aiPath = GetComponent<AIPathHolder>();
            waypointAI = GetComponent<WaypointAI>();
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
                    if (Vector3.Distance(player.transform.position, transform.position) < attackRange)
                    {
                        ChangeState(State.Attack);
                    }
                    break;

                case State.Attack:
                    Attacking();
                    if (Vector3.Distance(player.transform.position, transform.position) > attackRange)
                    {
                        ChangeState(State.Chase);
                        attackCol.SetActive(false);
                    }
                    break;
            }

            Debug.Log(CurrentState);
        }

        void getTarget()
        {
            target = wayPoints[Random.Range(0, wayPoints.Length)];

            while (true)
            {
                if (Vector3.Distance(transform.position, target.transform.position) < 3)
                    target = wayPoints[Random.Range(0, wayPoints.Length)];
                else
                    break;
            }
        }
   
        void Patrol()
        {
            if (target != null)
            {
                if (waypointAI.TargetReached)
                {
                    target = null;
                }

            }

            if (target == null)
            {
                getTarget();
                astar.FindPath(transform.position, target.transform.position, gameObject);
                waypointAI.MoveTowards(aiPath.shortestPath, rb, speed, 2.2f);
            }

        }

        public void Chasing()
        {
            
            astar.FindPath(transform.position, player.transform.position, gameObject);
            waypointAI.MoveTowards(aiPath.shortestPath, rb, speed, 2.2f);

        }

        public void Attacking()
        {
            attackCol.SetActive(true);
            //  anim.SetBool("IsAttacking", true);
        }
    }
}