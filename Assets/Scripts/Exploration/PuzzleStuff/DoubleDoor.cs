using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoor : MonoBehaviour
{
    public static bool doorOpen;
    [SerializeField] private GameObject[] doors;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            doorOpen = !doorOpen;
        }
        if(doorOpen)
        {
            doors[0].gameObject.SetActive(false);
            doors[1].gameObject.SetActive(true);
        }
        else if (!doorOpen)
        {
            doors[0].gameObject.SetActive(true);
            doors[1].gameObject.SetActive(false);
        }
    }

}
