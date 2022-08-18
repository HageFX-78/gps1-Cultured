using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSays : MonoBehaviour
{
    [SerializeField] public AudioSource audioSrc;
    public int[] simon;
    public GameObject[] simonSays;
    [SerializeField] private int n = 0;
    public static bool clickable = true ;
    [SerializeField] private GameObject remnant;
    [SerializeField] public GameObject door;
    private bool onTop;
    private int tempN = 5;
    private int r;
    private static bool collected;
    [SerializeField] private SpriteRenderer starter;
    [SerializeField] private Sprite green;
    [SerializeField] private Sprite red;

    private void Start()
    {
        onTop = false;
        clickable = true;
        n = 0;
    }
    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.Space) && onTop && clickable)
        {
            audioSrc.clip = LVL1SFList.sflInstance.SFList[4];
            audioSrc.Play();
            StartSystem();
        }
        if (SimonButton.complete && !collected) 
        {
            // Insert puzzle completion lines here
            audioSrc.clip = LVL1SFList.sflInstance.SFList[2];
            audioSrc.Play();

            remnant.SetActive(true);
            door.SetActive(false);
            FinishedPuzzlesManager.PuzzleList.Remove(door.name);
            FinishedPuzzlesManager.FinishedPuzzles.Add(door.name);
            collected = true;
        }

        if (clickable)
        {
            starter.sprite = red;
        }
        else if (!clickable)
        {
            starter.sprite = green;
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
        //Debug.Log("FAIL");
        StartCoroutine(Flickering(1));
    }
    IEnumerator Flickering(int timer)
    {
        clickable = false;
        audioSrc.clip = LVL1SFList.sflInstance.SFList[4];
        audioSrc.Play();
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
