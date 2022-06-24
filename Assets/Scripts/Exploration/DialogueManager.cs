using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] GameObject dialogueUI;
    [SerializeField] Transform camPos;
    [SerializeField] CameraController camState;
    [SerializeField] Transform playerPos;
    public TextMeshProUGUI talkerName, dialogueContent;

    [Header("Dialogue Settings")]
    [SerializeField] float switchDialogueCooldown;
    [SerializeField] int coolDownSplitPortion;
    [SerializeField] float typeSpeed;
    [SerializeField] float refreshDialogueTrigger;

    [Header("Lerp Settings")]
    [SerializeField] float lerpTravelInterval;
    [SerializeField] float travelDistancePerLoop;//Increase if you want it faster, lower to decrease

    Dictionary<string, Transform> gotoDC = new Dictionary<string, Transform>();//Transform list for locations the cam lerps to
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
            if (curLineNum >= dlsSize)
            {
                //Debug.Log("End of conversation");
                dialogueActive = false;
                dialogueUI.SetActive(false);
                dialogueCooldown = false;
                Time.timeScale = 1;
                
                StartCoroutine(smallDelay());//Stops player from triggering another convo in a frame at the end
                camState.isCutScene = false;
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
        if(thisLine.Length>2)
        {
            string sanitizedKey;
            if(curLineNum == dlsSize-1)
            {
                sanitizedKey = thisLine[2];//Last line doesnt have a weird extra character at the end so no need to sanitize
            }
            else
            {
                sanitizedKey = thisLine[2].Substring(0, thisLine[2].Length - 1);
            }
            camState.isCutScene = true;
            Vector3 targetPosition = new Vector3(gotoDC[sanitizedKey].position.x, gotoDC[sanitizedKey].position.y, camPos.position.z);
            Vector3 defaultPosition = new Vector3(camPos.position.x, camPos.position.y, camPos.position.z);
            StartCoroutine(lerpToTarget(defaultPosition, targetPosition));
        }
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
    IEnumerator lerpToTarget(Vector3 defPos, Vector3 tarPos)
    {
        float distance = Vector3.Distance(defPos, tarPos);
        
        float interpVal = 0.0f;
        //float travelTime = 5.0f;
        //lerpTravelInterval = 1.0f / travelTime;
        //travelDistancePerLoop = distance * lerpTravelInterval;
        while(interpVal<=1.0f)
        {
            camPos.position = Vector3.Lerp(defPos, tarPos, interpVal);
            
            yield return new WaitForSecondsRealtime(lerpTravelInterval);
            interpVal += travelDistancePerLoop;
            
            //Debug.Log(interpVal);
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
    public void startConversation(TextAsset targetFile, List<TransformList> trList)
    {       
        gotoDC.Clear();
        gotoDC.Add("player", playerPos);
        if (trList.Count > 0)
        {
            foreach (TransformList x in trList)
            {
                gotoDC.Add(x.locationName, x.transformReference);
            }
        }
        //*/

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
