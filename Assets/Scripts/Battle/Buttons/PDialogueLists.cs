using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;


public class PDialogueLists : MonoBehaviour
{
    [HideInInspector]
    public string[] rationality;
    [HideInInspector]
    public string[] love;
    [HideInInspector]
    public string[] hope;
    [HideInInspector]
    public string[] acceptance;

    public BattleStateManager battle;
    string myFilePath;
    
    public List<PDials> dialLists = new List<PDials>();

    private void Start()
    {
        //Acceptance
        myFilePath = Application.dataPath + "/" + "Scripts" + "/" + "Battle" + "/" + "BattleDialogue" + "/" + "PlayerAcceptance.txt";
        acceptance = File.ReadAllLines(myFilePath);
        System.Array.Sort(acceptance);
        for (int i = 0; i < acceptance.Length; i++)
        {
            dialLists.Add(new PDials(acceptance[i], 1));
        }

        //Hope
        myFilePath = Application.dataPath + "/" + "Scripts" + "/" + "Battle" + "/" + "BattleDialogue" + "/" + "PlayerHope.txt";
        hope = File.ReadAllLines(myFilePath);
        System.Array.Sort(hope);
        for (int i = 0; i < hope.Length; i++)
        {
            dialLists.Add(new PDials(hope[i], 2));
        }

        //Love
        myFilePath = Application.dataPath + "/" + "Scripts" + "/" + "Battle" + "/" + "BattleDialogue" + "/" + "PlayerLove.txt";
        love = File.ReadAllLines(myFilePath);
        System.Array.Sort(love);
        for (int i = 0; i < love.Length; i++)
        {
            dialLists.Add(new PDials(love[i], 3));
        }

        //Rationality
        myFilePath = Application.dataPath + "/" + "Scripts" + "/" + "Battle" + "/" + "BattleDialogue" + "/" + "PlayerRationality.txt";
        rationality = File.ReadAllLines(myFilePath);
        System.Array.Sort(rationality);
        for (int i = 0; i < rationality.Length; i++)
        {
            dialLists.Add(new PDials(rationality[i], 4));
        }


    }
}
