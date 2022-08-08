using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    [SerializeField] TextAsset convoFile;
    [SerializeField] DialogueManager manager;
    [SerializeField] bool interactableType;
    [SerializeField] bool convoTriggered;//False means dialogue never triggered, true means triggered alr and wont trigger again. False by default (One-time dialogue scenario)
    [SerializeField] float yPositionOffset;

    [SerializeField] GameObject promptPrefab;
    [SerializeField] List<TransformList> transformLocationList = new List<TransformList>();


    
    public static bool interacting = false;


    private void Start()
    {
        if (transform.Find("interact_prompt") == null)
        {
            GameObject thisPref = Instantiate(promptPrefab, new Vector3(transform.position.x, transform.position.y + yPositionOffset, transform.position.z), Quaternion.identity);
            thisPref.transform.SetParent(gameObject.transform);
            thisPref.name = "interact_prompt";
            thisPref.SetActive(false);
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !convoTriggered)
        {
            if(transform.Find("interact_prompt")!=null)
            {
                transform.Find("interact_prompt").gameObject.SetActive(true);
            }
            if(!interactableType)
            {
                convoTriggered = true;
                startConvo();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.Space)&& interactableType && !interacting && collision.gameObject.CompareTag("Player"))
        {
            if (transform.Find("interact_prompt") != null)
            {
                transform.Find("interact_prompt").gameObject.SetActive(false);
            }

            interacting = true;
            startConvo();
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if (transform.Find("interact_prompt") != null)
            {
                transform.Find("interact_prompt").gameObject.SetActive(false);
            }

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
