using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetected : MonoBehaviour
{
    [SerializeField] ZombieAgent agent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && agent.currentState != ZombieAgent.ZombieState.ATTACKING)
        {
            agent.Alarm();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //agent.Disengage();
        }
    }
}
