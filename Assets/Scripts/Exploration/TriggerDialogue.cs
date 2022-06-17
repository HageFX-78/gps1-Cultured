using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    [SerializeField] TextAsset convoFile;
    [SerializeField] DialogueManager manager;
    [SerializeField] bool interactableType;
    [SerializeField] bool convoTriggered;//False means dialogue never triggered, true means triggered alr and wont trigger again. False by default
    bool inRange = false, interacting = false;

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !convoTriggered)
        {
            if(!interactableType)
            {
                convoTriggered = true;
                manager.startConversation(convoFile);
            }
            else
            {
                inRange = true;
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
            interacting = false;
        }
        
    }
    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Space) && interactableType && inRange && !interacting)
        {
            interacting = true;
            manager.startConversation(convoFile);          
        }
    }
}
