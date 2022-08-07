using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSays : MonoBehaviour
{
    public int[] simon;
    public GameObject[] simonSays;
    [SerializeField] private float timer = 5;
    [SerializeField] private int n = 0;
    public static bool clickable = true ;
    [SerializeField] private GameObject remnant;
    private bool onTop;
    private int tempN = 5;
    private int r;
    private static bool collected;

    private void Start()
    {
        onTop = false;
        clickable = true;
        n = 0;

    }
    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.Space) && onTop && clickable )
        {
            StartSystem();
        }

        if (SimonButton.complete && !collected) 
        {
            // Insert puzzle completion lines here
            remnant.SetActive(true);
            collected = true;
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
        n = 0;
        StartCoroutine(Flickering(1));
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onTop = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        onTop = false;
    }
}
