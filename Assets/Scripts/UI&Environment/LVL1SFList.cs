using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVL1SFList : MonoBehaviour
{
    public static LVL1SFList sflInstance;
    [SerializeField] public AudioClip[] SFList;

    private void Awake()
    {
        sflInstance = this;
    }
}
