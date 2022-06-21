using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleManager : MonoBehaviour
{
    private float currentButton;
    [SerializeField] public switchScript button1;
    [SerializeField] public switchScript button2;
    [SerializeField] public switchScript button3;
    [SerializeField] public switchScript button4;
    [SerializeField] public GameObject door;

    private bool puzzleDone;

    private void Start()
    {
        puzzleDone = false;
    }

    public void ButtonPress(float buttonNumber)
    {
        if (puzzleDone == false)
        {
            if (buttonNumber != currentButton + 1)
            {
                button1.PuzzleFail();
                button2.PuzzleFail();
                button3.PuzzleFail();
                button4.PuzzleFail();
                currentButton = 0;
            }
            else
            {
                currentButton++;
                if (currentButton == 4)
                {
                    Destroy(door);
                    puzzleDone = true;
                }
                
            }
        }
    }
}


