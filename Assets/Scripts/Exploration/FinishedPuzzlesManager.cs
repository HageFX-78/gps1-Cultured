using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedPuzzlesManager : MonoBehaviour
{

    public static List<string> PuzzleList = new List<string>();
    public static List<string> FinishedPuzzles = new List<string>();

    void Start()
    {
        StoreAndCheckPuzzles();
    }


    void StoreAndCheckPuzzles()
    {
        foreach (Transform child in transform)
        {
            foreach (string obj in FinishedPuzzles)
            {
                if (child.gameObject.name == obj)
                {
                    child.gameObject.SetActive(false);
                }
            }

            if (child.gameObject.tag == "Door")
            {
                PuzzleList.Add(child.gameObject.name);
            }
        }
    }
}
