using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoBattleState : BattleBaseState
{
    BattleStateManager battle;
    

    public override void EnterState(BattleStateManager battle)
    {
        battle.turnNum = 0;
    }
    public override void UpdateState(BattleStateManager battle)
    {
        if (battle.inBattle)
            battle.SwitchState(battle.pTurn);
    }
}
