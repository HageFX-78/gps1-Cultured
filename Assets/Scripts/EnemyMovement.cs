using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public List<GameObject> playerInRange = new List<GameObject>();

    [SerializeField] private float enemyRange;
    public Transform target; //player
    public float speed;

    private void Awake()
    {
        gameObject.GetComponent<CircleCollider2D>().radius = enemyRange;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange.Count > 0 && playerInRange[0] != null)
        {
            target = playerInRange[0].transform;

            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerInRange.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange.Remove(collision.gameObject);
        }
    }
}
