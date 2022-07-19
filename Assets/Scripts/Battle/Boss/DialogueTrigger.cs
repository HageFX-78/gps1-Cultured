using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //setting file to read
    [Header("Ink JSON")]
    [SerializeField] private TextAsset phase1;

    private void Start()
    {
        BossDialogueManager.instance.EnterDialogueMode(phase1);
    }
}
