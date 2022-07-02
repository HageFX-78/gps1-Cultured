using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    [SerializeField] private static GameObject sounds;
    void Awake()
    {
           
        if (sounds != null)
        {
            Destroy(sounds);
        }
        sounds = gameObject;

        DontDestroyOnLoad(sounds);

    }


}
