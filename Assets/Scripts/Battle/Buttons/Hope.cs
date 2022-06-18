using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class Hope : MonoBehaviour
{
    public Button HopeButton;
    string[] hope;
    int[] used = { -1, -1, -1, -1, -1  };
    string myFilePath, fileName;
    public string txtName;
    public TMPro.TextMeshProUGUI tmp;
    public BattleStateManager battle;
    public int arrayNum;

    void Start()
    {
        fileName = txtName + ".txt";
        myFilePath = Application.dataPath + "/" + "Scripts" + "/" + "Battle" + "/" + "BattleDialogue" + "/" + fileName;
        txtChange();
        Button btn = HopeButton.GetComponent<Button>();
        btn.onClick.AddListener(onClick);
    }

    private void Update()
    {
        if (battle.nxtTurn)
        {
            txtChange();
            battle.nxtTurn = false;
        }
    }
    public void onClick()
    {
        if (battle.turnNum % 2 != 0)
        {
            Debug.Log("Hope GO!!!");
            Debug.Log(tmp.text);
            battle.turnNum++;
            battle.nxtTurn = true;
        }
    }
    public void txtChange()
    {
        ReadFromTheFile();
        arrayNum = Random.Range(0, hope.Length);
        for (int i = 0; i < used.Length; i++)
        {
            if (used[i] != -1 )
            used[i] = 0;
                }

        tmp.text = hope[arrayNum];
        hope[arrayNum] = null;
    }
    public void ReadFromTheFile()
    {
        hope = File.ReadAllLines(myFilePath);
        System.Array.Sort(hope);

    }
}
