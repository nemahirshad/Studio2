using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAgent : BehaviorTree
{
    public Transform point;

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

        rb = GetComponent<Rigidbody>();
	}

    public override void Update()
    {
        rootNode.Execute(this);
		wanderCountdown -= Time.deltaTime;

        astar.FindPath(transform.position, point.transform.position, gameObject);
        rb.AddForce(grid.MovementCalculator(gameObject) * speed * Time.deltaTime, ForceMode.Impulse);
        Debug.Log(grid.MovementCalculator(gameObject));
    }
}