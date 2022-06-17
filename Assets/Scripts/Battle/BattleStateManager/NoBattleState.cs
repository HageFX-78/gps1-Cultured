using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoBattleState : BattleBaseState
{
    BattleStateManager battle;
    float Timer = 3f;
    

    public override void EnterState(BattleStateManager battle)
    {
        battle.turnNum = 0;
        Timer = 3f;
    }

    public override void UpdateState(BattleStateManager battle)
    {
        if (Timer >0f)
        {
            Timer -= Time.deltaTime;
        }
        else
            battle.SwitchState(battle.pTurn);
    }
}
