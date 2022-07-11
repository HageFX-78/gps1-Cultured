using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : TutorialBaseState
{
    TutorialStateManager tutorial;
    private int step = 0;
    private int turn = 0;
    private float cd = 0.7f;

    public override void EnterState(TutorialStateManager tutorial)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        tutorial.battleUi[0].SetActive(true);
        tutorial.battleUi[1].SetActive(true);
        tutorial.battleUi[7].SetActive(false);
        tutorial.partialConvo(tutorial.convoFile3, 0, 9);
    }
    public override void UpdateState(TutorialStateManager tutorial)
    {
        if (tutorial.end && step == 0)
        {
            if (cd > 0)
            {
                cd -= Time.deltaTime;
            }
            else
            {
                tutorial.battleUi[2].SetActive(true);
                tutorial.partialConvo(tutorial.convoFile3, 9, 12);
                step++;
                cd = 0.6f;
            }
        }
        else if (tutorial.end && step == 1)
        {
            if (cd > 0)
            {
                cd -= Time.deltaTime;
            }
            else
            {
                tutorial.battleUi[3].SetActive(true);
                tutorial.SetGuideUi(385);
                tutorial.partialConvo(tutorial.convoFile3, 12, 16);
                step++;
                cd = 0.6f;
                turn++;
            }
        }
        else if (tutorial.end && turn == 1)
        {
            if (cd > 0)
            {
                cd -= Time.deltaTime;
            }
            else
            {//T enemy is delusional
                tutorial.partialConvo(tutorial.convoFile3, 16, 23);
                cd = 0.5f;
                turn++;
            }
        }
        else if (tutorial.end && turn == 2)
        {
            tutorial.A.interactable = true;
        }

        if (tutorial.pDial.activeSelf == true && tutorial.curTurn == 1) //Start of turn 1
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                tutorial.pDial.SetActive(false);
                tutorial.t1 = true;
            }
        }
        else if (tutorial.t1 && turn == 2)
        {
            if (cd > 0)
            {
                cd -= Time.deltaTime;
            }
            else
            {
                tutorial.partialConvo(tutorial.convoFile3, 23, 26);
                turn++;
                cd = 0.5f;
            }
        }
        else if (tutorial.end && turn == 3 && tutorial.eDial.activeSelf == false && tutorial.curTurn == 1)
        {
            tutorial.eDial.SetActive(true);
            tutorial.negBar.sizeDelta = new Vector2(400, 15);
        }
        else if (tutorial.eDial.activeSelf == true && tutorial.curTurn == 1 && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            Debug.Log(tutorial.eDial.activeSelf);
            tutorial.eDial.SetActive(false);
            turn++;
        }
        else if (tutorial.end && turn == 4)
        {
            if (cd > 0 )
            {
                cd -= Time.deltaTime;
            }
            else 
            {
                tutorial.partialConvo(tutorial.convoFile3, 26, 27);
                turn++;
                cd = 0.5f;
            }   
        }
        else if(turn == 5 && tutorial.end)
        {
            tutorial.dialopt.SetActive(true);
            tutorial.A.interactable = false;
            tutorial.D.interactable = true;
            turn++;
            tutorial.curTurn++;
        }

        if (tutorial.pDial.activeSelf == true && tutorial.curTurn == 2) //Start of turn 2
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                tutorial.pDial.SetActive(false);
                tutorial.t2 = true;
            }
        }
        else if (tutorial.t2 && turn == 6)
        {
            if (cd > 0)
            {
                cd -= Time.deltaTime;
            }
            else
            {
                tutorial.partialConvo(tutorial.convoFile3, 27, 29);
                turn++;
                cd = 0.5f;
            }
        }
        else if (tutorial.end && turn == 7 && tutorial.eDial.activeSelf == false)
        {
            tutorial.eDial.SetActive(true);
            tutorial.negBar.sizeDelta = new Vector2(320, 15);
        }
        else if (tutorial.eDial.activeSelf == true && tutorial.curTurn == 2 && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            tutorial.eDial.SetActive(false);
            turn++;
        }
        else if (tutorial.end && turn == 8)
        {
            if (cd > 0)
            {
                cd -= Time.deltaTime;
            }
            else
            {
                Debug.Log(turn);
                tutorial.partialConvo(tutorial.convoFile3, 29, 30);
                turn++;
                cd = 0.5f;
            }
        }
        else if (turn == 9 && tutorial.end)
        {
            tutorial.dialopt.SetActive(true);
            tutorial.D.interactable = false;
            tutorial.C.interactable = true;
            turn++;
            tutorial.curTurn++;
        }
       if (tutorial.pDial.activeSelf == true && tutorial.curTurn == 3) //Start of turn 3
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                tutorial.pDial.SetActive(false);
                tutorial.t3 = true;
            }
        }
        else if (tutorial.t3 && turn == 10)
        {
            if (cd > 0)
            {
                cd -= Time.deltaTime;
            }
            else
            {
                tutorial.partialConvo(tutorial.convoFile3, 30, 34);
                turn++;
                cd = 0.5f;
            }
        }
        else if (tutorial.end && turn == 11 && tutorial.eDial.activeSelf == false && tutorial.curTurn == 3)
        {
            tutorial.battleObj.SetActive(false);
            tutorial.enemy.SetActive(false);
            tutorial.SwitchState(tutorial.interaction);
        }
    }
        
    public override void OnTriggerEnter2D(TutorialStateManager tutorial, Collider2D trigger)
    {
    }
    public override void OnCollisionEnter2D(TutorialStateManager tutorial, Collision2D col)
    {
    }
}
