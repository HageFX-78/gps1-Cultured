using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class Acceptance : MonoBehaviour
{
    public PDialogueLists dialogueList;
    public Button AcceptanceButton;

    public TMPro.TextMeshProUGUI tmp;
    public BattleStateManager battle;
    private int prevNum = -1;
    private int diaNum = -1;


    void Start()
    {
        ReadFromTheFile();
        Button btn = AcceptanceButton.GetComponent<Button>();
        Debug.Log("Acceptance" + diaNum);
        btn.onClick.AddListener(onClick);
    }

    public void onClick()
    {
        if (battle.turnNum % 2 != 0)
        {
            Debug.Log("Acceptance GO!!!");
            Debug.Log(tmp.text);
            ReadFromTheFile();
            battle.turnNum++;
        }
    }

    public void ReadFromTheFile()
    {
        while (diaNum == prevNum || dialogueList.dialLists[diaNum].emotions != 1)
        {
            diaNum = Random.Range(0, 15);
        }
        prevNum = diaNum;
        tmp.text = dialogueList.dialLists[diaNum].dialogues;

    }

}
