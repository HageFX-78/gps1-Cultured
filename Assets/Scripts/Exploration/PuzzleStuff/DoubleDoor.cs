using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoor : MonoBehaviour
{
    public static bool doorOpen;
    private bool onTop = false;
    [SerializeField] private GameObject[] doors;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onTop)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            onTop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onTop = false;
    }

}
