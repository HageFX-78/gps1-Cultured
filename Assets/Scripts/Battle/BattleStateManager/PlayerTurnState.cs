using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnState : BattleBaseState
{
    BattleStateManager battle;
    public override void EnterState(BattleStateManager battle, DBManager dialbtn)
    {
        //battle.turnNum++;
        //Debug.Log("Turn : " + Mathf.RoundToInt(battle.turnNum / 1.8f));
        dialbtn.playerTurnInitialize();
    }

    public override void UpdateState(BattleStateManager battle, DBManager dialbtn)
    {
        
        if (battle.turnNum == battle.maxTurn)
        {
            battle.inBattle = false;
            battle.SwitchState(battle.battleless, dialbtn);
        }
        else if (battle.turnNum % 2 == 0)
        {
            battle.SwitchState(battle.eTurn, dialbtn);
        }
    }
}
