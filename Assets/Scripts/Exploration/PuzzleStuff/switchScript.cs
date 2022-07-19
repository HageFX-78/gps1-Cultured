using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchScript : MonoBehaviour
{
    [SerializeField] public PuzzleManager manager;

    [Header("Button ID")]
    public float buttonOrder;
    private new Renderer renderer;
    private bool onTop = false;

    [HideInInspector] public bool activated;
    public int puzzleType;

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
                switch (puzzleType) 
                {
                    case 1:
                        renderer.material.color = Color.green;
                        manager.ButtonPress(buttonOrder);
                        break;
                    case 2:
                        manager.ButtonPressType2(buttonOrder);
                        break;
                    case 3:
                        if (PuzzleManager.puzzleDone) manager.ResetPuzzle();
                        break;
                }
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
