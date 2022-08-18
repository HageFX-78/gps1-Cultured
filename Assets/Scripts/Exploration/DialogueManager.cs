using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSrc;

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
    public bool dialogueActive;
    string[] dls; int curLineNum, dlsSize;
    bool canInput, typingDialogue;
    private IEnumerator typeD;//Letter by letter display coroutine instance
    string currentText;

    private void Start()
    {
        dialogueActive = false;
        dialogueCooldown = false;
        curLineNum = 0;
        dlsSize = 0;
    }
    void Update()
    {
        if((Input.GetKey(KeyCode.Space)|| Input.GetKey(KeyCode.Mouse0)) && dialogueActive && canInput)
        {
            canInput = false;
            //Debug.Log(curLineNum);
            if (curLineNum >= dlsSize)
            {
                //Debug.Log("End of conversation");
                dialogueActive = false;
                dialogueUI.SetActive(false);
                dialogueCooldown = false;
                Time.timeScale = 1;
                
                StartCoroutine(smallDelay());//Stops player from triggering another convo in a frame at the end
                
            }
            else
            {             
                if(typingDialogue)
                {
                    instantShowDialogue();
                }
                else
                {
                    displayCurrentDialogue();
                }               
            }               
        }
    }
    void displayCurrentDialogue()
    {
        StartCoroutine(enableInput());
        string[] thisLine = dls[curLineNum].Split(':');
        talkerName.text = thisLine[0];
        typeD = typeDialogue(thisLine[1]);
        currentText = thisLine[1];
        if (thisLine.Length>2)
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
    public void instantShowDialogue()
    {
        canInput = false;
        typingDialogue = false;
        StopCoroutine(typeD);
        dialogueContent.text = currentText;
        StartCoroutine(enableInput());
    }
    IEnumerator enableInput()//Cooldown so player cant spam and immediately skip through entire conversation
    {
        while (!canInput)
        {
            
            yield return new WaitForSecondsRealtime(switchDialogueCooldown);
            canInput = true;

        }
    }
    IEnumerator typeDialogue(string content)//Display character 1 by 1, visual effect
    {
        typingDialogue = true;
        dialogueContent.text = "";

        audioSrc.clip = LVL1SFList.sflInstance.SFList[0];
        foreach (char letter in content)
        {
            if (Random.Range(0, PlayerCommonStatus.typeBeepChance) == 0)
            {
                audioSrc.Play();
            }
            dialogueContent.text += letter;
            yield return new WaitForSecondsRealtime(typeSpeed);
        }
        typingDialogue = false;
    }
    IEnumerator smallDelay()
    {
        while (TriggerDialogue.interacting)
        {
            yield return new WaitForSecondsRealtime(refreshDialogueTrigger);

            TriggerDialogue.interacting = false;
            camState.isCutScene = false;
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
        currentText = "";
        canInput = true;

        dialogueUI.SetActive(true);
        dialogueActive = true;
        dialogueCooldown = true;
        curLineNum = 0;
        dls = targetFile.text.Split('\n');
        dlsSize = dls.Length;
        Time.timeScale = 0;
        displayCurrentDialogue();
    }
    public void startConversation(TextAsset targetFile, List<TransformList> trList)//Function override
    {
        canInput = true;
        currentText = "";

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
        curLineNum = 0;
        dls = targetFile.text.Split('\n');
        dlsSize = dls.Length;
        Time.timeScale = 0;
        displayCurrentDialogue();
    }
    
    public void partialLineConvo(TextAsset targetFile, int begin, int end)
    {
        currentText = "";
        canInput = true;

        dialogueUI.SetActive(true);
        dialogueActive = true;
        dialogueCooldown = true;
        StartCoroutine(enableInput());
        curLineNum = begin;
        dls = targetFile.text.Split('\n');
        dlsSize = end;
        Time.timeScale = 0;
        displayCurrentDialogue();
    }
}
