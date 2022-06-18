using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class Acceptance : MonoBehaviour
{
    public Button AcceptanceButton;
    string[] acceptance;
    string myFilePath, fileName;
    public string txtName;
    public TMPro.TextMeshProUGUI tmp;

    void Start()
    {

        fileName = txtName + ".txt";
        myFilePath = Application.dataPath + "/" + "Scripts" + "/" +"Battle" + "/" + "BattleDialogue" + "/" + fileName;
        ReadFromTheFile();
        Button btn = AcceptanceButton.GetComponent<Button>();
        tmp.text = acceptance[Random.Range(1, acceptance.Length)]; 
        btn.onClick.AddListener(onClick);
    }

    public void onClick()
    {
        Debug.Log("Acceptance GO!!!");
        Debug.Log(tmp.text);
    }

    public void ReadFromTheFile()
    {
        acceptance = File.ReadAllLines(myFilePath);
        System.Array.Sort(acceptance);
        
    }
    
}
