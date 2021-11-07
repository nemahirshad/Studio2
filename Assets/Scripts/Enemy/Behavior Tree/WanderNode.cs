using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderNode : Node
{
    public override NodeOutcome Execute(BehaviorTree bt)
    {
        if (((ZombieAgent)bt).wanderCountdown <= 0)
        {
            ((ZombieAgent)bt).point.position = new Vector3(Random.Range(-1f, 1f), ((ZombieAgent)bt).point.position.y, Random.Range(-1f, 1f)) * ((ZombieAgent)bt).wanderForce;
            ((ZombieAgent)bt).wanderCountdown = ((ZombieAgent)bt).wanderTimer;
            ((ZombieAgent)bt).canFollow = true;
            bt.astar.FindPath(((ZombieAgent)bt).transform.position, ((ZombieAgent)bt).point.position, ((ZombieAgent)bt).gameObject);
        }

        bt.rb.AddForce(bt.grid.MovementCalculator(((ZombieAgent)bt).gameObject) * ((ZombieAgent)bt).speed * Time.deltaTime, ForceMode.Impulse);
        Debug.Log(bt.grid.MovementCalculator(((ZombieAgent)bt).gameObject));

        bt.anim.SetBool("Wandering", true);
        
        return NodeOutcome.SUCCESS;
    }
}
