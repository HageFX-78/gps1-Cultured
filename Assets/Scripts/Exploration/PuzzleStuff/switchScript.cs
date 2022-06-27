using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    [SerializeField] public PuzzleManager manager;

    [Header("Put number in the order the buttons supposed to be pressed")]
    public float buttonOrder;
    private new Renderer renderer;
    public bool onTop = false;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material.color = Color.red;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (onTop)
            {
                renderer.material.color = Color.green;
                manager.ButtonPress(buttonOrder);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.gameObject.CompareTag("Player"))
        {
           onTop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
            onTop = false;
    }
}
