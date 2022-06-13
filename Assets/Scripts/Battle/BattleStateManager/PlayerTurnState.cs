using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState : BattleBaseState
{
    BattleStateManager battle;

    public override void EnterState(BattleStateManager battle)
    {
        battle.turnNum++;
        Debug.Log("Turn : " + Mathf.RoundToInt(battle.turnNum / 1.8f));
    }
    public override void UpdateState(BattleStateManager battle)
    {
        
        if (battle.turnNum == battle.maxTurn)
        {
            battle.inBattle = false;
            battle.SwitchState(battle.battleless);
        }
        else if (battle.turnNum % 2 == 0)
        {
            battle.SwitchState(battle.eTurn);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Player Choice");
            battle.turnNum++;
        }

    }
}
