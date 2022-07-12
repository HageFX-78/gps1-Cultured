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
    public ScreenShake camRef;

    [Header("UI References")]
    public TextAsset pDialoguefile;//Player dialogue options file
    public TextAsset eDialoguefile;//Enemy dialogue options file
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

    private void Start()
    {
        //----------------------------Values and reference intialization-----------------------------------

        //Reading text file for player dialogue options
        string[] categorySplit = pDialoguefile.text.Split("\n~");

        for (int x = 0; x < categorySplit.Length; x++)
        {
            string[] temp = categorySplit[x].Split(new string[] { "\n" }, System.StringSplitOptions.RemoveEmptyEntries);
            foreach (string y in temp)
            {
                string emotionStr = "";
                switch (x)
                {
                    case 0:
                        emotionStr = "Rationality";
                        break;
                    case 1:
                        emotionStr = "Hope";
                        break;
                    case 2:
                        emotionStr = "Love";
                        break;
                    case 3:
                        emotionStr = "Acceptance";
                        break;
                }
                dialLists.Add(new PDials(y, emotionStr));
                //Debug.Log(y);
            }
        }


        //Reading text file for enemy dialogues and assign based on type
        string[] enemyTypeSplit = eDialoguefile.text.Split("\n~");
        int indexInEnemyDialogueFile = -1;
        string enemyType = enemyEmotion.emotion.currentType;
        if (enemyType == "Delusional") { indexInEnemyDialogueFile = 0;enemyIntro = "Wow! A fateful encounter with a retard!"; }
        else if (enemyType == "Hatred") { indexInEnemyDialogueFile = 1; enemyIntro = "Thought I saw a pile of feces turns out it was you!"; }
        else if (enemyType == "Self_Loathing") { indexInEnemyDialogueFile = 2; enemyIntro = "Why must I TALK TO YOU"; }
        else if (enemyType == "Despair") { indexInEnemyDialogueFile = 3;enemyIntro = "Ever tried a double suicide?"; }
        else if (enemyType == "Righteousness") { indexInEnemyDialogueFile = 4; enemyIntro = "You pitiful soul.."; }
        string[] typeDialogueSplit = enemyTypeSplit[indexInEnemyDialogueFile].Split("\n");
        for (int x = 0; x < enemyTypeSplit.Length; x++)
        {
            enemyDialList.Add(typeDialogueSplit[x]);
            //Debug.Log(typeDialogueSplit[x]);
        }


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
        shuffleOptionsAtStart();
        canInput = false;
        typingDialogue = false;
        playerTurn = true;
        if (currentSanity >= 100) { sanityEffectChance =sanityEffectChanceLVL1; }
        else if (currentSanity >= 80) { sanityEffectChance =sanityEffectChanceLVL2; }
        else if (currentSanity >= 60) { sanityEffectChance = sanityEffectChanceLVL3; }
        else if (currentSanity >= 40) { sanityEffectChance =sanityEffectChanceLVL4; }
        else if (currentSanity >= 20) { sanityEffectChance =sanityEffectChanceLVL5; }
        else { sanityEffectChance = 50; }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    }

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
    //==================================================================== Internal Functions to be called ================================================================
    public void nextDialogue()
    {
        canInput = false;

        battle.turnNum++;
        //Debug.Log($"Real turn : {battle.turnNum}");

        StartCoroutine(enableInput());
    }
    public void instantShowDialogue()
    {
        canInput = false;
        typingDialogue = false;
        //typeSpeed = 0;
        StopCoroutine(typeD);
        

        convoTextPlayer.text = currentText;
        convoTextEnemy.text = currentText;
        //*/
        StartCoroutine(enableInput());
    }
    void shuffleOptionsAtStart()
    {
        for (int z = 0; z < btnList.Count; z++)
        {
            switchOutThisOption(z);
        }
        if(Random.Range(1,100)<=remnant1TriggerChance)
        {
            circleBestOption();
        }
        
    }
    int getRandFromList()
    {
        return Random.Range(0, dialLists.Count);
    }
    public void clickOption(string objName)
    {
        string[] objArray = objName.Split("_");
        string dmgType = objArray[0];
        int dmgValue = int.Parse(objArray[1]);

        enemyEmotion.TakeDamage(dmgValue, dmgType);
        playerDialogueBoxShow(int.Parse(objArray[2]), bool.Parse(objArray[3]));


        dialLists.AddRange(currentDialLists);//Shuffle options stuffs start here
        currentDialLists.Clear();
        shuffleOptionsAtStart();
        turnScriptRef.turnUpdate();

        //Shake screen visual effect, shake enemy and bg rn
        if (enemyEmotion.emotionEffectivenss(dmgType)==1.0f)
        {
            camRef.ShakeScreen(0.2f);
        }
        else if(enemyEmotion.emotionEffectivenss(dmgType) == 1.5f)
        {
            camRef.ShakeScreen(0.3f, 0.6f);
        }
        else
        {
            camRef.ShakeScreen(0.2f, 0.1f);
        }
        

    }
    void switchOutThisOption(int btnIndex)//Switch out used dialogue option and take random dialogue option from the pool
    {
        int rand = getRandFromList();
        string thisDialogue ="";
        string effectiveColor = returnEffectiveColor(dialLists[rand].emotions);
        bool highlightState = false;
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
            yield return new WaitForSecondsRealtime(typeSpeed);
        }
        //Debug.Log("Finished typing");
        //typeSpeed = 0.01f;
        typingDialogue = false;

    }
    public string returnEffectiveColor(string emo)
    {
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
        //canInput = false;
        //StartCoroutine(enableInput());
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

        
        int randE = Random.Range(0, enemyDialList.Count);
        while (randE == lastRef)
        {
            randE = Random.Range(0, enemyDialList.Count);
        }
        currentText = enemyDialList[randE];
        enemyLastConvo.text = enemyDialList[randE];
        typeD = typeDialogue(enemyDialList[randE], convoTextEnemy);
        StartCoroutine(typeD);
        lastRef = randE;
        enemyEmotion.selfHarm(Random.Range(enemySelfHarmMinDmg, enemySelfHarmMaxDmg));

    }


    IEnumerator LoadBackLevel()
    {
        lastDialogueOn = true;
        yield return new WaitForSeconds(transitionTimer);
        lastDialogueOn = false;

        SceneManager.LoadSceneAsync("Lvl 1");
    }
    //----------------------------------------------------- On click functions -----------------------------------------------------------------------------------------

    public void showRunAway()
    {
        int runCount = PlayerCommonStatus.getRunCount();
        switch (runCount)
        {
            case 0:
                runText.text = "Run from battle?";
                runChanceVal = 100;
                break;
            case 1:
                runText.text = "Running away again?";
                runChanceVal = 80;
                break;
            case 2:
                runText.text = "This will make it the third :)";
                runChanceVal = 60;
                break;
            case 3:
                runText.text = "Can't help it right?";
                runChanceVal = 40;
                break;
            case 4:
                runText.text = "Ah... sweet escape";
                runChanceVal = 20;
                break;
            default:
                runText.text = "Chances are.. You're not getting away";
                runChanceVal = 0;
                break;
        }
        runChance.text = $"{runChanceVal}% Chance";
        runUI.SetActive(true);
        lastConvoUI.SetActive(false);
        playerOptionsUI.SetActive(false);
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
        }
        
    }
}
