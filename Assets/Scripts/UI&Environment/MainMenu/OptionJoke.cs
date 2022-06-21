using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionJoke : MonoBehaviour
{
    float timer = 3f;
    private void OnEnable()
    { 
        timer = 3f;
    }
    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
   
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
