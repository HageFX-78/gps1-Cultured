using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonButton : MonoBehaviour
{
    [SerializeField] private int buttonNo;
    [SerializeField] private SimonSays ss;
    [SerializeField] private KeyCode kc;
    [SerializeField] private static int order;
    [SerializeField] private static bool complete = false;
    public static bool stage1 = false;
    public static bool stage2 = false;
    public static bool stage3 = false;
    private void Update()
    {
        if (Input.GetKeyDown( kc ) && !complete && SimonSays.clickable)  //Button clicking with resets
        {
            if (buttonNo == ss.simon[order])
            {
                ss.simonSays[ss.simon[order]].SetActive(true);
                for ( int i = 0; i < ss.simonSays.Length; i++)
                {
                    if ( i != ss.simon[order])
                    {
                        ss.simonSays[i].SetActive(false);
                    }
                }

                order++;
            }
            else
            {
                ss.Failure();
                order = 0;
            }
        }

        if (order == ss.simon.Length && !stage1) //level completion checking
        {
            stage1 = true;
            ss.simon = new int[4];
            order = 0;
            SimonSays.clickable = false;
            StartCoroutine(LevelDelay());    
        }
        else if (order == ss.simon.Length && stage1 && !stage2)
        {
            stage2 = true;
            ss.simon = new int[5];
            order = 0;
            SimonSays.clickable = false;
            StartCoroutine(LevelDelay());
        }
        else if (order == ss.simon.Length && stage2) 
        {
            complete = true;
        }

        if (complete)
        {
            // Insert puzzle completion lines here
        }
    }
    IEnumerator LevelDelay()
    {
        for (int i = 0; i < ss.simonSays.Length; i++)
        {
            ss.simonSays[i].SetActive(false);
        }
        yield return new WaitForSeconds(1.5f);
        ss.StartSystem();
    }
}
