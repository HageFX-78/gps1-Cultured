using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
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
        sprintMax = sprintGauge;
    }
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        movement = new Vector2(x, y).normalized;

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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("Hit Enemy");
            SceneManager.LoadSceneAsync("Battle Scene");
        }
    }


}
