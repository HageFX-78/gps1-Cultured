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
    public TextMeshProUGUI convoText; public TextMeshProUGUI talkerName;
    public Button btn1; public Button btn2; public Button btn3; public Button btn4;
    List<Button> btnList;
    TextMeshProUGUI bText1; TextMeshProUGUI bText2; TextMeshProUGUI bText3; TextMeshProUGUI bText4;
    List<TextMeshProUGUI> btnTXTList;
    public GameObject playerOptionsUI, dialogueUI;

    [Header("Lists")]
    public List<PDials> dialLists = new List<PDials>();
    public List<string> enemyDialList = new List<string>();

    [Header("Settings")]
    [SerializeField] private int minBaseDmg;
    [SerializeField] private int maxBaseDmg;

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
            int indexTemp = x;//Weird workaround so it doesnt reference X after loop
            btnList[x].onClick.AddListener(delegate { clickOption(btnList[indexTemp].gameObject.name); });
            btnTXTList[x] = btnList[x].GetComponentInChildren<TextMeshProUGUI>();
        }
        /*
        btn1.onClick.AddListener(delegate { clickOption(btn1.gameObject); });
        bText1 = btn1.GetComponentInChildren<TextMeshProUGUI>();
        btn2.onClick.AddListener(delegate { clickOption(btn2.gameObject); });
        bText2 = btn2.GetComponentInChildren<TextMeshProUGUI>();
        btn3.onClick.AddListener(delegate { clickOption(btn3.gameObject); });
        bText3 = btn3.GetComponentInChildren<TextMeshProUGUI>();
        btn4.onClick.AddListener(delegate { clickOption(btn4.gameObject); });
        bText4 = btn4.GetComponentInChildren<TextMeshProUGUI>();
        //*/
        //-------------------------------------------------------------------------------------------------------

        //+++++++++++++++++++ Functions to run at start ++++++++++++++++++++++

        shuffleOptionsAtStart();

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void shuffleOptionsAtStart()
    {
        for(int z=0;z<btnList.Count;z++)
        {
            int rand = getRandFromList();
            btnTXTList[z].text = dialLists[rand].dialogues;
            btnList[z].gameObject.name = $"{dialLists[rand].emotions}_{Random.Range(minBaseDmg, maxBaseDmg)}";//Format of  <emotiontype_DamageValue>
            dialLists.RemoveAt(rand);
        }
        
    }
    int getRandFromList()
    {
        return Random.Range(0, dialLists.Count);
    }
    void testCheckDialogueList()
    {
        Debug.Log(dialLists.Count);
    }
    public void clickOption(string objName)
    {
        Debug.Log(objName);
        int rand = getRandFromList();
        //Deal dmg call from emotion manager HERE
        string enemyType = objName.Split("_")[0];
        int dmgValue = int.Parse(objName.Split("_")[1]);
        enemyEmotion.TakeDamage(dmgValue, enemyType);
        //Debug.Log(btnList[index].gameObject.name);
    }
}
