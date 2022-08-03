using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSays : MonoBehaviour
{
    public int[] simon;
    public GameObject[] simonSays;
    [SerializeField] private float timer = 5;
    [SerializeField] private int n = 0;
    public static bool clickable = false;
    public int turns = 4;
    private int tempN = 5;
    private int r;
    private void Start()
    {
        for (int i = 0; i < simon.Length; i++) 
        {
            RanNum(i);
        }
        StartSystem();
    }

    private void Update()
    {

       if(Input.GetKeyDown(KeyCode.P))
        {
            StartSystem();
        }
    }

    public void StartSystem() // Initiate random num + start the puzzle
    {
        for (int i = 0; i < simon.Length; i++)
        {
            RanNum(i);
        }
        n = 0;
        StartCoroutine(Flickering(1));
    }
    public void RanNum(int x) // Random num gen with no b2b number
    {
        r = Random.Range(0, 4);
        if(r == tempN)
        {
            RanNum(x);
        }
        else if (r != tempN)
        {
            simon[x] = r ;
            tempN = r;
        }
    }
    public void Failure() // puzzle reset
    {
        for (int i = 0; i < simonSays.Length; i++)
        {
            simonSays[i].SetActive(false);
        }
    }
    IEnumerator Flickering(int timer)
    {
        clickable = false;
        simonSays[simon[n]].SetActive(true);
        for (int i = 0; i < simonSays.Length;i++) //Check if i is equal to the value of simon[n]
        {
            if ( i != simon[n])
            {
                simonSays[i].SetActive(false);
            }
        }
        n++;
        yield return new WaitForSeconds(timer);

        if (n != simon.Length) // Repeating coroutine
        {
            StartCoroutine(Flickering(1));
        }
        else if (n == simon.Length)
        {
            for (int i = 0; i < simonSays.Length; i++) //Allow clicking once finished flicker
            {
                clickable = true;
                simonSays[i].SetActive(false);
            }
        }
    }
}
