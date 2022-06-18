using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
public class Rationality : MonoBehaviour
{
    public Button RationalityButton;
    string[] rationality;
    string myFilePath, fileName;
    public string txtName;
    public TMPro.TextMeshProUGUI tmp;
    public BattleStateManager battle;

    void Start()
    {
        fileName = txtName + ".txt";
        myFilePath = Application.dataPath + "/" + "Scripts" + "/" + "Battle" + "/" + "BattleDialogue" + "/" + fileName;
        ReadFromTheFile();
        Button btn = RationalityButton.GetComponent<Button>();
        tmp.text = rationality[Random.Range(0, rationality.Length)];
        btn.onClick.AddListener(onClick);
    }

    public void onClick()
    {
        if (battle.turnNum % 2 != 0)
        {
            Debug.Log("Rationality GO!!!");
            Debug.Log(tmp.text);
            battle.turnNum++;
        }
    }

    public void ReadFromTheFile()
    {
        rationality = File.ReadAllLines(myFilePath);
        System.Array.Sort(rationality);

    }
}
