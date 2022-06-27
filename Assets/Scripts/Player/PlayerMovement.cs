using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Boundaries")]
    Vector2 boundary;
    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;
    [SerializeField] private float topLimit;
    [SerializeField] private float btmLimit;

    [Header("Transition References")]
    public static Vector2 transitionPos;
    public static int transitionCount = 0;
    [SerializeField] private float timerToTransition;


    [Header("Player Characteristics")]
    public float moveSpeed = 3f;
    public float sprintSpeed = 6f;
    private float speed;

    [Header("Sprint Related")]
    public float sprintGauge = 100;
    private float sprintMax;
    public float sprintLossRate = 20;
    public float sprintGainRate = 10;
    public bool fatigue = false;

    public Rigidbody2D rb;
    
    Vector2 movement;

    private void Start()
    {
        //Checks if player has transitioned to battle scene more than once
        if(transitionCount > 0)
        {
            transform.position = transitionPos;
        }

        sprintMax = sprintGauge;
    }
    void Update()
    {
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>PLAYER MOVEMENT<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movement = new Vector2(x,y).normalized;

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>BOUNDARY DETECTION<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        /*boundary.x = Mathf.Clamp(transform.position.x + 1, leftLimit, rightLimit);
        boundary.y = Mathf.Clamp(transform.position.y + 1, topLimit, btmLimit);
            
        transform.position = boundary;

        Debug.Log(movement);*/

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>SPRINTING<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        if(Input.GetKey(KeyCode.LeftShift) && !fatigue)
        {
            if (x != 0 || y != 0)
            {
                if (sprintGauge > 0)
                {
                    speed = sprintSpeed;
                    sprintGauge -= sprintLossRate * Time.deltaTime;
                }
                else
                {
                    speed = moveSpeed;
                    fatigue = true;
                }
            }
        }
        else
        {
            speed = moveSpeed;
            if (sprintGauge < sprintMax)
            {
                sprintGauge += sprintGainRate * Time.deltaTime;
            }
        }

        if (sprintGauge > 50)
        {
            fatigue = false;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //on collision with enemy, set the transitionPos to the collide point, destroy the enemy and load next scene
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy");
            transitionPos = collision.transform.position;

            //currently after collision, set enemy to inactive, add into removed enemy and enemy list
            collision.gameObject.SetActive(false);
            EnemyManager.EnemyList.Remove(collision.gameObject.name);
            EnemyManager.RemovedEnemy.Add(collision.gameObject.name);
            Debug.Log(collision.gameObject.name);

            StartCoroutine(LoadingBattle());            
        }
    }

    IEnumerator LoadingBattle()
    {
        Time.timeScale = 0;
        //add to the transition count
        transitionCount++;

        yield return new WaitForSecondsRealtime(timerToTransition);
        //load scene after timer goes;
        SceneManager.LoadSceneAsync("Battle Scene");

    }
}
