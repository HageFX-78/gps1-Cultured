using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    private float currentButton;
    [SerializeField] List<GameObject> switchList;
    [SerializeField] public GameObject door;
    private new Renderer renderer;

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
                for(int i = 0; i < switchList.Count; i++)
                {
                    renderer = switchList[i].GetComponent<Renderer>();
                    renderer.material.color = Color.red;
                }
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


