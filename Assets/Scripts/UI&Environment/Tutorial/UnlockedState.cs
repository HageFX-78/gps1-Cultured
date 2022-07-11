using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedState : TutorialBaseState
{
    TutorialStateManager tutorial;
    private float runTimer = 0;
    private bool sprint = false;
    private bool walk = false;
    private bool enemy = false;
    private bool firstEnc = true;

    public override void EnterState(TutorialStateManager tutorial)
    {
        tutorial.partialConvo(tutorial.convoFile2, 0, 2);
    }
    public override void UpdateState(TutorialStateManager tutorial)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;

        if (Input.GetAxisRaw("Horizontal") !=0 || Input.GetAxisRaw("Vertical") !=0 ) 
        {
            if (runTimer < 2f)
            {
                runTimer += Time.deltaTime;
            }
            else if (runTimer < 4f && Input.GetKey(KeyCode.LeftShift))
            {
                runTimer += Time.deltaTime;
            }
        }
        if (runTimer > 1.5 && !sprint)
        {
            tutorial.partialConvo(tutorial.convoFile2, 2, 5);
            sprint = true;
        }
        else if ((runTimer > 3f && !walk) || (tutorial.gameObject.transform.position.x > 30 && !walk))
        {
            tutorial.partialConvo(tutorial.convoFile2, 5, 6);
            walk = true;
        }
    }
    public override void OnTriggerEnter2D(TutorialStateManager tutorial, Collider2D trigger)
    {  
        GameObject other = trigger.gameObject;
        if (other.CompareTag("TutorialEnemy") && firstEnc) //Alter collision box base on enemy entry into camera
        {
            tutorial.partialConvo(tutorial.convoFile2, 6, 12);
            firstEnc = false;
            tutorial.FirstEnc();
            
        }
        else if (other.CompareTag("TutorialEnemy") && !firstEnc)
        {
            tutorial.SwitchState(tutorial.battle);
        }
    }
    public override void OnCollisionEnter2D(TutorialStateManager tutorial, Collision2D col)
    {
    }
}
