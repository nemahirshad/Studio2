using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAgent : BehaviorTree
{
    public Transform point;

    public StarPathfinding astar;

    public StarGrid grid;

    public LayerMask enemyLayers;

    public float wanderTimer;
    public float wanderCountdown;
    public float chaseCountdown;
    public float attackCountdown;
    public float attackRange;
    public float speed;
    public float wanderForce;

    public bool canFollow;

	public int currentIndex;

    public Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        rootNode = new SelectorNode();
        rootNode.childrenNodes.Add(new SequenceNode());
        rootNode.childrenNodes.Add(new WanderNode());
        rootNode.childrenNodes[0].childrenNodes.Add(new ChaseNode());
        rootNode.childrenNodes[0].childrenNodes.Add(new AttackNode());

		point.position = transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * wanderForce;
	}

    public override void Update()
    {
        rootNode.Execute(this);
        astar.FindPath(transform.position, point.position);

		wanderCountdown -= Time.deltaTime;
    }

    public void FollowPath()
    {
        if (astar.grid.path != null && canFollow)
        {
            Vector3 waypoint = astar.grid.path[currentIndex].worldPosition;

            transform.position = Vector3.MoveTowards(transform.position, waypoint, speed * Time.deltaTime);

            Vector3 lookDir = new Vector3(waypoint.x, 0, waypoint.z);

            transform.rotation = Quaternion.LookRotation(lookDir);

            if (Vector3.Distance(waypoint, transform.position) < range)
            {
                currentIndex++;
            }

            if (currentIndex >= astar.grid.path.Count)
            {
                canFollow = false;
            }
        }
    }
}