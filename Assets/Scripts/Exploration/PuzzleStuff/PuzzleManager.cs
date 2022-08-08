using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleManager : MonoBehaviour
{
    private float currentButton;
    [SerializeField] List<GameObject> switchList;
    [SerializeField] public GameObject door;
    [SerializeField] public int puzzleType;
    public SpriteRenderer[] sprite;
    public Sprite[] green;
    public Sprite[] red;
    [HideInInspector] public bool puzzleDone;

    private void Start()
    {
        puzzleDone = false;
        for (int i = 0; i < switchList.Count; i++)
        {
            switchList[i].GetComponent<switchScript>().puzzleType = puzzleType;
        }

        if(door.activeInHierarchy == false)
        {
            FinishedPuzzle();
            puzzleDone = true;
        }
    }
    public void FinishedPuzzle()
    {
        for (int i = 0; i < switchList.Count; i++)
        {
            sprite[i].sprite = green[i];
        }
    }
    public void ButtonPress(float buttonNumber)
    {
        if (puzzleDone == false)
        {
            if (buttonNumber != currentButton + 1)
            {
                for(int i = 0; i < switchList.Count; i++)
                {
                    sprite[i].sprite = red[i];
                }
                currentButton = 0;
            }
            else
            {
                currentButton++;
                if (currentButton == switchList.Count)
                {
                    door.SetActive(false);
                    FinishedPuzzlesManager.PuzzleList.Remove(door.name);
                    FinishedPuzzlesManager.FinishedPuzzles.Add(door.name);
                    puzzleDone = true;
                }
                
            }
        }
    }

    public void ButtonPressType2(float buttonNumber)
    {
        if (puzzleDone == false)
        {
            switch(buttonNumber)
            {
                case 1:
                    for (int i = 0; i < 3; i++)
                    {
                        SwitchButtonColour(i);
                    }
                    break;
                case 2:
                    SwitchButtonColour(0);
                    SwitchButtonColour(1);
                    SwitchButtonColour(4);
                    break;
                case 3:
                    for (int i = 2; i < 5; i++)
                    {
                        SwitchButtonColour(i);
                    }
                    break;
                case 4:
                    SwitchButtonColour(1);
                    SwitchButtonColour(3);
                    SwitchButtonColour(4);
                    break;
                case 5:
                    SwitchButtonColour(5);
                    SwitchButtonColour(2);
                    SwitchButtonColour(4);
                    break;
                case 6:
                    SwitchButtonColour(1);
                    SwitchButtonColour(3);
                    SwitchButtonColour(5);
                    break;
            }
        }
        //Check if puzzle is completed
        for (int i = 0; i < switchList.Count; i++)
        {
            if (switchList[i].GetComponent<switchScript>().activated)
            {
                currentButton++;
            }
            else
            {
                currentButton = 0;
                break;
            }
            if (currentButton == switchList.Count)
            {
                door.SetActive(false);
                FinishedPuzzlesManager.PuzzleList.Remove(door.name);
                FinishedPuzzlesManager.FinishedPuzzles.Add(door.name);
                puzzleDone = true;
            }
        }
    }

    public void SwitchButtonColour(int num)
    {
        if (!switchList[num].GetComponent<switchScript>().activated)
        {
            sprite[num].sprite = green[num];
            switchList[num].GetComponent<switchScript>().activated = true;
        }
        else
        {
            sprite[num].sprite = red[num];
            switchList[num].GetComponent<switchScript>().activated = false;
        }
    }

    public void ResetPuzzle()
    {
        for (int i = 0; i < switchList.Count; i++)
        {
            sprite[i].sprite = red[i];
            switchList[i].GetComponent<switchScript>().activated = false;
        }
    }
}


