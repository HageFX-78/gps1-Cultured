using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    [SerializeField] TextAsset convoFile;
    [SerializeField] DialogueManager manager;
    [SerializeField] bool interactableType;
    [SerializeField] bool convoTriggered;//False means dialogue never triggered, true means triggered alr and wont trigger again. False by default (One-time dialogue scenario)
    
    [SerializeField] List<TransformList> transformLocationList = new List<TransformList>();

    public static bool interacting = false;

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !convoTriggered)
        {
            if(!interactableType)
            {
                convoTriggered = true;
                startConvo();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((Input.GetKey(KeyCode.Space)|| Input.GetKey(KeyCode.Mouse0)) && interactableType && !interacting)
        {
            interacting = true;
            startConvo();
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interacting = false;
        }
        
    }
    private void startConvo()
    {
        if (transformLocationList.Count == 0)
        {
            manager.startConversation(convoFile);
        }
        else
        {
            manager.startConversation(convoFile, transformLocationList);
        }
    }
}
