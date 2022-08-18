using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class DBManager : MonoBehaviour
{
    //DB stands for dialogue and button manager, not dick & balls
    [Header("Manager References")]
    public BattleStateManager battle;
    public EmotionManager enemyEmotion;
    public ScreenShake enemyShakeRef;
    
    [SerializeField] AudioSource audioSrc;
    [SerializeField] AudioClip[] SFList;

    [Header("UI References")]
    public TextAsset pDialoguefile;//Player dialogue options file
    public TextAsset eDialoguefile;//Enemy dialogue options file
    public TextAsset newDialogueFile;//Overhauled battle flow dialogue file

    public TextMeshProUGUI convoTextPlayer, convoTextEnemy, talkerName, enemyLastConvo, runText, runChance;//Text dialogue box reference
    public Turn turnScriptRef;

    public Button btn1; public Button btn2; public Button btn3; public Button btn4;
    TextMeshProUGUI bText1; TextMeshProUGUI bText2; TextMeshProUGUI bText3; TextMeshProUGUI bText4;
    List<Button> btnList;
    List<TextMeshProUGUI> btnTXTList;

    public GameObject playerOptionsUI, playerDialogueUI, enemyDialogueUI, lastConvoUI ,runUI;

    [Header("Lists")]
    public List<PDials> dialLists = new List<PDials>();
    public List<PDials> currentDialLists = new List<PDials>();
    public List<string> enemyDialList = new List<string>();
    int currentDialogueBundle;
    string[] dialogueBundleSplit;

    [Header("Settings")]
    [SerializeField] private float transitionTimer;
    [SerializeField] private int minBaseDmg;
    [SerializeField] private int maxBaseDmg;
    [SerializeField] private int enemySelfHarmMinDmg;
    [SerializeField] private int enemySelfHarmMaxDmg;
    [SerializeField] private float typeSpeed;
    [SerializeField] private float generalCooldown;
    
    [Header("Sanity Settings")]//Chance of sanity effect triggering from full sanity to insanity
    [SerializeField] private float sanityEffectChanceLVL1;
    [SerializeField] private float sanityEffectChanceLVL2;
    [SerializeField] private float sanityEffectChanceLVL3;
    [SerializeField] private float sanityEffectChanceLVL4;
    [SerializeField] private float sanityEffectChanceLVL5;

    [Header("Text Color Coding")]
    [SerializeField] private string notEffective;
    [SerializeField] private string superEffective;
    [SerializeField] private string normallyEffective;

    [Header("Remnant Settings")]
    [SerializeField] private int remnant1TriggerChance;
    bool remnant1Acquired;

    private IEnumerator typeD; // Type out dialogue coroutine instance reference
    public bool optionsVisible, lastDialogueOn, typingDialogue, playerTurn;
    bool canInput;//Global disable and enable the input of player
    string currentText;//Last used enemy dialogue to not have consecutive same dialogue
    int lastRef = -1;//Last reference for enemy dialogue shuffle
    string enemyIntro;

    float currentSanity = PlayerCommonStatus.sanityValue;
    float sanityEffectChance;//Final val
    int runChanceVal;
    private void Awake()
    {
        //transition
        Time.timeScale = 1;
    }

    #region start
    private void Start()
    {
        //----------------------------Values and reference intialization-----------------------------------


        //Deciding dialogue based on enemy type
        string[] enemyEmoTypeSplit = newDialogueFile.text.Split("\n<SPLIT>");
        int emoTypeIndex = -1;
        string enemyType = enemyEmotion.emotion.currentType;
        if (enemyType == "Delusional") { emoTypeIndex = 0;}
        else if (enemyType == "Hatred") { emoTypeIndex = 1;}
        else if (enemyType == "Self_Loathing") { emoTypeIndex = 2;}
        else if (enemyType == "Despair") { emoTypeIndex = 3; }
        else if (enemyType == "Righteousness") { emoTypeIndex = 4;}
        dialogueBundleSplit = enemyEmoTypeSplit[emoTypeIndex].Split("\n///");       
        enemyIntro = dialogueBundleSplit[0].Split("\n")[1];//Setting enemy intro
        currentDialogueBundle = 0;



        //Button listeners and text references
        btnList = new List<Button> { btn1, btn2, btn3, btn4 };
        btnTXTList = new List<TextMeshProUGUI> { bText1, bText2, bText3, bText4 };
        for (int x = 0; x < btnList.Count; x++)
        {
            int indexTemp = x;//Workaround so it doesnt only reference X value after loop
            btnList[x].onClick.AddListener(delegate { clickOption(btnList[indexTemp].gameObject.name); });
            //btnList[x].on(btnList[x].Select());
            btnTXTList[x] = btnList[x].GetComponentInChildren<TextMeshProUGUI>();
        }
        //-------------------------------------------------------------------------------------------------------

        //+++++++++++++++++++ Functions to run at start ++++++++++++++++++++++
        if(PlayerCommonStatus.checkIfRemnantExist("bear"))
        {
            if (PlayerCommonStatus.checkRemnantAcquired("bear"))
            {
                remnant1Acquired = true;
            }
            else
            {
                remnant1Acquired = false;
            }
        }
        else
        {
            remnant1Acquired = true;
            //True for testing purposes
            //This is for when u just run battle scene without going through exploration scene
            //If player went through exploration scene, all remnants should exist
        }
        
        
        canInput = false;
        typingDialogue = false;
        playerTurn = true;
        if (currentSanity >= 100) { sanityEffectChance =sanityEffectChanceLVL1; }
        else if (currentSanity >= 80) { sanityEffectChance =sanityEffectChanceLVL2; }
        else if (currentSanity >= 60) { sanityEffectChance = sanityEffectChanceLVL3; }
        else if (currentSanity >= 40) { sanityEffectChance =sanityEffectChanceLVL4; }
        else if (currentSanity >= 20) { sanityEffectChance =sanityEffectChanceLVL5; }
        else { sanityEffectChance = 50; }
        shuffleOptionsAtStart();
        updateRunChance();
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    }
    #endregion
    // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>  Update <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.Mouse0))  && !optionsVisible && !lastDialogueOn && canInput)
        {
            
            if(typingDialogue)
            {
                instantShowDialogue();
            }
            else
            {
                nextDialogue();
            }
            
        }
    }
    #region internal functions
    //==================================================================== Internal Functions to be called ================================================================
    public void nextDialogue()
    {
        canInput = false;

        battle.turnNum++;

        StartCoroutine(enableInput());
    }
    public void instantShowDialogue()
    {
        canInput = false;
        typingDialogue = false;
        StopCoroutine(typeD);
        

        convoTextPlayer.text = currentText;
        convoTextEnemy.text = currentText;
        StartCoroutine(enableInput());
    }
    void shuffleOptionsAtStart()
    {

        /*Rationality = 2
         *Love = 3
         *Hope = 4
         *Acceptance = 5
         *
         *All dialogue options goes by this sequence
         */

        string[] currentBundleOptions = dialogueBundleSplit[currentDialogueBundle].Split("\n");

        for (int x= 2; x< currentBundleOptions.Length; x++)
        {
            string curEmo="";
            switch (x)
            {
                case 2:
                    curEmo = "Rationality";
                    break;
                case 3:
                    curEmo = "Love";
                    break;
                case 4:
                    curEmo = "Hope";
                    break;
                case 5:
                    curEmo = "Acceptance";
                    break;
            }
            dialLists.Add(new PDials(currentBundleOptions[x], curEmo));
            
        }

        for (int z = 0; z < btnList.Count; z++)
        {
            switchOutThisOption(z);
        }

        int remChance = Random.Range(1, 100);
        if (remChance <= remnant1TriggerChance && remnant1Acquired)
        {
            circleBestOption();
        }
        
    }
    public void clickOption(string objName)
    {
        string[] objArray = objName.Split("_");
        string dmgType = objArray[0];
        int dmgValue = int.Parse(objArray[1]);

        audioSrc.clip = SFList[1];
        audioSrc.Play();

        enemyEmotion.TakeDamage(dmgValue, dmgType);
        playerDialogueBoxShow(int.Parse(objArray[2]), bool.Parse(objArray[3]));

        currentDialLists.Clear();
        turnScriptRef.turnUpdate();

        //Shake screen visual effect, shake enemy and bg rn
        if (enemyEmotion.emotionEffectivenss(dmgType)==1.0f)
        {
            enemyShakeRef.ShakeScreen(0.2f);
        }
        else if(enemyEmotion.emotionEffectivenss(dmgType) == 1.5f)
        {
            enemyShakeRef.ShakeScreen(0.3f, 0.6f);
        }
        else
        {
            enemyShakeRef.ShakeScreen(0.2f, 0.1f);
        }
        

    }
    void switchOutThisOption(int btnIndex)//Switch out used dialogue option and take random dialogue option from the pool
    {
        int rand = Random.Range(0, dialLists.Count-1);
        
        string thisDialogue ="";
        string effectiveColor = returnEffectiveColor(dialLists[rand].emotions);
        bool highlightState = false;
        GameObject circleUI;


;       if(Random.Range(1, 100) <= sanityEffectChance)
        {
            thisDialogue = dialLists[rand].dialogues.Replace("[", $"<color={effectiveColor}>").Replace("]", "</color>");
            highlightState = true;
        }
        else
        {
            thisDialogue = dialLists[rand].dialogues.Replace("[", "").Replace("]", "");
        }
        
        btnTXTList[btnIndex].text = thisDialogue;
        
        btnList[btnIndex].gameObject.name = $"{dialLists[rand].emotions}_{Random.Range(minBaseDmg, maxBaseDmg)}_{btnIndex}_{highlightState}";//Format of  <emotiontype_DamageValue_btnReferenceIndex_effectiveColor>
        currentDialLists.Add(dialLists[rand]);
        dialLists.RemoveAt(rand);


        //-------Reset hint circles
        for (int ind = 0; ind < btnList.Count; ind++)
        {
            circleUI = btnList[ind].transform.Find("TC").gameObject;
            circleUI.SetActive(false);
        }
        /*  
         * Only when navigation is turned on, can select using arrow keys/wasd
        if (btnIndex == 0)
        {
            btnList[btnIndex].Select();
        }
        //*/
    }
    void circleBestOption()
    {
        List<float> dmgList = new List<float>();
        GameObject circleUI;
        int bestOption = 0; float bestValue = 100;
        float min = enemyEmotion.minThreshold;
        float max = enemyEmotion.maxThreshold;
        float cur = enemyEmotion.currentThreshold;
        float mid = min+((max - min) / 2);

        //Remnant Effect
        for (int ind = 0; ind<btnList.Count;ind++)
        {
            string[] nameSplit = btnList[ind].gameObject.name.Split("_");
            float finalDmg = float.Parse(nameSplit[1]) * enemyEmotion.emotionEffectivenss(nameSplit[0]);

            float afterDmg = cur + finalDmg;
            //Debug.Log(afterDmg);
            float currentValue = 0;

            if (afterDmg > mid)
            {
                currentValue = afterDmg - mid;
            }
            else if (afterDmg < mid)
            {
                currentValue = mid - afterDmg;
            }
            else
            {
                bestValue = 0;
                bestOption = ind;
            }
            
            if (currentValue < bestValue)
            {
                bestOption = ind;
                bestValue = currentValue;
            }
            
        }
        for (int ind = 0; ind < btnList.Count; ind++)
        {
            circleUI = btnList[ind].transform.Find("TC").gameObject;
            if (ind == bestOption)
            {
                circleUI.SetActive(true);
            }
            else
            {
                circleUI.SetActive(false);
            }
        }

    }
    IEnumerator enableInput()//Cooldown so player cant spam and immediately skip through entire conversation
    {
        while (!canInput)
        {
            yield return new WaitForSecondsRealtime(generalCooldown);
            canInput = true;
        }
    }
    IEnumerator typeDialogue(string content, TextMeshProUGUI txtbox, string emo=null)//Display character 1 by 1, visual effect
    {
        typingDialogue = true;
        
        string fColor = emo!=null?returnEffectiveColor(emo):null;
        txtbox.text = "";
        bool colorFontMode = false;

        audioSrc.clip = SFList[0];

        foreach (char letter in content)
        {

            if (letter == '[')
            {
                colorFontMode = true;
                continue;
            }
            else if (letter == ']')
            {
                colorFontMode = false;
                continue;
            }
            if (colorFontMode)
            {
                if (fColor != null)
                {
                    txtbox.text += $"<color={fColor}>{letter}</color>";
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
            if(Random.Range(0, PlayerCommonStatus.typeBeepChance) == 0)
            {
                audioSrc.Play();
            }
            
            yield return new WaitForSecondsRealtime(typeSpeed);
        }
        typingDialogue = false;

    }
    public string returnEffectiveColor(string emo)
    {
        //Debug.Log(emo);
        float effectiveness = enemyEmotion.emotionEffectivenss(emo);
        if (effectiveness == -1.0f)
        {
            return notEffective;
        }
        else if (effectiveness == 1.5f)
        {
            return superEffective;
        }
        else
        {
            return normallyEffective;
        }
    }
    #endregion

    #region external functions and initializations
    //================================================================== External Functions to be called ==================================================================
    public void noBattleStateInitialize()
    {

        optionsVisible = true;
        
        if (battle.turnNum > battle.maxTurn)
        {
            talkerName.text = "Info";
            playerDialogueUI.SetActive(true);
            playerOptionsUI.SetActive(false);
            enemyDialogueUI.SetActive(false);
            if (enemyEmotion.checkTargetThreshold() == true)
            {
                typeD = typeDialogue("A soul was saved...", convoTextPlayer);
                PlayerCommonStatus.modifySanity(20);
            }
            else
            {
                audioSrc.clip = SFList[2];
                audioSrc.Play();
                typeD = typeDialogue("Alex felt something left his body... something that seemed important..", convoTextPlayer);
                PlayerCommonStatus.modifySanity(-20);
            }

            //>>>>>>>>>>>>>>>>>>>>TRANSITION<<<<<<<<<<<<<<<<<<<<<<<<
            StartCoroutine(LoadBackLevel());
            //End of battle, switch back to main scene
        }
        else
        {
            //talkerName.text = "Info";
            playerDialogueUI.SetActive(false);
            playerOptionsUI.SetActive(false);
            enemyDialogueUI.SetActive(true);

            typeD = typeDialogue(enemyIntro, convoTextEnemy);
            enemyLastConvo.text = enemyIntro;
        }

        StartCoroutine(typeD);
    }
    public void playerTurnInitialize()
    {
        playerTurn = true;
        lastConvoUI.SetActive(true);
        playerDialogueUI.SetActive(false);
        playerOptionsUI.SetActive(true);
        enemyDialogueUI.SetActive(false);
        optionsVisible = true;
    }
    public void playerDialogueBoxShow(int btnIndex, bool highlighted)
    {

        StartCoroutine(enableInput());
        lastConvoUI.SetActive(false);
        playerDialogueUI.SetActive(true);
        playerOptionsUI.SetActive(false);
        enemyDialogueUI.SetActive(false);
        optionsVisible = false;

        talkerName.text = "Alex";
        currentText = btnTXTList[btnIndex].text;
        typeD = typeDialogue(currentDialLists[btnIndex].dialogues, convoTextPlayer, highlighted?currentDialLists[btnIndex].emotions:null);
        StartCoroutine(typeD);
        
    }
    public void enemyTurnInitialize()
    {
        playerTurn = false;
        StartCoroutine(enableInput());
        lastConvoUI.SetActive(false);
        playerDialogueUI.SetActive(false);
        playerOptionsUI.SetActive(false);
        enemyDialogueUI.SetActive(true);

        
        int randE = Random.Range(1, dialogueBundleSplit.Length);
        while (randE == lastRef)
        {
            randE = Random.Range(1, dialogueBundleSplit.Length);
        }
        string[] dialBundleInDepthSplit = dialogueBundleSplit[randE].Split("\n");
        currentText = dialBundleInDepthSplit[1];//The enemy reply is always index 1, index 0 is empty
        enemyLastConvo.text = dialBundleInDepthSplit[1];
        typeD = typeDialogue(dialBundleInDepthSplit[1], convoTextEnemy);
        StartCoroutine(typeD);

        currentDialogueBundle = randE;//Set the options for the players
        shuffleOptionsAtStart();

        lastRef = randE;
        enemyEmotion.selfHarm(Random.Range(enemySelfHarmMinDmg, enemySelfHarmMaxDmg));

    }


    IEnumerator LoadBackLevel()
    {
        lastDialogueOn = true;
        yield return new WaitForSeconds(transitionTimer);
        lastDialogueOn = false;

        SceneManager.LoadScene((int)sceneIndex.LV1);
    }
    #endregion
    //----------------------------------------------------- On click functions -----------------------------------------------------------------------------------------

    public void showRunAway()
    {
        
        
        runChance.text = $"{runChanceVal}% Chance";
        runUI.SetActive(true);
        lastConvoUI.SetActive(false);
        playerOptionsUI.SetActive(false);
    }
    public void updateRunChance()
    {
        int runCount = PlayerCommonStatus.getRunCount();
        runChanceVal = PlayerCommonStatus.runChance;
        switch (runCount)
        {
            case 0:
                runText.text = "Run from battle?";
                break;
            case 1:
                runText.text = "Running away again?";
                break;
            case 2:
                runText.text = "This will make it the third :)";
                break;
            case 3:
                runText.text = "Can't help it right?";
                break;
            case 4:
                runText.text = "Ah... sweet escape";
                break;
            default:
                runText.text = "Chances are.. You're not getting away";
                runChanceVal = 0;
                break;
        }
    }
    public void closeRunAway()
    {
        runUI.SetActive(false);
        lastConvoUI.SetActive(true);
        playerOptionsUI.SetActive(true);
    }
    public void runAway()
    {
        if (Random.Range(1,100)<=runChanceVal)
        {
            talkerName.text = "Info";
            typeD = typeDialogue("Alex ran away from his problems...", convoTextPlayer);
            StartCoroutine(typeD);
            playerDialogueUI.SetActive(true);
            runUI.SetActive(false);
            PlayerCommonStatus.addRunCount();
            PlayerCommonStatus.setRunChance(PlayerCommonStatus.runChance - 20);
            StartCoroutine(LoadBackLevel());
        }
        else
        {
            StartCoroutine(enableInput());
            optionsVisible = false;
            typeD = typeDialogue("YOU CAN'T RUN AWAY FROM ME, NEVER AGAIN", convoTextEnemy);
            currentText = "YOU CAN'T RUN AWAY FROM ME, NEVER AGAIN";
            StartCoroutine(typeD);
            enemyDialogueUI.SetActive(true);
            runUI.SetActive(false);

            turnScriptRef.turnUpdate();
            PlayerCommonStatus.setRunChance(PlayerCommonStatus.runChance - 10);
            runChanceVal = PlayerCommonStatus.runChance;
        }
        
    }
}
