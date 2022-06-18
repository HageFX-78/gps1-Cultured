using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class Love : MonoBehaviour
{
    public Button LoveButton;
    string[] love;
    string myFilePath, fileName;
    public string txtName;
    public TMPro.TextMeshProUGUI tmp;
    public BattleStateManager battle;

    void Start()
    {
        fileName = txtName + ".txt";
        myFilePath = Application.dataPath + "/" + "Scripts" + "/" + "Battle" + "/" + "BattleDialogue" + "/" + fileName;
        ReadFromTheFile();
        Button btn = LoveButton.GetComponent<Button>();
        tmp.text = love[Random.Range(0, love.Length)];
        btn.onClick.AddListener(onClick);
    }

    public void onClick()
    {
        if (battle.turnNum % 2 != 0)
        {
            Debug.Log("Love GO!!!");
            Debug.Log(tmp.text);
            battle.turnNum++;
        }
    }

    public void ReadFromTheFile()
    {
        love = File.ReadAllLines(myFilePath);
        System.Array.Sort(love);

    }
}
