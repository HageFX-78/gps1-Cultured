using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonButton : MonoBehaviour
{
    [SerializeField] private int buttonNo;
    [SerializeField] private SimonSays ss;
    [SerializeField] private KeyCode kc;
    [SerializeField] private static int order;
    [SerializeField] public static bool complete = false;
    public static bool stage1 = false;
    public static bool stage2 = false;
    [SerializeField]private bool onTop = false;

    private void Start()
    {
        stage1 = false;
        stage2 = false;
        order = 0;
        onTop = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(kc) && !complete && SimonSays.clickable && onTop)  //Button clicking with resets
        {
            if (buttonNo == ss.simon[order])
            {
                ss.simonSays[ss.simon[order]].SetActive(true);
                Debug.Log(ss.simon[order]);
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
    }
    IEnumerator LevelDelay()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < ss.simonSays.Length; i++)
        {
            ss.simonSays[i].SetActive(false);
        }
        yield return new WaitForSeconds(1.5f);
        ss.StartSystem();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            {
            onTop = true;
            }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onTop = false ;
    }
}
