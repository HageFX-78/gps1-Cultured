using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossDialogueManager : MonoBehaviour
{
    public static BossDialogueManager instance;
    public BossEmotionManager bossEmotionManager;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Image dialoguePanelImage;
    [SerializeField] private RectTransform dialogueTransform;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Emotion UI")]
    [SerializeField] private GameObject emotionPanel;

    private Story currentStory;
    public bool storyIsPlaying;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI [] choicesText;
    private int choiceIndex;
    public List<string> tempTag;
    private int tagIndex;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Debug.LogWarning("Found more than one singleton");
        }
        else
            instance = this;


        storyIsPlaying = false;
        //initialise array of choicetext to be the same as the amount of choices
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            //setting the element of the array to the text of each choice
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if(!storyIsPlaying)
        {
            return;
        }

        //if there are no more options to give and player click
        if (storyIsPlaying && currentStory.currentChoices.Count == 0)
        {
            //if there is no choices, sets the color, size, position
            dialoguePanelImage.color = Color.white;
            dialogueText.color = Color.black;

            //setting the alpha on the dialogueBox
            Color temp = dialoguePanelImage.color;
            temp.a = 0.5f;
            dialoguePanelImage.color = temp;

            if (Input.GetMouseButtonDown(0))
            {
                ContinueStory();
            }
        }
    }

    public void EnterDialogueMode(TextAsset inkJson)
    {
        //instantiate a new instance of story based on the json file and enable the panels
        currentStory = new Story(inkJson.text);
        storyIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        storyIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        //if there is more than 1 line in the story, set the dialogue text to the currentStory line
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();

            //reads the current tags
            List<string> currentTag = currentStory.currentTags;
            tempTag = currentTag; 
            DisplayChoices();
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private void DisplayChoices()
    {
        //creates a list of choices and tags into a list
        List<Choice> currentChoices = currentStory.currentChoices;
        
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given: " + currentChoices.Count);
        }

        choiceIndex = 0;

        foreach(Choice choice in currentChoices)
        {
            //for each choice in the list, activate the buttons, and set that buttons text to that choice
            choices[choiceIndex].gameObject.SetActive(true);
            choicesText[choiceIndex].text = choice.text;

            choiceIndex++;
        }

        for(int i = choiceIndex; i< choices.Length; i++)
        {
            //if there are any extra choices based on where the index left off, setactive(false) the remaining buttons
            choices[i].gameObject.SetActive(false);
        }

        if(choiceIndex != 0)
        {
            //if there are choices, resets the color, size, position
            dialoguePanelImage.color = Color.black;
            dialogueText.color = Color.white;
            
            //setting the alpha on the dialogueBox
            Color temp = dialoguePanelImage.color;
            temp.a = 0.5f;
            dialoguePanelImage.color = temp;

        }
    }

    //onclick function for buttons
    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        //========================================Player deals dmg================================================
        if(tempTag.Count > 0)
        {
            if(bossEmotionManager.phase1)
            {
                //deals damage to boss based on choice and the tag
                int randDmg = (int)UnityEngine.Random.Range(bossEmotionManager.minBaseDamage, bossEmotionManager.maxBaseDamage);
                //passes in the temptag based on the choiceIndex
                bossEmotionManager.DealDamage(randDmg, tempTag[choiceIndex]);
                
                //boss recovers after attack
                int randRecover = (int)UnityEngine.Random.Range(bossEmotionManager.minBaseDamage, bossEmotionManager.maxBaseDamage);
                bossEmotionManager.Recover(randRecover);
            }
        }
        
        ContinueStory();     
        BossEmotionManager.turnCounter--;

    }
}
