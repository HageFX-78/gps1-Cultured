using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityManager : MonoBehaviour
{
    public int sanity = 100;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && sanity != 0)
        {
            Debug.Log("Ouchies!");
            sanity -= 10;
            Debug.Log("Health Remaining: " + sanity);
        }
        if (sanity <= 0)
        {
            Debug.Log("Game Over Loser");
        }
    }
}
