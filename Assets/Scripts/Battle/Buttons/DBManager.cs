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

    [Header("UI References")]
    public TextAsset pDialoguefile;//Player dialogue options file
    public TextAsset eDialoguefile;//Enemy dialogue options file
    public TextMeshProUGUI convoTextPlayer, convoTextEnemy, talkerName, enemyLastConvo;//Text dialogue box reference

    public Button btn1; public Button btn2; public Button btn3; public Button btn4;
    TextMeshProUGUI bText1; TextMeshProUGUI bText2; TextMeshProUGUI bText3; TextMeshProUGUI bText4;
    List<Button> btnList;
    List<TextMeshProUGUI> btnTXTList;

    public GameObject playerOptionsUI, playerDialogueUI, enemyDialogueUI, lastConvoUI;

    [Header("Lists")]
    public List<PDials> dialLists = new List<PDials>();
    public List<string> enemyDialList = new List<string>();

    [Header("Settings")]
    [SerializeField] private float transitionTimer;
    [SerializeField] private int minBaseDmg;
    [SerializeField] private int maxBaseDmg;
    [SerializeField] private int enemySelfHarmMinDmg;
    [SerializeField] private int enemySelfHarmMaxDmg;
    [SerializeField] private float typeSpeed;
    [SerializeField] private float generalCooldown;
    private IEnumerator typeD; // Type out dialogue coroutine instance reference
    bool optionsVisible, lastDialogueOn, typingDialogue, playerTurn;

    bool canInput;//Global disable and enable the input of player
    string currentText;

    private void Awake()
    {
        //transition
        Time.timeScale = 1;
    }

    private void Start()
    {
        //-------------------------Values and reference intialization-----------------------------------

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
        if (enemyType == "Delusional") { indexInEnemyDialogueFile = 0; }
        else if (enemyType == "Hatred") { indexInEnemyDialogueFile = 1; }
        else if (enemyType == "Self_Loathing") { indexInEnemyDialogueFile = 2; }
        else if (enemyType == "Despair") { indexInEnemyDialogueFile = 3; }
        else if (enemyType == "Righteousness") { indexInEnemyDialogueFile = 4; }
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
            btnTXTList[x] = btnList[x].GetComponentInChildren<TextMeshProUGUI>();
        }
        //-------------------------------------------------------------------------------------------------------

        //+++++++++++++++++++ Functions to run at start ++++++++++++++++++++++

        shuffleOptionsAtStart();
        canInput = false;
        typingDialogue = false;
        playerTurn = true;
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    }

    // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>  Update <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    void Update()
    {
        if ((Input.GetKey(KeyCode.Space)|| Input.GetKey(KeyCode.Mouse0))  && !optionsVisible && !lastDialogueOn && canInput)
        {
            
            if(typingDialogue)
            {
                instantShowDialogue();
                //Debug.Log("instant moment");
            }
            else
            {
                nextDialogue();
                //Debug.Log("Progressing");
            }
            
        }
    }
    //=============================== Internal Functions to be called ================================================
    public void nextDialogue()
    {
        canInput = false;

            battle.turnNum++;


        Debug.Log($"Real turn : {battle.turnNum}");

        StartCoroutine(enableInput());
    }
    public void instantShowDialogue()
    {
        canInput = false;
        typingDialogue = false;
        StopCoroutine(typeD);
        if (playerTurn)
        {
            convoTextPlayer.text = currentText;
        }
        else
        {
            convoTextEnemy.text = currentText;
        }
        StartCoroutine(enableInput());
    }
    void shuffleOptionsAtStart()
    {
        for (int z = 0; z < btnList.Count; z++)
        {
            switchOutThisOption(z);
        }

    }
    int getRandFromList()
    {
        return Random.Range(0, dialLists.Count);
    }
    public void clickOption(string objName)
    {
        string enemyType = objName.Split("_")[0];
        int dmgValue = int.Parse(objName.Split("_")[1]);

        enemyEmotion.TakeDamage(dmgValue, enemyType);
        playerDialogueBoxShow(int.Parse(objName.Split("_")[2]));
        switchOutThisOption(int.Parse(objName.Split("_")[2]));

    }
    void switchOutThisOption(int btnIndex)//Switch out used dialogue option and take random dialogue option from the pool
    {
        int rand = getRandFromList();
        btnTXTList[btnIndex].text = dialLists[rand].dialogues;
        btnList[btnIndex].gameObject.name = $"{dialLists[rand].emotions}_{Random.Range(minBaseDmg, maxBaseDmg)}_{btnIndex}";//Format of  <emotiontype_DamageValue_btnReferenceIndex>
        dialLists.RemoveAt(rand);
    }
    IEnumerator enableInput()//Cooldown so player cant spam and immediately skip through entire conversation
    {
        while (!canInput)
        {
            yield return new WaitForSecondsRealtime(generalCooldown);
            canInput = true;
        }
    }
    IEnumerator typeDialogue(string content, TextMeshProUGUI txtbox)//Display character 1 by 1, visual effect
    {
        typingDialogue = true;
        txtbox.text = "";
        foreach (char letter in content)
        {
            txtbox.text += letter;
            yield return new WaitForSecondsRealtime(typeSpeed);
        }
        //Debug.Log("Finished typing");
        typingDialogue = false;

    }
    //=============================== External Functions to be called ================================================
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
            }
            else
            {
                typeD = typeDialogue("Alex felt something left his body... something that seemed important..", convoTextPlayer);
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
            typeD = typeDialogue("<Starter insult>.... doesnt affect bar ", convoTextEnemy);
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
    public void playerDialogueBoxShow(int btnIndex)
    {

        StartCoroutine(enableInput());
        lastConvoUI.SetActive(false);
        playerDialogueUI.SetActive(true);
        playerOptionsUI.SetActive(false);
        enemyDialogueUI.SetActive(false);
        optionsVisible = false;

        talkerName.text = "Alex";
        currentText = btnTXTList[btnIndex].text;
        typeD = typeDialogue(btnTXTList[btnIndex].text, convoTextPlayer);
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

        int lastRef = -1;
        int randE = Random.Range(0, enemyDialList.Count);
        while (lastRef == randE)
        {
            randE = Random.Range(0, enemyDialList.Count);
        }


        currentText = enemyDialList[randE];
        enemyLastConvo.text = enemyDialList[randE];
        typeD = typeDialogue(enemyDialList[randE], convoTextEnemy);
        StartCoroutine(typeD);
        lastRef = randE;
        enemyEmotion.selfHarm(Random.Range(0, 20));
    }


    IEnumerator LoadBackLevel()
    {
        lastDialogueOn = true;
        yield return new WaitForSeconds(transitionTimer);
        lastDialogueOn = false;
        SceneManager.LoadSceneAsync("Lvl 1");
    }
}
