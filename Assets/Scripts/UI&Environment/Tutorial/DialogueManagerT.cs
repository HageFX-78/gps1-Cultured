using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManagerT : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] GameObject dialogueUI;
    public TextMeshProUGUI talkerName, dialogueContent;

    [Header("Dialogue Settings")]
    [SerializeField] float switchDialogueCooldown;
    [SerializeField] int coolDownSplitPortion;
    [SerializeField] float typeSpeed;
    [SerializeField] float refreshDialogueTrigger;
    [SerializeField] float defType;
    [SerializeField] AudioSource audioSrc;
    [SerializeField] AudioClip[] SFList;

    bool dialogueCooldown;
    public bool dialogueActive;
    string[] dls; int curLineNum, dlsSize;
    bool canInput, typingDialogue;
    private IEnumerator typeD;//Letter by letter display coroutine instance
    string currentText;

    private void Start()
    {
        defType = typeSpeed;
        dialogueActive = false;
        dialogueCooldown = false;
        curLineNum = 0;
        dlsSize = 0;
    }
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) && dialogueActive && canInput)
        {
            canInput = false;
            if (curLineNum >= dlsSize)
            {
                dialogueActive = false;
                dialogueUI.SetActive(false);
                dialogueCooldown = false;
                Time.timeScale = 1;

                StartCoroutine(smallDelay());//Stops player from triggering another convo in a frame at the end
            }
            else
            {
                if (typingDialogue)
                {
                    instantShowDialogue();
                }
                else
                {
                    typeSpeed = defType;
                    displayCurrentDialogueTutorial();
                }
            }
        }
    }
    public void instantShowDialogue()
    {
        canInput = false;
        typingDialogue = false;
        typeSpeed = 0;
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
        foreach (char letter in content)
        {
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
        }
    }
    public void startConversation(TextAsset targetFile)
    {
        currentText = "";
        canInput = true;

        dialogueUI.SetActive(true);
        dialogueActive = true;
        dialogueCooldown = true;
        StartCoroutine(enableInput());
        curLineNum = 0;
        dls = targetFile.text.Split('\n');
        dlsSize = dls.Length;
        Time.timeScale = 0;
        displayCurrentDialogueTutorial();
    }
    IEnumerator typeDialogueT(string content, TextMeshProUGUI txtbox)
    {
        typingDialogue = true;

        txtbox.text = "";
        bool colorFontMode = false;
        bool green = false;
        bool red = false;
        bool blue = false;

        foreach (char letter in content)
        {

            if (letter == '[')
            {
                colorFontMode = true;
                continue;
            }
            else if (letter == '-')
            {
                green = true;
                red = false;
                blue = false;
                continue;
            }
            else if (letter == '_')
            {
                red = true;
                green = false;
                blue = false;
                continue;
            }
            else if (letter == '=')
            {
                red = false;
                green = false;
                blue = true;
                continue;
            }
            else if (letter == ']')
            {
                red = false;
                green = false;
                blue = false;
                colorFontMode = false;
                continue;
            }
            if (Random.Range(0, PlayerCommonStatus.typeBeepChance) == 0)
            {
                audioSrc.clip = SFList[0];
                audioSrc.Play();
            }
            if (colorFontMode)
            {
                if (green)
                {
                    txtbox.text += $"<color={"green"}>{letter}</color>";
                }
                else if (blue)
                {
                    txtbox.text += $"<color={"blue"}>{letter}</color>";
                }
                else if (red)
                {
                    txtbox.text += $"<color={"red"}>{letter}</color>";
                }
                else
                {
                    txtbox.text += $"{letter}";//Default color coding in case
                }
            }
            else
            {
                txtbox.text += letter;
            }
            yield return new WaitForSecondsRealtime(typeSpeed);
        }
        typingDialogue = false;
    }
    void displayCurrentDialogueTutorial()
    {
        StartCoroutine(enableInput());
        string[] thisLine = dls[curLineNum].Split(':');
        talkerName.text = thisLine[0];
        typeD = typeDialogueT(thisLine[1], dialogueContent);
        currentText = thisLine[1];
        if (thisLine.Length > 2)
        {
            string sanitizedKey;
            if (curLineNum == dlsSize - 1)
            {
                sanitizedKey = thisLine[2];//Last line doesnt have a weird extra character at the end so no need to sanitize
            }
            else
            {
                sanitizedKey = thisLine[2].Substring(0, thisLine[2].Length - 1);
            }
        }
        StartCoroutine(typeD);
        curLineNum++;
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
        displayCurrentDialogueTutorial();
    }
}
