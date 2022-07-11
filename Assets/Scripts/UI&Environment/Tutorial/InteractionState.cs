using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionState : TutorialBaseState
{
    TutorialStateManager tutorial;
    private float timer = 0.6f;
    private bool waiting = true;
    private bool triggered;
    private bool triggeredR;

    public override void EnterState(TutorialStateManager tutorial)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        tutorial.battleUi[7].SetActive(true);
    }
    public override void UpdateState(TutorialStateManager tutorial)
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (waiting)
            {
                tutorial.partialConvo(tutorial.convoFile3, 34, 37);
                waiting = false;
            }
        }
        if (tutorial.player.transform.position.x >= 61 && !triggered)
        {
            tutorial.partialConvo(tutorial.convoFile3, 37, 41);
            triggered = true;
        }

        if(TRemnant.collect && !triggeredR)
        {
            tutorial.partialConvo(tutorial.convoFile3, 42, 51);
            triggeredR = true;
        }
    }
    public override void OnTriggerEnter2D(TutorialStateManager tutorial, Collider2D trigger)
    {

        if(TRemnant.collect && trigger.CompareTag("Door"))
        {
            tutorial.SceneChange();
        }
    }
    public override void OnCollisionEnter2D(TutorialStateManager tutorial, Collision2D col)
    {
    }
}
