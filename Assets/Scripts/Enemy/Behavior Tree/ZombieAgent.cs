using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAgent : BehaviorTree
{
    public LayerMask enemyLayers;

    public float wanderTimer;
    public float chaseCountdown;
    public float attackCountdown;
    public float attackRange;
    public float speed;

    public bool chasing;

    public float circleRadius = 1;
    public float turnChance = 0.05f;
    public float maxRadius = 5;

    public float mass = 15;
    public float maxSpeed = 3;
    public float maxForce = 15;

    public Vector3 velocity;
    public Vector3 wanderForce;
    public Vector3 targetPos;

    Vector2 currentPosition;
    Vector2 endPosition;

    // Start is called before the first frame update
    void Start()
    {
        rootNode = new SelectorNode();
        rootNode.childrenNodes.Add(new SequenceNode());
        rootNode.childrenNodes.Add(new WanderNode());
        rootNode.childrenNodes[0].childrenNodes.Add(new ChaseNode());
        rootNode.childrenNodes[0].childrenNodes.Add(new AttackNode());

        velocity = Random.onUnitSphere;
        wanderForce = GetRandomWanderForce();
    }

    public override void Update()
    {
        rootNode.Execute(this);
    }

    private Vector3 GetRandomWanderForce()
    {
        Vector3 circleCenter = velocity.normalized;
        Vector2 randomPoint = Random.insideUnitCircle;

        Vector3 displacement = new Vector3(randomPoint.x, randomPoint.y) * circleRadius;
        displacement = Quaternion.LookRotation(velocity) * displacement;

        Vector3 wanderForce = circleCenter + displacement;
        return wanderForce;
    }

    public Vector3 GetWanderForce()
    {
        if (transform.position.magnitude > maxRadius)
        {
            Vector3 directionToCenter = (targetPos - transform.position).normalized;
            wanderForce = velocity.normalized + directionToCenter;
        }
        else if (Random.value < turnChance)
        {
            wanderForce = GetRandomWanderForce();
        }

        return wanderForce;
    }
}