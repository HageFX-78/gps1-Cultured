using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public List<GameObject> playerInRange = new List<GameObject>();

    public Transform target; //player
    [SerializeField] private float enemyRange;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float patrolSpeed;

    [SerializeField] private float patrolRange;
    [SerializeField] private float patrolTimer;
    private float tempTimer;

    [SerializeField] private bool chasePlayer;
    [SerializeField] private bool patrol;
    [SerializeField] private bool patrolOnCD;

    [SerializeField] private Vector3 walkPoint;

    private void Awake()
    {
        gameObject.GetComponent<CircleCollider2D>().radius = enemyRange;
    }

    private void Start()
    {
        chasePlayer = false;
        patrol = true;
        patrolOnCD = false;
        tempTimer = patrolTimer;
    }

    void Update()
    {
        if (playerInRange.Count > 0 && playerInRange[0] != null) //if player is in list and exist
        {
            ChasePlayer();
        }
        else
        {
            Patrolling();
        }
    }


    private void Patrolling()
    {
        if(patrol)
        {
            if (!patrolOnCD) //if you are not on CD, set a walkpoint
            {
                float RandomX = Random.Range(-patrolRange, patrolRange);
                float RandomY = Random.Range(-patrolRange, patrolRange);

                walkPoint = new Vector3(transform.position.x + RandomX, transform.position.y + RandomY, 0.0f);
                patrolOnCD = true;
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

            transform.position = Vector3.MoveTowards(transform.position, walkPoint, patrolSpeed * Time.deltaTime); //updates position
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

            transform.position = Vector3.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) //if player is in range, adds into list, and change to chase state
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerInRange.Add(collision.gameObject);
            patrol = false;
            chasePlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //if player is out of range, remove player from lsit, change to patrol state
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange.Remove(collision.gameObject);
            patrol = true;
            chasePlayer = false;
        }
    }


}
