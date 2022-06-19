using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class Hope : MonoBehaviour
{
    public PDialogueLists dialogueList;
    public Button HopeButton;

    public TMPro.TextMeshProUGUI tmp;
    public BattleStateManager battle;
    private int prevNum = -1;
    private int diaNum = -1;


    void Start()
    {
        ReadFromTheFile();
        Button btn = HopeButton.GetComponent<Button>();

        btn.onClick.AddListener(onClick);
    }

    public void onClick()
    {
        if (battle.turnNum % 2 != 0)
        {
            // Debug.Log("Hope GO!!!");
            Debug.Log(tmp.text);
            ReadFromTheFile();
            battle.turnNum++;
        }
    }

    public void ReadFromTheFile()
    {
        while (diaNum == prevNum || dialogueList.dialLists[diaNum].emotions != 2)
        {
            diaNum = Random.Range(0, dialogueList.dialLists.Count);
        }
        prevNum = diaNum;
        tmp.text = dialogueList.dialLists[diaNum].dialogues;

    }

}
