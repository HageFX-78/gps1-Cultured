using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //setting file to read
    [Header("Ink JSON")]
    [SerializeField] public TextAsset prePhase;
    [SerializeField] public TextAsset phase1Dialogue;
    [SerializeField] public TextAsset phase2Dialogue;
    [SerializeField] public TextAsset gameOver;

    private void Start()
    {
        BossDialogueManager.instance.EnterDialogueMode(phase1Dialogue);
    }
}
