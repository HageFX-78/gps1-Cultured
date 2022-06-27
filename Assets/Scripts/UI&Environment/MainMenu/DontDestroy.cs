using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    [SerializeField] private GameObject musical = null;
    private void Awake()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Audio");

        if (objects.Length >1)
        {
            Destroy(musical);
        }

        DontDestroyOnLoad(musical);
    }
}
