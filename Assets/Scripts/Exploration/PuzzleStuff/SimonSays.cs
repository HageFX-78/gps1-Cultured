using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSays : MonoBehaviour
{
    [SerializeField] private int[] simon;
    [SerializeField] private GameObject[] simonSays;
    [SerializeField] private int[] player;
    [SerializeField] private float timer = 1;
    [SerializeField] private int n = 0;
    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            simon[i] = Random.Range(1, 4);
        }
    }

    private void Update()
    {
        Flicker();
    }

    public void Flicker()
    {
        if( timer > 0 )
        {
            timer -= Time.deltaTime;
        }
        else
        {
            for ( int i = 0; i < 4; i++)
            {
                if (i != n)
                {
                    simonSays[simon[i]].SetActive(false);
                }
                else
                {
                    simonSays[simon[i]].SetActive(true);
                }
            }
            n++;
            n = n % 4;
            if(n == 0)
            {
                timer = 2f;
            }
            else
            {
                timer = 1;
            }
        }
    }
}
