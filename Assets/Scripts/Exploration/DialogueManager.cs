using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] GameObject dialogueUI;
    public TextMeshProUGUI talkerName, dialogueContent;

    [Header("Dialogue Settings")]
    [SerializeField] float switchDialogueCooldown;
    [SerializeField] int coolDownSplitPortion;
    [SerializeField] float typeSpeed;
    [SerializeField] float refreshDialogueTrigger;
    bool dialogueCooldown;
    bool dialogueActive;
    string[] dls; int curLineNum, dlsSize;

    private IEnumerator typeD;//Letter by letter display coroutine instance

    private void Start()
    {
        dialogueActive = false;
        dialogueCooldown = false;
        curLineNum = 0;
        dlsSize = 0;
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.Space)&& dialogueActive && !dialogueCooldown)
        {
            StopCoroutine(typeD);
            dialogueCooldown = true;
            
            //Debug.Log(curLineNum);
            if (curLineNum == dlsSize)
            {
                //Debug.Log("End of conversation");
                dialogueActive = false;
                dialogueUI.SetActive(false);
                dialogueCooldown = false;
                Time.timeScale = 1;
                StartCoroutine(smallDelay());
            }
            else
            {             
                StartCoroutine(endDialogueCooldown());
                displayCurrentDialogue();
            }    
            
        }
    }
    
    void displayCurrentDialogue()
    {
        string[] thisLine = dls[curLineNum].Split(':');
        talkerName.text = thisLine[0];

        typeD = typeDialogue(thisLine[1]);
        
        StartCoroutine(typeD);
        curLineNum++;
        
    }
    IEnumerator endDialogueCooldown()//Cooldown so player cant spam and immediately skip through entire conversation
    {
        while (dialogueCooldown)
        {
            
            yield return new WaitForSecondsRealtime(switchDialogueCooldown);
            dialogueCooldown = false;

        }
    }
    IEnumerator typeDialogue(string content)//Display character 1 by 1, visual effect
    {
        dialogueContent.text = "";
        foreach (char letter in content)
        {
            dialogueContent.text += letter;
            yield return new WaitForSecondsRealtime(typeSpeed);
        }
    }
    IEnumerator smallDelay()
    {
        while (TriggerDialogue.interacting)
        {
            yield return new WaitForSecondsRealtime(refreshDialogueTrigger);
            TriggerDialogue.interacting = false;
        }
    }
    public void startConversation(TextAsset targetFile)
    {
        dialogueUI.SetActive(true);
        dialogueActive = true;
        dialogueCooldown = true;
        StartCoroutine(endDialogueCooldown());
        curLineNum = 0;
        dls = targetFile.text.Split('\n');
        dlsSize = dls.Length;
        Time.timeScale = 0;
        displayCurrentDialogue();
    }

}
