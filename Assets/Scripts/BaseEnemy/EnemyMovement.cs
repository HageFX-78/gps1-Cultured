using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("List to store player to reference")]
    public List<GameObject> playerInRange = new List<GameObject>();

    public Transform target; //player to chase
    [Header("Enemy Characteristics")]
    [SerializeField] private float enemyDetectRange;
    [SerializeField] private float patrolMoveRange;
    [SerializeField] private float patrolTimer;
    [SerializeField] private float chaseSpeed;
    private float tempTimer;

    [Header("Ememy Booleans for reference")]
    [SerializeField] private bool chasePlayer;
    [SerializeField] private bool patrol;
    [SerializeField] private bool patrolOnCD;

    [Header("Animation")]
    public Animator animator;

    private NavMeshAgent agent;
    private Vector3 walkPoint;
    float RandomX;
    float RandomY;

    private void Awake()
    {
        gameObject.GetComponent<CircleCollider2D>().radius = enemyDetectRange;
    }

    private void Start()
    {        
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
        chasePlayer = false;
        patrol = true;
        patrolOnCD = false;
        tempTimer = patrolTimer;
    }

    void Update()
    {

        if (playerInRange.Count > 0 && playerInRange[0] != null && !patrol) //if player is in list and exist
        {
            ChasePlayer();
        }
        else
        {
            Patrolling();
        }

        //if enemy is not moving, set bool idle to true
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                animator.SetBool("Idle", true);
            }
        }
        else
            //set animator idle to false
            animator.SetBool("Idle", false);


        
    }


    private void Patrolling()
    {
        if(patrol)
        {
            if (!patrolOnCD) //if you are not on CD, set a walkpoint
            {

                RandomX = Random.Range(-patrolMoveRange, patrolMoveRange);
                RandomY = Random.Range(-patrolMoveRange, patrolMoveRange);

                walkPoint = new Vector3(transform.position.x + RandomX, transform.position.y + RandomY, 0.0f);
                patrolOnCD = true;

                //set the direction the enemy is facing
                if (transform.position.x - walkPoint.x > 0)
                {
                    animator.SetBool("Left", true);
                }
                else
                {
                    animator.SetBool("Left", false);
                }
            }

            if (patrolOnCD) //if your are on CD, countsdown till you can set a walk point
            {
                patrolTimer -= Time.deltaTime;

                if (patrolTimer <= 0.0f)
                {
                    patrolOnCD = false;
                    patrolTimer = tempTimer;
                }
            }

            agent.SetDestination(walkPoint); //sets the agent walkpoint
            //transform.position = Vector3.MoveTowards(transform.position, walkPoint, patrolSpeed * Time.deltaTime); 
        }
        else if(!patrol)
        {
            patrolOnCD = false;
            patrolTimer = tempTimer;
        }
    }

    private void ChasePlayer()
    {
        if(chasePlayer)
        {
            target = playerInRange[0].transform; //tracks players position

            agent.SetDestination(target.position); //sets the agent to track player(target)

            //for chasing
            if (target != null)
            {
                if (transform.position.x - target.position.x > 0)
                {
                    animator.SetBool("Left", true);
                }
                else
                {
                    animator.SetBool("Left", false);
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) //if player is in range, adds into list, and change to chase state
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            agent.speed = chaseSpeed;

            playerInRange.Add(collision.gameObject);
            patrol = false;
            chasePlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //if player is out of range, remove player from list, change to patrol state
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange.Remove(collision.gameObject);
            patrol = true;
            chasePlayer = false;
        }
    }

}
