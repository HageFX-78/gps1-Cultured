using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DBManager : MonoBehaviour
{
    //DB stands for dialogue and button manager, not dick & balls
    [Header("Manager References")]
    public BattleStateManager battle;
    public EmotionManager enemyEmotion;

    [Header("UI References")]
    public TextAsset pDialoguefile;//Player dialogue options file
    public TextAsset eDialoguefile;//Enemy dialogue options file
    public TextMeshProUGUI convoTextPlayer, convoTextEnemy, talkerName;//Text dialogue box reference

    public Button btn1; public Button btn2; public Button btn3; public Button btn4;    
    TextMeshProUGUI bText1; TextMeshProUGUI bText2; TextMeshProUGUI bText3; TextMeshProUGUI bText4;
    List<Button> btnList;
    List<TextMeshProUGUI> btnTXTList;

    public GameObject playerOptionsUI, playerDialogueUI, enemyDialogueUI;

    [Header("Lists")]
    public List<PDials> dialLists = new List<PDials>();
    public List<string> enemyDialList = new List<string>();

    [Header("Settings")]
    [SerializeField] private int minBaseDmg;
    [SerializeField] private int maxBaseDmg;
    [SerializeField] private int enemySelfHarmMinDmg;
    [SerializeField] private int enemySelfHarmMaxDmg;
    [SerializeField] private float typeSpeed;
    [SerializeField] private float nextDialogueCooldown;
    bool dialogueCooldown;
    private IEnumerator typeD; // Type out dialogue coroutine instance reference
    bool optionsVisible;

    private void Start()
    {
        //-------------------------Values and reference intialization-----------------------------------

        //Reading text file for player dialogue options
        string[] categorySplit = pDialoguefile.text.Split("\n~");

        for (int x = 0; x < categorySplit.Length; x++)
        {
            string[] temp = categorySplit[x].Split(new string[] {"\n"}, System.StringSplitOptions.RemoveEmptyEntries);
            foreach(string y in temp)
            {
                string emotionStr="";
                switch(x)
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
        for (int x=0;x<enemyTypeSplit.Length;x++)
        {
            enemyDialList.Add(typeDialogueSplit[x]);
            //Debug.Log(typeDialogueSplit[x]);
        }


        //Button listeners and text references
        btnList = new List<Button> {btn1, btn2, btn3, btn4};
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
        dialogueCooldown = true;
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    }

    // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>  Update <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !dialogueCooldown && !optionsVisible)
        {
            nextDialogue();
        }
    }
    //=============================== Internal Functions to be called ================================================
    public void nextDialogue()
    {
        dialogueCooldown = true;
        StopCoroutine(typeD);  
        if(battle.turnNum<battle.maxTurn)
        {
            battle.turnNum++;
            StartCoroutine(endDialogueCooldown());
        }
        else
        {
            Debug.Log("Battle Ended");
            /*
             *  ????????????????????????????????
                ????????????????????????????????
                ????????????????????????????????
                ????????????????????????????????
                ????????????????????????????????
                ????????????????????????????????
                ????????????????????????????????
                ????????????????????????????????
                ????????????????????????????????
                ????????????????????????????????
                ????????????????????????????????
                ????????????????????????????????
                ????????????????????????????????
             */
            //Return to MAINSCENE code<________________________________________________________________________________________________________8=====================D
        }
        Debug.Log($"Real turn : {battle.turnNum}");
    }
    void shuffleOptionsAtStart()
    {
        for(int z=0;z<btnList.Count;z++)
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
        int rand = getRandFromList();
        string enemyType = objName.Split("_")[0];
        int dmgValue = int.Parse(objName.Split("_")[1]);

        enemyEmotion.TakeDamage(dmgValue, enemyType);
        playerDialogueBoxShow(int.Parse(objName.Split("_")[2]));
        switchOutThisOption(int.Parse(objName.Split("_")[2]));
        StartCoroutine(endDialogueCooldown());
    }
    void switchOutThisOption(int btnIndex)//Switch out used dialogue option and take random dialogue option from the pool
    {
        int rand = getRandFromList();
        btnTXTList[btnIndex].text = dialLists[rand].dialogues;
        btnList[btnIndex].gameObject.name = $"{dialLists[rand].emotions}_{Random.Range(minBaseDmg, maxBaseDmg)}_{btnIndex}";//Format of  <emotiontype_DamageValue_btnReferenceIndex>
        dialLists.RemoveAt(rand);
    }
    IEnumerator endDialogueCooldown()//Cooldown so player cant spam and immediately skip through entire conversation
    {
        while (dialogueCooldown)
        {

            yield return new WaitForSecondsRealtime(nextDialogueCooldown);
            dialogueCooldown = false;

        }
    }
    IEnumerator typeDialogue(string content, TextMeshProUGUI txtbox)//Display character 1 by 1, visual effect
    {
        txtbox.text = "";
        foreach (char letter in content)
        {
            txtbox.text += letter;
            yield return new WaitForSecondsRealtime(typeSpeed);
        }
    }
    //=============================== External Functions to be called ================================================
    public void noBattleStateInitialize()
    {

        //<---Possibly Start battle screen/animation code here too
        dialogueCooldown = true;
        playerDialogueUI.SetActive(true);
        playerOptionsUI.SetActive(false);
        enemyDialogueUI.SetActive(false);
        optionsVisible = false;
        talkerName.text = "Info";
        if (battle.turnNum==battle.maxTurn)
        {
            if(enemyEmotion.checkTargetThreshold()==true)
            {           
                typeD = typeDialogue("A soul was saved...", convoTextPlayer);
            }
            else
            {
                typeD = typeDialogue("Alex felt something left his body... something that seemed important..", convoTextPlayer);
            }
            
            //End of battle, switch back to main scene
        }
        else
        {
            typeD = typeDialogue("Welcome to hell....", convoTextPlayer);
        }
        
        StartCoroutine(typeD);
    }
    public void playerTurnInitialize()
    {
       
        playerDialogueUI.SetActive(false);
        playerOptionsUI.SetActive(true);
        enemyDialogueUI.SetActive(false);
        optionsVisible = true;
    }
    public void playerDialogueBoxShow(int btnIndex)
    {
        playerDialogueUI.SetActive(true);
        playerOptionsUI.SetActive(false);
        enemyDialogueUI.SetActive(false);
        optionsVisible = false;
        talkerName.text = "Alex";
        typeD = typeDialogue(btnTXTList[btnIndex].text, convoTextPlayer);

        StartCoroutine(typeD);
    }
    public void enemyTurnInitialize()
    {
        playerDialogueUI.SetActive(false);
        playerOptionsUI.SetActive(false);
        enemyDialogueUI.SetActive(true);
        int lastRef = -1;
        int randE = Random.Range(0, enemyDialList.Count);
        while (lastRef==randE)
        {
            randE = Random.Range(0, enemyDialList.Count);
        }

        typeD = typeDialogue(enemyDialList[randE], convoTextEnemy);
        StartCoroutine(typeD);
        lastRef = randE;
        enemyEmotion.selfHarm(Random.Range(0,20));
    }
}
