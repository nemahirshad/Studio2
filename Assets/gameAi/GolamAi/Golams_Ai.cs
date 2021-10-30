using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public Grid grid;
        AIPathHolder aiPath;


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

        public void Chasing()
        {
            //astar.TargetPosition = player.transform;
            astar.FindPath(transform.position, player.transform.position, gameObject);
            rb.AddForce(grid.MovementCalculator(gameObject) * speed * Time.deltaTime, ForceMode.Impulse);
            Debug.Log(grid.MovementCalculator(gameObject));
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
}
