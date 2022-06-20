using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;


public class PDialogueLists : MonoBehaviour
{
    public string[] rationality;
    public string[] love;
    public string[] hope;
    public string[] acceptance;

    public BattleStateManager battle;
    public TextAsset pdialoguefile;
    string myFilePath;
    
    public List<PDials> dialLists = new List<PDials>();

    private void Start()
    {
        
        string[] categorySplit = pdialoguefile.text.Split("~");

        for (int x = 0; x < categorySplit.Length; x++)
        {
            string[] temp = categorySplit[x].Split("\n");
            foreach(string y in temp)
            {
                dialLists.Add(new PDials(y, ""));
            }
        }
        /*
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

        Debug.Log(dialLists.Count);
        //*/


    }
}
