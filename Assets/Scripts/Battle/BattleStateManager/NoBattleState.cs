using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoBattleState : BattleBaseState
{
    BattleStateManager battle;
    float Timer = 3f;
    

    public override void EnterState(BattleStateManager battle, DBManager dialbtn)
    {
        battle.turnNum = 1;
        Timer = 3f;
        dialbtn.noBattleStateInitialize();
    }

    public override void UpdateState(BattleStateManager battle, DBManager dialbtn)
    {
        if (Timer > 0f)
        {
            Timer -= Time.deltaTime;
        }
        else
        {
            if(battle.turnNum<battle.maxTurn)
            {
                battle.SwitchState(battle.pTurn, dialbtn);
            }
            
        }
    }
}
