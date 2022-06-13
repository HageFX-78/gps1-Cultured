using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    [SerializeField] TextAsset convoFile;
    [SerializeField] DialogueManager manager;

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        manager.startConversation(convoFile);
    }
}
