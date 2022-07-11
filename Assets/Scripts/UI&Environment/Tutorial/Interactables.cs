using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    public bool onTop;
    public GameObject door;

    private void Update()
    {
        if (onTop)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                door.SetActive(false);
            }

        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onTop = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        onTop = false;
    }
}
