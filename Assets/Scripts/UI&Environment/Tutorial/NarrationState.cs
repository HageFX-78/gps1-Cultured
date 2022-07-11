using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrationState : TutorialBaseState
{
    TutorialStateManager tutorial;
    private float timer = 1f;
    private bool begin = false;
    private float alphaV = 1f;
    private bool showTime = false;

    [SerializeField] DialogueManager manager;
 

    public override void EnterState(TutorialStateManager tutorial)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
    }
    public override void UpdateState(TutorialStateManager tutorial)
    {
        if (timer > 0f && !begin)
        {
            timer -= Time.deltaTime;
        }
        else if (!begin)
        {
            tutorial.startConvo(tutorial.convoFile);
            begin = true;
        }
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)) && begin  && tutorial.end)
        {
            showTime = true;
        }
        if (showTime)
        {
            if (alphaV > 0f)
            {
                alphaV -= 0.51f * Time.deltaTime;
                Color alpha = tutorial.overlay.GetComponent<SpriteRenderer>().color;
                alpha.a = alphaV;
                tutorial.overlay.GetComponent<SpriteRenderer>().color = alpha;
            }
            else if (alphaV <= 0f)
            {
                tutorial.overlay.SetActive(false);
                tutorial.SwitchState(tutorial.unlocked);
            }
        }   
    }
    public override void OnTriggerEnter2D(TutorialStateManager tutorial, Collider2D trigger)
    {
    }
    public override void OnCollisionEnter2D(TutorialStateManager tutorial, Collision2D col)
    {
    }
}
