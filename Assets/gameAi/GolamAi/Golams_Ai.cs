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
    public float attackRange;
    private State CurrentState; //Local variable that represents our state

    public void ChangeState(State newState)
    {
        CurrentState = newState;
    }



  
   void Start()
    {
        CurrentState = State.Idle;
       

            switch (CurrentState)
            {
               
               
            }
           
        
    }


 void Patrol()
    {
        if (!target)
        {
           // getTarget();

        }
        // calculate direction towards the target
        Vector3 dir = target.transform.position - gameObject.transform.position;
        // we are normalizing the direction vector, in order to get a unit vector out of the direction where the magnitude is always = 1
        rb.AddForce(dir.normalized * speed * Time.deltaTime, ForceMode.Impulse);

        if (distanceToTarget() <= 1.5f)
        {
            //getTarget();
        }

    }
        public float distanceToTarget()
        {
            float dist = Vector3.Distance(gameObject.transform.position, target.transform.position);
            return dist;
        }

        public void Chasing()
        {

            Vector3 dir = player.transform.position - gameObject.transform.position;
            // we are normalizing the direction vector, in order to get a unit vector out of the direction where the magnitude is always = 1
            rb.AddForce(dir.normalized * speed * Time.deltaTime, ForceMode.Impulse);
        }

   
  
}
