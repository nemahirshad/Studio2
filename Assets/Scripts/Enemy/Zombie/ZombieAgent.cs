using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAgent : MonoBehaviour
{
    public enum ZombieState
    {
        IDLE,
        CHASING,
        ATTACKING
    }

    public ZombieState currentState;

    public List<ZombieAgent> neighbors;

    [SerializeField] Animator anim;

    [SerializeField] float attackRange, chaseRange, attackCooldown, alarmCooldown, speed, rotSpeed;
    
    [SerializeField] int damage;

    Transform player;

    EnemyInfo info;

    float attackTimer, alarmTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<HealthSystem>().transform;
        info = GetComponent<EnemyInfo>();

        attackTimer = attackCooldown;
        alarmTimer = alarmCooldown;
	}

    void Update()
    {
        switch (currentState)
        {
            case ZombieState.IDLE:
                anim.SetBool("Chasing", false);

                alarmTimer -= Time.deltaTime;
                break;

            case ZombieState.CHASING:
                anim.SetBool("Chasing", true);
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed);

                Vector3 targetDirection = player.position - transform.position;
                
                float singleStep = rotSpeed * Time.deltaTime;
                
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
                
                Debug.DrawRay(transform.position, newDirection, Color.red);
                transform.rotation = Quaternion.LookRotation(newDirection);

                if (Vector3.Distance(player.position, transform.position) > chaseRange)
                {
                    Disengage();
                }
                
                if (Vector3.Distance(player.position, transform.position) < chaseRange)
                {
                    currentState = ZombieState.ATTACKING;
                }
                break;

            case ZombieState.ATTACKING:
                anim.SetBool("Chasing", false);

                if (Vector3.Distance(player.position, transform.position) > attackRange)
                {
                    currentState = ZombieState.CHASING;
                }

                attackTimer -= Time.deltaTime;
                if (attackTimer <= 0)
                {
                    player.GetComponent<HealthSystem>().LoseHP(damage);
                    anim.SetTrigger("Attack");
                    attackTimer = attackCooldown;
                }
                break;
        }
    }

    public void Alarm()
    {
        for (int i = 0; i < neighbors.Count; i++)
        {
            neighbors[i].currentState = ZombieState.CHASING;
        }

        alarmTimer = alarmCooldown;
    }

    public bool CanBeAlarmed()
    {
        if (alarmTimer <= 0)
        {
            return true;
        }

        return false;
    }

     void Disengage()
    {
        int count = 0;

        for (int i = 0; i < neighbors.Count; i++)
        {
            if (Vector3.Distance(player.position, neighbors[i].transform.position) > chaseRange)
            {
                count++;
            }
        }

        if (count > (neighbors.Count) / 2)
        {
            for (int i = 0; i < neighbors.Count; i++)
            {
                neighbors[i].currentState = ZombieState.IDLE;
            }
        }

        count = 0;
    }
}