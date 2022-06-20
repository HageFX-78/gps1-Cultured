using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class Love : MonoBehaviour
{
    public PDialogueLists dialogueList;
    public Button LoveButton;

    public TMPro.TextMeshProUGUI tmp;
    public BattleStateManager battle;
    private int prevNum = -1;
    private int diaNum = -1;


    void Start()
    {
        ReadFromTheFile();
        Button btn = LoveButton.GetComponent<Button>();
        Debug.Log("Love" + diaNum);
        btn.onClick.AddListener(onClick);
    }

    public void onClick()
    {
        if (battle.turnNum % 2 != 0)
        {
            // Debug.Log("love GO!!!");
            Debug.Log(tmp.text);
            ReadFromTheFile();
            battle.turnNum++;
        }
    }

    public void ReadFromTheFile()
    {
        //while (diaNum == prevNum || dialogueList.dialLists[diaNum].emotions != 3)
        {
            diaNum = Random.Range(0, dialogueList.dialLists.Count);
        }
        prevNum = diaNum;
        tmp.text = dialogueList.dialLists[diaNum].dialogues;

    }
}
