using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnState : BattleBaseState
{
    BattleStateManager battle;
    public override void EnterState(BattleStateManager battle, DBManager dialbtn)
    {
        dialbtn.enemyTurnInitialize();
    }
    public override void UpdateState(BattleStateManager battle, DBManager dialbtn)
    {
        
        if(battle.turnNum%2!=0)
        {
            //  Debug.Log("Enemy choice");
            if (battle.turnNum > battle.maxTurn)
            {
                //  battle.inBattle = false;
                battle.SwitchState(battle.battleless, dialbtn);
            }
            else
            {
                battle.SwitchState(battle.pTurn, dialbtn);

            }
            
        }

    }
}
