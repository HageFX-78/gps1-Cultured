using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Turn : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public BattleStateManager battle;
    // Update is called once per frame
    /*
    void Update()
    {
        textMeshProUGUI.text = "Turn " + Mathf.RoundToInt(battle.turnNum / 1.9f);
    }//*/
    int turnLeft;
    private void Start()
    {
        turnLeft = battle.maxTurn / 2;
        textMeshProUGUI.text = "Turns left:" + turnLeft;
    }
    public void turnUpdate()
    {
        turnLeft = Mathf.Clamp(turnLeft-1, 0, turnLeft);
       
        textMeshProUGUI.text = "Turns left:" + turnLeft;
    }
}
